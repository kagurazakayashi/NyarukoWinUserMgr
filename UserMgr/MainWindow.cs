using System;
using System.Windows.Forms;
using UserInfo;
using System.Threading;
using SystemRes;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace WinUserMgr
{
    public partial class MainWindow : Form
    {
        private static string gitURL = "https://github.com/kagurazakayashi/winusermgr";
        private static string defaultTitle = "Nyaruko Windows User Manager";
        private string version = " v";
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
        private Dictionary<(int row, int col), object> originalData = new Dictionary<(int row, int col), object>();
        private static Color[] addDelColor = new Color[] { Color.LightGreen, Color.LightCoral };
        private ChangeUser chUser = new ChangeUser(addDelColor);
        bool changesDO = false;
        bool isAdmin = false;
        bool rowCodeAdding = false;
        int loadCount = 0;

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
            Assembly assembly = Assembly.GetExecutingAssembly();
            version += assembly.GetName().Version.ToString();
            string[] versionParts = version.ToString().Split('.');
            version = string.Join(".", versionParts.Take(versionParts.Length - 1));
            Text = defaultTitle + version;
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
            menuStrip.Enabled = false;
            menuStrip.UseWaitCursor = true;
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
            timerStopWaitAni.Enabled = true;

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

        /// <summary>
        /// 處理工具欄按鈕點選事件以生成密碼並設定到當前單元格。
        /// </summary>
        /// <param name="sender">觸發事件的物件。</param>
        /// <param name="e">包含事件資料的引數。</param>
        private void toolStripButtonPWGen_Click(object sender, EventArgs e)
        {
            // 呼叫密碼生成函式，返回生成的密碼。
            string pw = pwgen();

            // 檢查密碼長度是否大於0，以及當前選定的單元格是否不為空。
            if (pw.Length > 0 && dataGridUsers.CurrentCell != null)
            {
                // 將生成的密碼設定為當前選定單元格的值。
                dataGridUsers.CurrentCell.Value = pw;
            }
        }

        /// <summary>
        /// 處理 DataGrid 中當前單元格更改的事件。
        /// 根據當前單元格的位置和內容調整一些功能，例如工具按鈕的啟用狀態和列的只讀屬性。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void dataGridUsers_CurrentCellChanged(object sender, EventArgs e)
        {
            // 如果當前單元格為空，則直接返回。
            if (dataGridUsers.CurrentCell == null)
            {
                return;
            }

            // 獲取當前單元格的列索引。
            int columnIndex = dataGridUsers.CurrentCell.ColumnIndex;
            // 獲取當前單元格的行索引。
            int rowIndex = dataGridUsers.CurrentCell.RowIndex;

            // 根據列索引控制工具按鈕 "toolStripButtonPWGen" 是否啟用。
            if (!dataGridUsers.ReadOnly)
            {
                // 只有噹前列為第 4 列時，啟用密碼生成按鈕。
                toolStripButtonPWGen.Enabled = columnIndex == 4;
            }

            // 如果當前單元格位於第 0 列，需要檢查單元格內容以設定該列的只讀屬性。
            if (columnIndex == 0)
            {
                // 如果當前單元格的值為空或長度為 0。
                if (dataGridUsers.CurrentCell.Value == null || dataGridUsers.CurrentCell.Value.ToString().Length == 0)
                {
                    // 設定第 0 列為可編輯。
                    dataGridUsers.Columns[0].ReadOnly = false;
                }
                else
                {
                    // 如果當前行是新行，設定第 0 列為可編輯，否則設為只讀。
                    if (dataGridUsers.Rows[rowIndex].IsNewRow)
                    {
                        dataGridUsers.Columns[0].ReadOnly = false;
                    }
                    else
                    {
                        dataGridUsers.Columns[0].ReadOnly = true;
                    }
                }
            }
        }

        /// <summary>
        /// 點選工具欄中的 OK 按鈕時觸發的事件處理方法。
        /// 如果確認視窗未開啟，則建立並顯示確認視窗。
        /// 如果確認視窗已開啟，則關閉確認視窗。
        /// 同時更新工具欄按鈕的選中狀態。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void toolStripButtonOK_Click(object sender, EventArgs e)
        {
            // 檢查確認視窗是否已存在
            if (confirmWindow == null)
            {
                // 建立新的確認視窗例項
                confirmWindow = new ConfirmWindow();

                // 給使用者修改類列表控制元件，供其新增即時資訊
                chUser.parentWindow = confirmWindow;

                // 設定確認視窗中工具欄的背景顏色
                confirmWindow.toolStrip1.BackColor = Color.SkyBlue;

                // 訂閱確認視窗的關閉事件
                confirmWindow.FormClosed += ConfirmWindow_FormClosed;

                // 顯示確認視窗
                confirmWindow.Show();

                // 訂閱確認視窗中開始按鈕點選事件
                confirmWindow.StartButtonClicked += StartButtonClicked;

                // 訂閱確認視窗中取消按鈕點選事件
                confirmWindow.CancelButtonClicked += CancelButtonClicked;

                // 設定工具欄 OK 按鈕為選中狀態
                toolStripButtonOK.Checked = true;
            }
            else
            {
                // 如果確認視窗已存在，關閉確認視窗
                confirmWindow.Close();

                // 釋放確認視窗物件
                confirmWindow = null;
                chUser.parentWindow = null;

                // 設定工具欄 OK 按鈕為未選中狀態
                toolStripButtonOK.Checked = false;
            }

            // 更新變更列表
            updateChgList();
        }

        /// <summary>
        /// 確認視窗關閉時觸發的事件處理方法。
        /// 釋放確認視窗物件，並更新工具欄 OK 按鈕的狀態。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void ConfirmWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 確認視窗關閉時釋放物件
            confirmWindow = null;
            chUser.parentWindow = null;

            // 設定工具欄 OK 按鈕為未選中狀態
            toolStripButtonOK.Checked = false;
        }

        /// <summary>
        /// 資料網格中某個單元格的值發生更改時觸發的事件處理方法。
        /// 更新變更列表。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數，包含更改的單元格的行列索引。</param>
        private void dataGridUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 更新變更列表
            updateChgList();
        }

        /// <summary>
        /// 開啟經典的使用者賬戶管理介面。
        /// </summary>
        /// <param name="sender">事件觸發的物件。</param>
        /// <param name="e">事件引數。</param>
        private void ToolStripMenuItemOpenAccL_Click(object sender, EventArgs e)
        {
            // 呼叫方法啟動程式“control userpasswords2”，
            // 該命令用於開啟傳統的使用者賬戶管理介面。
            startEXE("control", "userpasswords2");
        }

        /// <summary>
        /// 開啟現代設定中的其他使用者管理介面。
        /// </summary>
        /// <param name="sender">事件觸發的物件。</param>
        /// <param name="e">事件引數。</param>
        private void ToolStripMenuItemOpenAccM_Click(object sender, EventArgs e)
        {
            // 呼叫方法啟動程式“ms-settings:otherusers”，
            // 該命令用於開啟系統設定中的“其他使用者”頁面。
            startEXE("ms-settings:otherusers");
        }

        /// <summary>
        /// 開啟本地使用者和組管理工具。
        /// </summary>
        /// <param name="sender">事件觸發的物件。</param>
        /// <param name="e">事件引數。</param>
        private void ToolStripMenuItemOpenAccC_Click(object sender, EventArgs e)
        {
            // 呼叫方法啟動程式“lusrmgr.msc”，
            // 該命令用於開啟本地使用者和組管理控制檯。
            startEXE("lusrmgr.msc");
        }

        /// <summary>
        /// 工具欄按鈕 "PWGenC" 的點選事件處理程式，用於生成密碼並將其複製到剪貼簿。
        /// </summary>
        /// <param name="sender">事件的傳送者。</param>
        /// <param name="e">包含事件資料的 EventArgs 物件。</param>
        private void toolStripButtonPWGenC_Click(object sender, EventArgs e)
        {
            // 呼叫密碼生成方法
            string pw = pwgen();

            // 檢查生成的密碼長度是否大於0
            if (pw.Length > 0)
            {
                try
                {
                    // 嘗試將生成的密碼複製到剪貼簿
                    Clipboard.SetText(pw);
                }
                catch (Exception ex)
                {
                    // 捕獲異常並顯示錯誤訊息
                    MessageBox.Show(ex.Message, "剪贴板操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 工具欄按鈕 "UDIDGen" 的點選事件處理程式，用於生成一個新的 GUID 並將其複製到剪貼簿。
        /// </summary>
        /// <param name="sender">事件的傳送者。</param>
        /// <param name="e">包含事件資料的 EventArgs 物件。</param>
        private void toolStripButtonUDIDGen_Click(object sender, EventArgs e)
        {
            try
            {
                // 嘗試生成新的 GUID 並將其複製到剪貼簿
                Clipboard.SetText(Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                // 捕獲異常並顯示錯誤訊息
                MessageBox.Show(ex.Message, "生成失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 處理使用者試圖刪除 DataGridView 行時的事件。
        /// </summary>
        /// <param name="sender">事件的觸發者物件。</param>
        /// <param name="e">包含行刪除相關資訊的事件引數。</param>
        private void dataGridUsers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // 獲取當前被刪除的行
            DataGridViewRow currentRow = e.Row;

            // 獲取當前行的第一列的值
            string firstCell = currentRow.Cells[0].Value?.ToString();

            // 如果當前行的背景顏色為綠色（即允許刪除的顏色）
            if (currentRow.DefaultCellStyle.BackColor == addDelColor[0])
            {
                // 彈出確認刪除對話方塊，使用者選擇取消則不刪除
                e.Cancel = !confirmDelete(firstCell);
                return; // 允許刪除
            }

            // 如果不是允許刪除的行，則執行標記邏輯
            HandleRowMarking(currentRow);

            // 取消刪除操作
            e.Cancel = true;
        }

        /// <summary>
        /// 處理 DataGridView 按鍵事件，例如按下 Delete 鍵時的邏輯。
        /// </summary>
        /// <param name="sender">事件的觸發者物件。</param>
        /// <param name="e">包含按鍵相關資訊的事件引數。</param>
        private void dataGridUsers_KeyDown(object sender, KeyEventArgs e)
        {
            // 獲取當前選中的行
            DataGridViewRow currentRow = dataGridUsers.CurrentRow;

            // 獲取當前行的第一列的值
            string firstCell = currentRow.Cells[0].Value?.ToString();

            // 如果當前行的背景顏色為綠色（即允許刪除的顏色）
            if (currentRow.DefaultCellStyle.BackColor == addDelColor[0])
            {
                // 彈出確認刪除對話方塊，使用者選擇確認則刪除行
                if (confirmDelete(firstCell))
                {
                    dataGridUsers.Rows.Remove(currentRow);
                }
                return;
            }

            // 如果按下的是 Delete 鍵，且當前行不為空
            if (e.KeyCode == Keys.Delete && currentRow != null)
            {
                // 對當前行進行標記
                HandleRowMarking(currentRow);

                // 標記事件為已處理，防止其他預設刪除操作
                e.Handled = true;
            }

            // 更新更改列表
            updateChgList();
        }

        /// <summary>
        /// 處理 DataGridView 使用者單元格編輯結束的事件。
        /// </summary>
        /// <param name="sender">事件的發起者物件。</param>
        /// <param name="e">包含與單元格相關的資料，例如行和列的索引。</param>
        private void dataGridUsers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 獲取當前編輯的行
            DataGridViewRow row = dataGridUsers.Rows[e.RowIndex];
            // 檢查當前行是否包含非空資料
            bool hasData = false;
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    hasData = true;
                    break;
                }
            }
            // 檢查當前行的索引是否小於載入的記錄數量（表示是否為已有資料）
            bool hasOriginalData = e.RowIndex < loadCount;
            // 如果當前行有資料且尚未標記顏色，並且不是原始資料，則將行背景色標記為綠色
            if (hasData && row.DefaultCellStyle.BackColor != addDelColor[0] && !hasOriginalData)
            {
                row.DefaultCellStyle.BackColor = addDelColor[0];
            }
            // 更新更改列表
            updateChgList();
            // 啟用選單項
            menuStrip.Enabled = true;
        }

        /// <summary>
        /// 處理 DataGridView 使用者刪除行後的事件。
        /// </summary>
        /// <param name="sender">事件的發起者物件。</param>
        /// <param name="e">包含與被刪除行相關的資料。</param>
        private void dataGridUsers_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            // 更新更改列表
            updateChgList();
        }

        /// <summary>
        /// 處理 DataGridView 的 CurrentCellDirtyStateChanged 事件。
        /// 噹噹前單元格是複選框單元格且處於髒狀態時，提交編輯操作。
        /// </summary>
        /// <param name="sender">事件的傳送者，一般是 DataGridView。</param>
        /// <param name="e">事件引數。</param>
        private void dataGridUsers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // 判斷當前單元格是否為 DataGridViewCheckBoxCell 且處於髒狀態。
            if (dataGridUsers.CurrentCell is DataGridViewCheckBoxCell && dataGridUsers.IsCurrentCellDirty)
            {
                // 提交當前單元格的編輯操作，以便立即觸發後續事件。
                dataGridUsers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// 處理 DataGridView 使用者單元格驗證的事件。
        /// 用於驗證輸入的使用者名稱是否重複。
        /// </summary>
        /// <param name="sender">事件的發起者物件。</param>
        /// <param name="e">包含與驗證相關的資料，例如列和行的索引及使用者輸入的值。</param>
        private void dataGridUsers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // 檢查是否是第一列（索引為 0）
            if (e.ColumnIndex == 0)
            {
                string newValue = e.FormattedValue.ToString();
                // 遍歷 DataGridView 檢查是否有重複的使用者名稱
                foreach (DataGridViewRow row in dataGridUsers.Rows)
                {
                    if (row.Index != e.RowIndex && row.Cells[0].Value != null)
                    {
                        string existingValue = row.Cells[0].Value.ToString();
                        if (existingValue == newValue)
                        {
                            // 彈出提示框告知使用者輸入的使用者名稱重複
                            MessageBox.Show("这个用户名已经存在了，请取个别的用户名吧。", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true; // 取消輸入
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 處理 DEBUG 選單項點選事件，用於切換 DataGridView 到可編輯模式。
        /// </summary>
        /// <param name="sender">事件的發起者物件。</param>
        /// <param name="e">事件資料。</param>
        private void dEBUGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 將 DataGridView 設定為可編輯模式
            dataGridUsers.ReadOnly = false;
            dataGridUsers.AllowUserToAddRows = true;
            dataGridUsers.AllowUserToDeleteRows = true;
            // 停用 DEBUG 選單項
            dEBUGToolStripMenuItem.Enabled = false;
            dEBUGToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// 點選事件處理方法：開啟專案的 README 文件。
        /// </summary>
        /// <param name="sender">觸發事件的控制元件。</param>
        /// <param name="e">事件引數。</param>
        private void helpREADME_Click(object sender, EventArgs e)
        {
            // 開啟指定 URL，指向 README.md 檔案
            openURL(gitURL + "/blob/main/README.md");
        }

        /// <summary>
        /// 點選事件處理方法：開啟專案的提交歷史頁面。
        /// </summary>
        /// <param name="sender">觸發事件的控制元件。</param>
        /// <param name="e">事件引數。</param>
        private void helpCommits_Click(object sender, EventArgs e)
        {
            // 開啟指定 URL，指向提交歷史頁面
            openURL(gitURL + "/commits/main/");
        }

        /// <summary>
        /// 點選事件處理方法：開啟專案的問題跟蹤頁面。
        /// </summary>
        /// <param name="sender">觸發事件的控制元件。</param>
        /// <param name="e">事件引數。</param>
        private void helpIssues_Click(object sender, EventArgs e)
        {
            // 開啟指定 URL，指向問題跟蹤頁面
            openURL(gitURL + "/issues");
        }

        /// <summary>
        /// 點選事件處理方法：開啟專案的釋出版本頁面。
        /// </summary>
        /// <param name="sender">觸發事件的控制元件。</param>
        /// <param name="e">事件引數。</param>
        private void helpReleases_Click(object sender, EventArgs e)
        {
            // 開啟指定 URL，指向釋出版本頁面
            openURL(gitURL + "/releases");
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            Opacity = 1;
        }
    }
}
