using System;
using System.Windows.Forms;
using UserInfo;
using System.Threading;
using SystemRes;

namespace winusermgr
{
    public partial class MainWindow : Form
    {
        private UserLoader userLoader = new UserLoader();
        private SlbootAni slboot = new SlbootAni();
        private int timerStopWaitAniEndMode = 0;
        private string linkMachineName = Environment.MachineName;
        private const int WidthMoe = 1300;
        private const int WidthNormal = 800;
        private DraggingOpacity draggingOpacity;

        /// <summary>
        /// 表示主窗體的建構函式。
        /// 初始化窗體元件並根據螢幕寬度調整窗體寬度。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent(); // 初始化窗體元件
            draggingOpacity = new DraggingOpacity(this);

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
            AdjustPictureBox(true); // 調整圖片框的顯示狀態

            // 檢查當前執行程式是否具有管理員許可權
            bool isAdmin = UAC.IsRunAsAdministrator();
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
            // 建立 FormGroupSelect 物件，用於選擇使用者組。
            // 引數 linkMachineName 表示關聯的機器名稱。
            FormGroupSelect formGroupSelect = new FormGroupSelect(linkMachineName);

            // 除錯時可使用以下程式碼：透過工具條組合框的文字設定使用者組選擇視窗。
            // FormGroupSelect formGroupSelect = new FormGroupSelect(toolStripComboBoxMachine.Text); //DEBUG

            // 如果使用者組選擇視窗的對話方塊返回結果為“確定”，重新載入資料。
            if (formGroupSelect.ShowDialog() == DialogResult.OK)
            {
                reloadData();
            }
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
    }
}
