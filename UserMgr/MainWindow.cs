using System;
using System.Windows.Forms;
using UserInfo;
using System.Threading;
using SystemRes;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace WinUserMgr
{
    public partial class MainWindow : Form
    {
        private UserLoader userLoader = new UserLoader();
        private SlbootAni slboot = new SlbootAni();
        private INI iniconf;
        private int timerStopWaitAniEndMode = 0;
        private string linkMachineName = "";
        private const int WidthMoe = 1300;
        private const int WidthNormal = 800;
        private DraggingOpacity draggingOpacity;
        private string[] groupList = new string[0];
        private int defaultDataGridUsersColumnsCount = 0;
        private ConfirmWindow confirmWindow;
        private bool confirmWindowOpened = false;
        private Dictionary<(int row, int col), object> originalData = new Dictionary<(int row, int col), object>();
        private List<DataGridChange> changes = new List<DataGridChange>();
        bool changesDO = false;
        bool isAdmin = false;

        /// <summary>
        /// 表示主窗體的建構函式。
        /// 初始化窗體元件並根據螢幕寬度調整窗體寬度。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent(); // 初始化窗體元件
            draggingOpacity = new DraggingOpacity(this);
            iniconf = new INI(Application.StartupPath + "\\config.ini");

            // 根據螢幕寬度調整窗體的寬度
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            if (screenWidth < WidthMoe)
            {
                this.Width = WidthNormal; // 設定為正常寬度
            }
        }

        /// <summary>
        /// 覆寫的 WndProc 方法，用於處理 Windows 訊息。
        /// 此方法專注於處理視窗拖動和狀態變更時的透明度。
        /// </summary>
        /// <param name="m">參考型別的 Message 結構，表示 Windows 訊息。</param>
        protected override void WndProc(ref Message m)
        {
            draggingOpacity.wndProc(m);
            // 呼叫基類的 WndProc 方法以處理其餘的訊息。
            base.WndProc(ref m);
        }

        /// <summary>
        /// 窗體載入事件處理函式。
        /// 初始化控制元件狀態、檢查管理員許可權並載入資料。
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            linkMachineName = Environment.MachineName;
            AdjustPictureBox(true); // 調整圖片框的顯示狀態
            for (int i = 4; i <= 127; i++)
            {
                toolStripComboBoxPwdCount.Items.Add(i);
            }
            toolStripComboBoxPwdCount.SelectedIndex = 16 - int.Parse(toolStripComboBoxPwdCount.Items[0].ToString());
            toolStripComboBoxPwdType.SelectedIndex = 1;

            // 檢查當前執行程式是否具有管理員許可權
            isAdmin = UAC.IsRunAsAdministrator();
            toolStripLockON.Visible = !isAdmin; // 如果不是管理員，顯示鎖定按鈕
            toolStripLockOFF.Visible = isAdmin; // 如果是管理員，顯示解鎖按鈕

            // 根據管理員許可權設定資料表格的可編輯性
            dataGridUsers.ReadOnly = !isAdmin;
            dataGridUsers.AllowUserToAddRows = isAdmin;
            dataGridUsers.AllowUserToDeleteRows = isAdmin;

            slboot.Init(20); // 初始化動畫字型元件
            if (slboot.aniFont != null)
            {
                labelWait.Font = slboot.aniFont; // 設定等待標籤的字型
            }

            // 初始化機器名稱選擇框
            toolStripComboBoxMachine.Items.Add(linkMachineName);
            toolStripComboBoxMachine.Text = linkMachineName;
            defaultDataGridUsersColumnsCount = dataGridUsers.Columns.Count;

            reloadData(); // 載入資料
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 響應工具條按鈕點選事件，重新載入使用者資料。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            // 獲取工具條中選擇的機器名稱
            linkMachineName = toolStripComboBoxMachine.Text;
            // 重新載入資料
            reloadData();
        }

        /// <summary>
        /// 等待動畫計時器的滴答事件處理。
        /// 更新等待動畫的顯示字元。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void timerWaitAni_Tick(object sender, EventArgs e)
        {
            // 更新等待動畫標籤中的字元
            labelWait.Text = slboot.UpdateChar();
        }

        /// <summary>
        /// 停止等待動畫計時器的滴答事件處理。
        /// 停止顯示等待動畫。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void timerStopWaitAni_Tick(object sender, EventArgs e)
        {
            // 停止等待動畫
            waitAni(false);
        }

        /// <summary>
        /// 處理點選工具條按鈕使用者組的事件。
        /// </summary>
        /// <param name="sender">事件的傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void toolStripButtonGroups_Click(object sender, EventArgs e)
        {
            toolStrip1.Enabled = false;
            toolStrip1.UseWaitCursor = true;
            dataGridUsers.ReadOnly = true;
            toolStripButtonGroups.Text = "请稍候...";
            timerOpenGroup.Enabled = true;
            ControlBox = false;
            Thread thread = new Thread(openGroupSelectThread);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 處理點選工具條按鈕“獲取管理員許可權”的事件。
        /// 開啟等待動畫並啟動後臺執行緒獲取管理員許可權。
        /// </summary>
        /// <param name="sender">事件的傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void toolStripLockON_Click(object sender, EventArgs e)
        {
            // 設定定時器停止等待動畫的模式為 -1（表示動畫未結束的狀態）。
            timerStopWaitAniEndMode = -1;

            // 啟動等待動畫。
            waitAni(true);

            // 建立一個後臺執行緒用於獲取管理員許可權。
            Thread thread = new Thread(getAdmin);
            thread.IsBackground = true; // 設定為後臺執行緒，以便主執行緒結束時該執行緒自動終止。
            thread.Start();
        }

        /// <summary>
        /// 窗體大小改變事件處理程式。
        /// 當窗體大小發生變化時，調整 PictureBox。
        /// </summary>
        /// <param name="sender">事件的傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            // 调整 PictureBox
            AdjustPictureBox();
            draggingOpacity.firstMove = true;
        }

        /// <summary>
        /// 處理主視窗移動事件的方法。
        /// 當主視窗移動時，通知透明度控制。
        /// </summary>
        /// <param name="sender">事件的傳送者（主視窗）</param>
        /// <param name="e">事件引數。</param>
        private void MainWindow_Move(object sender, EventArgs e)
        {
            draggingOpacity.winMove();
        }

        /// <summary>
        /// 窗体关闭事件处理程序。
        /// 释放 PictureBox 占用的资源。
        /// </summary>
        /// <param name="sender">事件的傳送者。</param>
        /// <param name="e">窗體關閉事件引數。</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 釋放 PictureBox 的資源並置空引用
            pictureBoxBG.Dispose();
            pictureBoxBG = null;
        }

        /// <summary>
        /// 按下工具條按鈕“選擇使用者組列”時，提示請稍候。
        /// </summary>
        /// <param name="sender">事件的傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void timerOpenGroup_Tick(object sender, EventArgs e)
        {
            toolStripButtonGroups.Text = "选择用户组列(&S)";
            timerOpenGroup.Enabled = false;
        }

        private void toolStripButtonPWGen_Click(object sender, EventArgs e)
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
                return;
            }
            Random random = new Random();
            string pwd = "";
            for (int i = 0; i < pwdCount; i++)
            {
                pwd += pwdChars[random.Next(pwdChars.Length)];
            }
            //MessageBox.Show("生成的密码为：" + pwd, "密码生成器", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridUsers.CurrentCell.Value = pwd;
        }

        private void dataGridUsers_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridUsers.CurrentCell == null)
            {
                return;
            }
            // 獲取當前單元格的列索引
            int columnIndex = dataGridUsers.CurrentCell.ColumnIndex;
            // 獲取當前單元格的行索引
            //int rowIndex = dataGridUsers.CurrentCell.RowIndex;
            //MessageBox.Show($"当前单元格位于：列 {columnIndex}, 行 {rowIndex}");
            if (!dataGridUsers.ReadOnly)
            {
                toolStripButtonPWGen.Enabled = columnIndex == 4;
            }
        }

        private void toolStripButtonOK_Click(object sender, EventArgs e)
        {
            if (confirmWindowOpened)
            {
                if (confirmWindow != null)
                {
                    confirmWindow.Close();
                    confirmWindow = null; // 释放资源
                }
                toolStripButtonOK.Checked = false;
            }
            else
            {
                confirmWindow = new ConfirmWindow();
                confirmWindow.FormClosed += ConfirmWindow_FormClosed;
                confirmWindow.Show();
                confirmWindow.StartButtonClicked += StartButtonClicked;
                toolStripButtonOK.Checked = true;
            }
            confirmWindowOpened = !confirmWindowOpened;
            updateChgList();
        }

        private void ConfirmWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            confirmWindowOpened = false;
            toolStripButtonOK.Checked = false;
        }

        private void dataGridUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            updateChgList();
        }
    }
}
