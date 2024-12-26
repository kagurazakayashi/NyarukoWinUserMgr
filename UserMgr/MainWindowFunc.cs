using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
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
            if (enable)
            {
                if (slboot.aniFont != null)
                {
                    timerWaitAni.Enabled = true; // 啟用等待動畫計時器
                    //timerStopWaitAni.Enabled = true; // 啟用停止動畫計時器
                }
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
                    groupList[i] = iniconf.IniReadValue("Groups", "G" + i.ToString());
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
            string arguments = linkMachineName + " " + currentPosition.X.ToString() + "," + currentPosition.Y.ToString() + "," + currentSize.Width.ToString() + "," + currentSize.Height.ToString();
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
                    if (MessageBox.Show(ex.Message, programPath, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
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
                        Name = "Group" + i.ToString(), // 設定列名
                        Width = 80, // 設定列寬
                    };
                    dataGridUsers.Columns.Add(groupColumn);
                }
            }

            // 更新視窗標題為當前機器名
            Text = linkMachineName + " 的用户目录 - " + defaultTitle + version;

            // 遍歷使用者列表，填充使用者資訊到表格
            rowCodeAdding = true;
            loadCount = 0;
            foreach (UserInfoType user in userLoader.users)
            {
                // 準備填充表格的一行資料
                List<object> newRow = new List<object>();
                newRow.Add(user.Name);                       // 使用者名稱
                newRow.Add(user.FullName);                   // 全名
                newRow.Add(user.Description);                // 描述
                newRow.Add(user.AccountExpires);             // 賬戶過期時間
                newRow.Add("");                              // 空白列，用於僅填寫密碼
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
                loadCount++;
            }
            rowCodeAdding = false;

            foreach (var column in dataGridUsers.Columns)
            {
                // 設定所有列的排序模式為不可排序
                ((DataGridViewColumn)column).SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // 啟動停止等待動畫的計時器
            timerStopWaitAni.Enabled = true;
            saveDataGridOriginalData();
        }

        /// <summary>
        /// 儲存 DataGridView 的初始資料到 originalData 字典中。
        /// originalData 使用單元格的行索引和列索引作為鍵，單元格的值作為對應的值。
        /// </summary>
        private void saveDataGridOriginalData()
        {
            // 清空 originalData 字典，確保不包含舊資料。
            originalData.Clear();

            // 遍歷 dataGridUsers 的每一行。
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                // 遍歷當前行中的每一個單元格。
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // 將單元格的行索引和列索引作為鍵，單元格的值作為值，存入 originalData。
                    Coordinate coordinate = new Coordinate(cell.RowIndex, cell.ColumnIndex);
                    originalData[coordinate] = cell.Value;
                }
            }

            // 更新更改列表。
            updateChgList();
        }

        /// <summary>
        /// 更新更改列表，移除空行並獲取更改資料。
        /// 同時檢查確認視窗是否存在，必要時執行相應操作。
        /// </summary>
        private void updateChgList()
        {
            // 移除 DataGridView 中的空行。
            RemoveEmptyRows();

            // 使用 chUser 的 GetChangeList 方法對比當前資料和 originalData，生成更改列表。
            chUser.GetChangeList(dataGridUsers, originalData);

            // 如果確認視窗為空，則直接返回。
            if (confirmWindow == null)
            {
                return;
            }

            // 重置 changesDO 標誌為 false，表示沒有未確認的更改。
            changesDO = false;

            // 執行後續操作。
            run();
        }

        /// <summary>
        /// 從 DataGridView 中移除空行。
        /// 遍歷 DataGridView 的所有行，檢查第一列是否為空，若為空則移除該行。
        /// 確保不會刪除新行（IsNewRow）。
        /// </summary>
        private void RemoveEmptyRows()
        {
            // 遍歷 DataGridView 中的所有行
            for (int i = dataGridUsers.Rows.Count - 1; i >= 0; i--) // 從後向前刪除，避免索引問題
            {
                // 獲取當前行
                DataGridViewRow row = dataGridUsers.Rows[i];

                // 檢查第一列的單元格是否為空或僅包含空白字元
                if (row.Cells[0].Value == null || string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                {
                    // 如果當前行不是新行，則刪除
                    if (!row.IsNewRow) // 確保不刪除新行
                    {
                        dataGridUsers.Rows.RemoveAt(i); // 刪除當前行
                    }
                }
            }
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

        /// <summary>
        /// 當用戶點選“開始”按鈕時觸發的事件處理程式。
        /// 負責顯示載入動畫、載入配置並啟動後臺執行緒以處理資料載入邏輯。
        /// </summary>
        public void StartButtonClicked()
        {
            // 顯示載入動畫
            waitAni(true);
            loadConfig(); // 載入配置

            // 設定使用者列預設顯示數量和域名
            chUser.defaultDataGridUsersColumnsCount = defaultDataGridUsersColumnsCount;
            chUser.doDomain = linkMachineName;
            confirmWindow.toolStripProgressBar1.Value = 0;

            // 建立並啟動後臺執行緒執行資料更改邏輯
            Thread thread = new Thread(changeNow);
            thread.IsBackground = true; // 設定執行緒為後臺執行緒
            thread.Start(); // 啟動執行緒
        }

        /// <summary>
        /// 當用戶點選“取消”按鈕時觸發的事件處理程式。
        /// 負責重新載入資料。
        /// </summary>
        public void CancelButtonClicked()
        {
            reloadData();
        }

        /// <summary>
        /// 負責處理資料更改的邏輯，包括更新介面和顯示更改項的狀態。
        /// 在後臺執行緒中執行。
        /// </summary>
        private void changeNow()
        {
            // 使用 UI 執行緒清空任務列表
            this.Invoke((Action)(() =>
            {
                confirmWindow.listBoxTasks.Items.Clear();
            }));

            // 獲取更改列表並更新任務列表
            string[] markedRows = chUser.ShowChangeList();
            this.Invoke((Action)(() =>
            {
                for (int i = 0; i < markedRows.Length; i++)
                {
                    if (!confirmWindow.listBoxTasks.Items.Contains(markedRows[i]) )
                    {
                        confirmWindow.listBoxTasks.Items.Add(markedRows[i]);
                    }
                }

                // 更新狀態標籤和工具欄樣式
                confirmWindow.toolStripLabelStatus.Text = "队列完成，已尝试 " + confirmWindow.listBoxTasks.Items.Count.ToString() + " 个修改项。";
                confirmWindow.toolStripButtonClose.Visible = true;
                confirmWindow.toolStrip1.BackColor = Color.Pink;
            }));
        }

        /// <summary>
        /// 執行資料更改邏輯，包括清空任務列表和顯示更改項狀態。
        /// </summary>
        private void run()
        {
            // 確保介面清理後執行更改邏輯
            try
            {
                confirmWindow.listBoxTasks.Items.Clear();
                confirmWindow.toolStripProgressBar1.Value = 0;
            }
            catch (Exception)
            {
                return; // 如果發生異常則直接返回
            }

            // 設定使用者列預設顯示數量和域名
            chUser.defaultDataGridUsersColumnsCount = defaultDataGridUsersColumnsCount;
            chUser.doDomain = "";

            // 獲取更改列表並更新任務列表
            string[] markedRows = chUser.ShowChangeList();
            confirmWindow.listBoxTasks.Items.Clear();
            for (int i = 0; i < markedRows.Length; i++)
            {
                // 去重
                if (!confirmWindow.listBoxTasks.Items.Contains(markedRows[i]) )
                {
                    confirmWindow.listBoxTasks.Items.Add(markedRows[i]);
                }
            }

            confirmWindow.toolStripProgressBar1.Value = confirmWindow.toolStripProgressBar1.Maximum;

            // 更新狀態標籤
            confirmWindow.toolStripLabelStatus.Text = confirmWindow.listBoxTasks.Items.Count.ToString() + " 个修改项。";
            if (chUser.hasChPwd)
            {
                confirmWindow.toolStripLabelStatus.Text += "注意：直接修改密码会导致未备份私钥的 EFS 加密文件失效。";
            }
        }

        /// <summary>
        /// 啟動一個外部可執行檔案。
        /// </summary>
        /// <param name="path">可執行檔案的路徑。</param>
        /// <param name="arguments">可選引數，用於傳遞給可執行檔案。</param>
        private void startEXE(string path, string arguments = "")
        {
            try
            {
                // 根據是否提供引數啟動外部程式
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
                // 捕獲異常並顯示錯誤訊息
                MessageBox.Show(ee.Message, "无法执行此命令", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 生成隨機密碼的方法。
        /// </summary>
        /// <returns>生成的密碼字串。如果未選擇密碼型別，則返回空字串。</returns>
        private string pwgen()
        {
            // 從工具欄的密碼長度選擇框中獲取密碼長度，並將其解析為整數。
            int pwdCount = int.Parse(toolStripComboBoxPwdCount.Text);

            // 從工具欄的密碼型別選擇框中獲取選中的型別，並以逗號分隔存入陣列。
            string[] types = toolStripComboBoxPwdType.Text.Split(',');

            // 初始化儲存所有可用字元的字串。
            string pwdChars = "";

            // 遍歷每個選中的密碼型別，根據型別新增相應的字元到 pwdChars。
            foreach (string type in types)
            {
                switch (type)
                {
                    case "0-9":
                        // 新增數字字元 '0' 到 '9'。
                        for (int i = 0; i < 10; i++)
                        {
                            pwdChars += i.ToString();
                        }
                        break;
                    case "a-z":
                        // 新增小寫字母 'a' 到 'z'。
                        for (int i = 0; i < 26; i++)
                        {
                            pwdChars += (char)('a' + i);
                        }
                        break;
                    case "A-Z":
                        // 新增大寫字母 'A' 到 'Z'。
                        for (int i = 0; i < 26; i++)
                        {
                            pwdChars += (char)('A' + i);
                        }
                        break;
                    case "sym":
                        // 新增常用的特殊符號。
                        pwdChars += "!#$%&'()*+,-./:;<=>?@[]^_`{|}~";
                        break;
                }
            }

            // 如果未選擇任何密碼型別，則彈出提示框並返回空字串。
            if (pwdChars.Length == 0)
            {
                MessageBox.Show("请选择密码类型！", "密码生成器", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            // 建立隨機數生成器。
            Random random = new Random();

            // 初始化用於儲存最終生成密碼的字串。
            string pwd = "";

            // 根據指定的密碼長度，隨機從 pwdChars 中選擇字元，拼接到 pwd 中。
            for (int i = 0; i < pwdCount; i++)
            {
                pwd += pwdChars[random.Next(pwdChars.Length)];
            }

            // 返回生成的密碼字串。
            return pwd;
        }

        /// <summary>
        /// 處理 DataGridView 行的標記操作。
        /// 根據行的背景色，標記或取消標記行，並設定只讀狀態。
        /// </summary>
        /// <param name="row">要處理的 DataGridViewRow 物件。</param>
        private void HandleRowMarking(DataGridViewRow row)
        {
            // 檢查行是否已被標記為刪除（透過背景色判斷）
            if (row.DefaultCellStyle.BackColor == addDelColor[1])
            {
                // 恢復預設背景色
                row.DefaultCellStyle.BackColor = Color.Empty;
                // 取消只讀狀態
                row.ReadOnly = false;
            }
            else
            {
                // 將行標記為刪除（背景色設定為紅色）
                row.DefaultCellStyle.BackColor = addDelColor[1];
                // 設定為只讀狀態
                row.ReadOnly = true;
            }
            // 取消當前行的選中狀態
            dataGridUsers.ClearSelection();
        }

        /// <summary>
        /// 確認是否刪除指定使用者。
        /// 彈出確認對話方塊詢問使用者是否放棄建立新使用者。
        /// </summary>
        /// <param name="user">要刪除的使用者名稱。</param>
        /// <returns>如果使用者選擇“是”，返回 true；否則返回 false。</returns>
        private bool confirmDelete(string user)
        {
            return MessageBox.Show("放弃新建用户 \"" + user + "\" 吗？", "删除用户", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// 開啟指定的 URL。
        /// 如果無法透過預設方法開啟，嘗試使用系統資源管理器開啟。
        /// </summary>
        /// <param name="url">要開啟的 URL 地址。</param>
        private void openURL(string url)
        {
            try
            {
                // 使用預設的外部程式開啟 URL
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // 使用外部關聯程式開啟
                });
            }
            catch (Exception)
            {
                // 如果預設方法失敗，使用系統資源管理器開啟
                startEXE("explorer", url);
            }
        }
    }
}
