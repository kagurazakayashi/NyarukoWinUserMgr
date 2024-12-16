using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SystemRes;
using UserInfo;
namespace WinUserMgr
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// 控制等待動畫的啟用和關閉。
        /// </summary>
        /// <param name="enable">是否啟用等待動畫</param>
        private void waitAni(bool enable)
        {
            // 如果未啟用等待動畫並且結束模式為 -1，則直接返回
            if (!enable && timerStopWaitAniEndMode == -1)
            {
                return;
            }

            // 如果未啟用等待動畫並且結束模式為 1，則退出程式
            if (!enable && timerStopWaitAniEndMode == 1)
            {
                Application.Exit();
            }

            menuStrip.Enabled = !enable; // 停用或啟用工具欄
            labelWait.Visible = enable; // 顯示或隱藏等待標籤
            dataGridUsers.Visible = !enable; // 顯示或隱藏資料表格

            // 如果啟用了等待動畫並且存在動畫字型
            if (enable && slboot.aniFont != null)
            {
                timerWaitAni.Enabled = enable; // 啟用等待動畫計時器
                timerStopWaitAni.Enabled = enable; // 啟用停止動畫計時器
            }
            else
            {
                timerWaitAni.Enabled = false; // 停用等待動畫計時器
                timerStopWaitAni.Enabled = false; // 停用停止動畫計時器
                slboot.StopUpdateChar(); // 停止動畫更新
            }
        }

        /// <summary>
        /// 開始重新載入資料的操作。
        /// </summary>
        private void reloadData()
        {
            // 顯示載入動畫
            waitAni(true);
            loadConfig(); // 載入配置
            // 建立一個後臺執行緒執行資料載入
            Thread thread = new Thread(reloadDataThread);
            thread.IsBackground = true; // 設定執行緒為後臺執行緒
            thread.Start(); // 啟動執行緒
        }

        /// <summary>
        /// 從 INI 檔案載入配置。
        /// </summary>
        private void loadConfig()
        {
            // 檢查 INI 檔案是否存在
            if (!iniconf.ExistINIFile())
            {
                return;
            }

            try
            {
                // 載入每個組名
                string countS = iniconf.IniReadValue("Config", "GroupsCount");
                if (countS.Length == 0)
                {
                    return;
                }
                int count = int.Parse(countS);
                groupList = new string[count];
                for (int i = 0; i < count; i++)
                {
                    groupList[i] = iniconf.IniReadValue("Groups", $"G{i}");
                }
            }
            catch (Exception ex)
            {
                // 弹出错误提示
                if (MessageBox.Show(ex.Message, "配置读取失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    loadConfig();
                }
            }
        }

        private void openGroupSelectThread()
        {
            // 获取当前程序所在的文件夹路径
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // 拼接 exe 的完整路径
            string programPath = Path.Combine(currentDirectory, "GroupMgr.exe");
            Point currentPosition = new Point();
            Size currentSize = new Size();
            this.Invoke((Action)(() =>
            {
                currentPosition = this.Location;
                currentSize = this.Size;
            }));
            string arguments = $"{linkMachineName} {currentPosition.X},{currentPosition.Y},{currentSize.Width},{currentSize.Height}";
            // 创建 ProcessStartInfo 实例
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = programPath,
                Arguments = arguments,
                RedirectStandardOutput = false, // 如果需要读取标准输出
                RedirectStandardError = false,  // 如果需要读取标准错误
                UseShellExecute = false,        // 必须为 false 以重定向输出
                CreateNoWindow = false          // 不显示窗口
            };

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    // 启动进程
                    process.Start();
                    // 等待进程完成
                    process.WaitForExit();
                    // 获取退出代码
                    int exitCode = process.ExitCode;

                    // 读取标准输出和标准错误
                    //string output = process.StandardOutput.ReadToEnd();
                    //string error = process.StandardError.ReadToEnd();
                    //Console.WriteLine("Output: " + output);
                    //Console.WriteLine("Error: " + error);

                    // 退出代码
                    //MessageBox.Show("Exit Code: " + exitCode); // 0取消 1错误 2确定
                    if (exitCode == 2)
                    {
                        //如果使用者組選擇視窗的對話方塊返回結果為“確定”，重新載入資料。
                        this.Invoke((Action)reloadData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke((Action)(() =>
                {
                    if (MessageBox.Show(ex.Message, "未能打开对话框", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        toolStripButtonGroups_Click(null, null);
                    }
                }));
            }
            this.Invoke((Action)(() =>
            {
                menuStrip.Enabled = true;
                menuStrip.UseWaitCursor = false;
                ControlBox = true;
                if (isAdmin)
                {
                    dataGridUsers.ReadOnly = false;
                }
            }));
        }

        /// <summary>
        /// 後臺執行緒中執行的重新載入資料邏輯。
        /// </summary>
        private void reloadDataThread()
        {
            // 獲取指定機器的本地使用者列表
            userLoader.GetLocalUsers(linkMachineName);

            // 在主執行緒中更新UI
            this.Invoke((Action)reloadDataUpdateTable);
        }

        /// <summary>
        /// 重新載入資料並更新使用者資訊表格。
        /// </summary>
        private void reloadDataUpdateTable()
        {
            // 如果使用者列表中只有一個使用者且該使用者包含錯誤資訊
            if (userLoader.users.Count == 1 && userLoader.users[0].ErrorInfo.Length > 0)
            {
                // 顯示錯誤資訊彈窗
                MessageBox.Show(userLoader.users[0].ErrorInfo, "加载用户信息失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 使用當前機器名重新載入本地使用者資訊
                linkMachineName = Environment.MachineName;
                userLoader.GetLocalUsers(linkMachineName);
            }

            // 清空表格的所有行
            dataGridUsers.Rows.Clear();

            // 載入配置
            loadConfig();

            // 移除動態新增的列，保留預設的列數量
            for (int i = dataGridUsers.Columns.Count - 1; i >= defaultDataGridUsersColumnsCount; i--)
            {
                dataGridUsers.Columns.RemoveAt(i);
            }

            // 如果使用者組列表為空，新增顯示所有使用者組的文字列
            if (groupList.Length > 0)
            {
                // 為每個使用者組新增一個複選框列
                for (int i = 0; i < groupList.Length; i++)
                {
                    DataGridViewCheckBoxColumn groupColumn = new DataGridViewCheckBoxColumn
                    {
                        HeaderText = groupList[i], // 設定列頭文字為使用者組名稱
                        Name = $"Group{i}",       // 設定列名
                        Width = 80,               // 設定列寬
                    };
                    dataGridUsers.Columns.Add(groupColumn);
                }
            }

            // 更新視窗標題為當前機器名
            Text = linkMachineName + " 的用户目录";


            // 遍歷使用者列表，填充使用者資訊到表格
            foreach (UserInfoType user in userLoader.users)
            {
                // 準備填充表格的一行資料
                List<object> newRow = new List<object>();
                newRow.Add(user.Name);                       // 使用者名稱
                newRow.Add(user.FullName);                   // 全名
                newRow.Add(user.Description);                // 描述
                newRow.Add(user.AccountExpires);             // 賬戶過期時間
                newRow.Add("");
                newRow.Add(user.Disabled);                   // 是否禁用
                newRow.Add(user.PasswordNeverExpires);       // 密碼是否永不過期
                newRow.Add(user.UserMayNotChangePassword);   // 是否禁止使用者更改密碼
                newRow.Add(string.Join(", ", user.Groups));  // 所屬的所有使用者組

                if (groupList.Length > 0)
                {
                    // 根據使用者組列表，逐一檢查使用者是否屬於該使用者組
                    for (int i = 0; i < groupList.Length; i++)
                    {
                        string groupName = groupList[i];
                        newRow.Add(user.Groups.Contains(groupName)); // 添加布爾值表示是否屬於使用者組
                    }
                }

                // 將準備好的資料新增到表格中
                dataGridUsers.Rows.Add(newRow.ToArray());
            }

            // 啟動停止等待動畫的計時器
            timerStopWaitAni.Enabled = true;
            saveDataGridOriginalData();
        }

        private void saveDataGridOriginalData()
        {
            originalData.Clear();
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    originalData[(cell.RowIndex, cell.ColumnIndex)] = cell.Value;
                }
            }
            updateChgList();
        }

        private void updateChgList()
        {
            changes.RemoveAll(x => true);
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.RowIndex < 0 || cell.ColumnIndex < 0 || cell.RowIndex >= dataGridUsers.Rows.Count || cell.ColumnIndex >= dataGridUsers.Columns.Count)
                    {
                        continue;
                    }
                    if (!originalData.ContainsKey((cell.RowIndex, cell.ColumnIndex)))
                    {
                        originalData[(cell.RowIndex, cell.ColumnIndex)] = cell.Value;
                    }
                    var originalValue = originalData[(cell.RowIndex, cell.ColumnIndex)];
                    var newValue = cell.Value;
                    bool isBothNullOrEmpty = (originalValue == null || string.IsNullOrEmpty(originalValue.ToString())) &&
                                     (newValue == null || string.IsNullOrEmpty(newValue.ToString()));
                    if (!isBothNullOrEmpty && !Equals(originalValue, newValue))
                    {
                        changes.Add(new DataGridChange
                        {
                            OriginalValue = originalValue,
                            NewValue = newValue,
                            RowIndex = cell.RowIndex,
                            ColumnIndex = cell.ColumnIndex,
                            RowFirstColumnValue = (string)row.Cells[0].Value,
                            Title = dataGridUsers.Columns[cell.ColumnIndex].HeaderText
                        });
                    }
                }
            }
            if (confirmWindow == null)
            {
                return;
            }
            changesDO = false;
            run();
        }

        private string viewTaskStrConv(string info)
        {
            if (info == null || info.Length == 0)
            {
                return "（空）";
            }
            else if (info == "True")
            {
                return "是";
            }
            else if (info == "False")
            {
                return "否";
            }
            return info;
        }

        /// <summary>
        /// 獲取管理員許可權。
        /// 如果成功獲取管理員許可權，則設定定時器停止等待動畫的間隔並更新模式；否則停止等待動畫。
        /// </summary>
        private void getAdmin()
        {
            // 嘗試以管理員許可權重新啟動程式。
            bool toAdmin = UAC.RestartAsAdministrator();

            // 在主執行緒上執行以下操作。
            this.Invoke((Action)(() =>
            {
                if (toAdmin)
                {
                    // 如果成功獲取管理員許可權，設定定時器的間隔為 3000 毫秒，並將模式設定為 1（成功）。
                    timerStopWaitAni.Interval = 3000;
                    timerStopWaitAniEndMode = 1;
                }
                else
                {
                    // 如果獲取管理員許可權失敗，設定模式為 0（失敗），並停止等待動畫。
                    timerStopWaitAniEndMode = 0;
                    waitAni(false);
                }
            }));
        }

        /// <summary>
        /// 調整 PictureBox 的位置和尺寸。
        /// 根據窗體大小動態調整 PictureBox 和 DataGrid 的顯示狀態。
        /// </summary>
        /// <param name="loadImg">是否載入圖片，預設值為 true。</param>
        private void AdjustPictureBox(bool loadImg = true)
        {
            // 獲取窗體客戶區的高度和寬度
            int clientHeight = this.ClientSize.Height;
            int clientWidth = this.ClientSize.Width;

            // 設定 PictureBox 的高度和寬度與客戶區高度相同
            pictureBoxBG.Height = clientHeight;
            pictureBoxBG.Width = clientHeight;

            // 設定 PictureBox 的左邊距，使其在客戶區右側顯示
            pictureBoxBG.Left = this.ClientSize.Width - pictureBoxBG.Width;

            // 如果窗體寬度小於預設寬度（WidthMoe）
            if (this.Width < WidthMoe)
            {
                // DataGrid 的寬度佔滿客戶區
                dataGridUsers.Width = clientWidth;

                // 如果 PictureBox 中有影像，清除影像並隱藏 PictureBox
                if (pictureBoxBG.Image != null)
                {
                    pictureBoxBG.Image = null;
                    pictureBoxBG.Visible = false;
                }
            }
            else
            {
                // DataGrid 的寬度為客戶區寬度的 80%
                dataGridUsers.Width = (int)(clientWidth * 0.8);

                // 如果需要載入圖片且 PictureBox 中沒有影像
                if (loadImg && pictureBoxBG.Image == null)
                {
                    // 載入預設圖片並顯示 PictureBox
                    pictureBoxBG.Image = Properties.Resources.app1;
                    pictureBoxBG.Visible = true;
                }
            }
        }

        public void StartButtonClicked()
        {
            MessageBox.Show("TODO: 执行任务");
        }

        private void run()
        {
            // 先改 List<DataGridChange> changes 和 bool changesDO 再 run()
            Thread thread = new Thread(runT);
            thread.IsBackground = true;
            thread.Start();
        }

        private void runT()
        {
            this.Invoke((Action)(() =>
            {
                confirmWindow.listBoxTasks.Items.Clear();
            }));
            bool hasChPwd = false;
            for (int i = 0; i < changes.Count; i++)
            {
                DataGridChange change = changes[i];
                string originalValue = viewTaskStrConv(change.OriginalValue.ToString());
                string newValue = viewTaskStrConv(change.NewValue.ToString());
                string info = "";
                string ii = (i + 1).ToString();
                if (change.ColumnIndex == 1)
                {
                    info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的全名从 \"{originalValue}\" 改为 \"{newValue}\"";
                }
                else if (change.ColumnIndex == 2)
                {
                    info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的描述从 \"{originalValue}\" 改为 \"{newValue}\"";
                }
                else if (change.ColumnIndex == 4)
                {
                    //string passwd = "";
                    //for (int j = 0; j < newValue.Length; j++)
                    //{
                    //    passwd += "*";
                    //}
                    info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码重置为 \"{newValue}\"";
                    hasChPwd = true;
                }
                else if (change.ColumnIndex == 5)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 禁用用户 \"{change.RowFirstColumnValue}\"";
                    }
                    else
                    {
                        info = $"{ii}. 启用用户 \"{change.RowFirstColumnValue}\"";
                    }
                }
                else if (change.ColumnIndex == 6)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为永不过期";
                    }
                    else
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为需要定期更改";
                    }
                }
                else if (change.ColumnIndex == 7)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为可以由用户更改";
                    }
                    else
                    {
                        info = $"{i + 1}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为不允许用户更改";
                    }
                }
                else if (change.ColumnIndex >= defaultDataGridUsersColumnsCount)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 从用户组 \"{change.Title}\" 中移除";
                    }
                    else
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 添加到用户组 \"{change.Title}\"";
                    }
                }
                else
                {
                    //info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的 \"{change.Title}\" 从 \"{originalValue}\" 改为 \"{newValue}\"";
                    continue;
                }
                this.Invoke((Action)(() =>
                {
                    confirmWindow.listBoxTasks.Items.Add(info);
                }));
            }
            this.Invoke((Action)(() =>
            {
                confirmWindow.toolStripLabelStatus.Text = $"{confirmWindow.listBoxTasks.Items.Count} 个修改项。";
                if (hasChPwd)
                {
                    confirmWindow.toolStripLabelStatus.Text += "注意：直接修改密码会导致未备份私钥的 EFS 加密文件失效。";
                }
            }));
        }

        private void startEXE(string path, string arguments = "")
        {
            try
            {
                if (arguments.Length == 0)
                {
                    Process.Start(path);
                }
                else
                {
                    Process.Start(path, arguments);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "无法执行此命令", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string pwgen()
        {
            int pwdCount = int.Parse(toolStripComboBoxPwdCount.Text);
            string[] types = toolStripComboBoxPwdType.Text.Split(',');
            string pwdChars = "";
            foreach (string type in types)
            {
                switch (type)
                {
                    case "0-9":
                        for (int i = 0; i < 10; i++)
                        {
                            pwdChars += i.ToString();
                        }
                        break;
                    case "a-z":
                        for (int i = 0; i < 26; i++)
                        {
                            pwdChars += (char)('a' + i);
                        }
                        break;
                    case "A-Z":
                        for (int i = 0; i < 26; i++)
                        {
                            pwdChars += (char)('A' + i);
                        }
                        break;
                    case "sym":
                        pwdChars += "!#$%&'()*+,-./:;<=>?@[]^_`{|}~";
                        break;
                }
            }
            if (pwdChars.Length == 0)
            {
                MessageBox.Show("请选择密码类型！", "密码生成器", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            Random random = new Random();
            string pwd = "";
            for (int i = 0; i < pwdCount; i++)
            {
                pwd += pwdChars[random.Next(pwdChars.Length)];
            }
            //MessageBox.Show("生成的密码为：" + pwd, "密码生成器", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return pwd;
        }
    }
}
