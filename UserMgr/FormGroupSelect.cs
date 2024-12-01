using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using SystemRes;

namespace winusermgr
{
    public partial class FormGroupSelect : Form
    {
        private Bitmap flippedBitmap;
        private string[] systemGroups;
        private INI iniconf;
        private string linkMachineName = Environment.MachineName;
        private string[] listBoxSystemGroupStartItems = null;
        private string[] listBoxSelectedGroupStartItems = null;
        private bool closeNow = false;
        private SlbootAni slboot = new SlbootAni();
        private int timerStopWaitAniEndMode = 0;
        private DraggingOpacity draggingOpacity;

        /// <summary>
        /// 表單群組選擇 (FormGroupSelect) 的建構函式。
        /// 此建構函式初始化表單元件，並根據提供的引數設定相關元件。
        /// </summary>
        /// <param name="linkMachineName">可選引數，鏈結的機器名稱。如果未提供，將使用預設值。</param>
        public FormGroupSelect(string linkMachineName = "")
        {
            // 初始化表單元件。
            InitializeComponent();
            draggingOpacity = new DraggingOpacity(this);

            // 如果提供了鏈結的機器名稱，並且名稱不是空白，則設定至類別屬性。
            if (!string.IsNullOrWhiteSpace(linkMachineName))
            {
                this.linkMachineName = linkMachineName;
            }

            // 初始化 INI 配置物件，讀取應用程式啟動路徑下的 config.ini 檔案。
            iniconf = new INI(Application.StartupPath + "\\config.ini");

            // 取得按鈕 "Add" 的影象，並翻轉以供 "Remove" 按鈕使用。
            // 由於程式碼的需求，目前部分原始程式碼被註解。

            // 使用原始 "Add" 按鈕的影像，建立翻轉後的影像物件。
            flippedBitmap = new Bitmap(buttonAdd.Image);

            // 將影像左右翻轉。
            flippedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);

            // 設定 "Remove" 按鈕的影像為翻轉後的影像。
            buttonRemove.Image = flippedBitmap;
            // buttonOK.Image = Shell32IconHelper.GetBitmapFromSysImageres(301);
            // cancelBitmap = Shell32IconHelper.GetBitmapFromSysImageres(131);
            // buttonCancel.Image = cancelBitmap;
            // Icon = Shell32IconHelper.GetIconFromSysImageres(111);
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
        /// 處理取消按鈕的點選事件。
        /// 将对话框结果设置为取消，并关闭当前窗口。
        /// </summary>
        /// <param name="sender">事件的发送者对象。</param>
        /// <param name="e">事件参数。</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // 设置对话框结果为取消
            DialogResult = DialogResult.Cancel;
            // 标记窗口需要立即关闭
            closeNow = true;
            // 关闭当前窗口
            Close();
        }

        /// <summary>
        /// 處理確定按鈕的點選事件。
        /// 執行虛擬組的檢查和儲存配置，並根據結果決定是否關閉視窗。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // 檢查是否有被移除的虛擬組
            string[] removedGroups = chkSystemGroup();
            if (removedGroups.Length > 0)
            {
                // 彈出確認對話方塊，提示使用者永久刪除這些虛擬組
                if (MessageBox.Show(
                        $"有已经被移除的虚拟组名。\n如果这些虚拟组名不包含在此电脑的用户组列表中，它将被永久删除！\n以下是要永久删除的组名:\n\n{string.Join("\n", removedGroups)}\n\n是否继续？",
                        $"永久删除 {removedGroups.Length} 个虚拟组名",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                {
                    // 如果使用者選擇取消，且事件為 FormClosingEventArgs，則停止關閉視窗
                    if (e is FormClosingEventArgs)
                    {
                        closeNow = false;
                        (e as FormClosingEventArgs).Cancel = true;
                    }
                    return;
                }
            }

            // 儲存配置並檢查儲存是否成功
            if (saveConfig())
            {
                // 設定對話方塊結果為確定
                DialogResult = DialogResult.OK;
                // 如果事件不是視口關閉事件，關閉當前視窗
                if (!(e is FormClosingEventArgs))
                {
                    closeNow = true;
                    Close();
                }
            }
        }

        /// <summary>
        /// 表單載入事件，用於初始化元件和啟動獲取使用者組的執行緒。
        /// </summary>
        /// <param name="sender">事件的觸發者</param>
        /// <param name="e">事件引數</param>
        private void FormGroupSelect_Load(object sender, EventArgs e)
        {
            // 初始化動畫設定，指定引數 20
            slboot.Init(20);

            // 如果動畫字型不為空，設定等待標籤的字型
            if (slboot.aniFont != null)
            {
                labelWait.Font = slboot.aniFont;
            }

            // 建立一個後臺執行緒，用於非同步獲取使用者組
            Thread thread = new Thread(getUserGroup);
            thread.IsBackground = true; // 設定執行緒為後臺執行緒
            thread.Start(); // 啟動執行緒
        }

        /// <summary>
        /// 處理新增按鈕的點選事件，將選中的專案從系統組列表框移動到已選組列表框。
        /// </summary>
        /// <param name="sender">事件的觸發者</param>
        /// <param name="e">事件引數</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // 如果未選擇任何專案，直接返回
            if (listBoxSystemGroup.SelectedIndex == -1)
            {
                return;
            }

            // 儲存當前選中項的索引
            int nowSelectedIndex = listBoxSystemGroup.SelectedIndex;
            // 獲取選中的所有專案，並將其轉換為列表
            List<Object> selectedObjectCollection = listBoxSystemGroup.SelectedItems.Cast<object>().ToList();

            // 清除目標列表框的選中狀態
            listBoxSelectedGroup.SelectedItems.Clear();

            // 將選中的專案從系統組列表框移動到已選組列表框
            foreach (var item in selectedObjectCollection)
            {
                listBoxSelectedGroup.Items.Add(item); // 新增到已選組列表框
                listBoxSelectedGroup.SelectedItems.Add(item); // 設定為選中狀態
                listBoxSystemGroup.Items.Remove(item); // 從系統組列表框移除
            }

            // 更新系統組列表框的選中狀態
            if (listBoxSystemGroup.Items.Count > 0 && nowSelectedIndex >= 0)
            {
                if (nowSelectedIndex >= listBoxSystemGroup.Items.Count)
                {
                    listBoxSystemGroup.SelectedIndex = listBoxSystemGroup.Items.Count - 1;
                }
                else
                {
                    listBoxSystemGroup.SelectedIndex = nowSelectedIndex;
                }
            }

            // 檢查按鈕狀態
            chkBtnEnable();
            // 檢查狀態改變
            chkChange();
        }

        /// <summary>
        /// 處理移除按鈕的點選事件，將選中的專案從已選組列表框移動到系統組列表框。
        /// </summary>
        /// <param name="sender">事件的觸發者</param>
        /// <param name="e">事件引數</param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // 如果未選擇任何專案，直接返回
            if (listBoxSelectedGroup.SelectedIndex == -1)
            {
                return;
            }

            // 儲存當前選中項的索引
            int nowSelectedIndex = listBoxSelectedGroup.SelectedIndex;
            // 獲取選中的所有專案，並將其轉換為列表
            List<Object> selectedObjectCollection = listBoxSelectedGroup.SelectedItems.Cast<object>().ToList();

            // 清除目標列表框的選中狀態
            listBoxSystemGroup.SelectedItems.Clear();

            // 將選中的專案從已選組列表框移動到系統組列表框
            foreach (var item in selectedObjectCollection)
            {
                listBoxSystemGroup.Items.Add(item); // 新增到系統組列表框
                listBoxSystemGroup.SelectedItems.Add(item); // 設定為選中狀態
                listBoxSelectedGroup.Items.Remove(item); // 從已選組列表框移除
            }

            // 更新已選組列表框的選中狀態
            if (listBoxSelectedGroup.Items.Count > 0 && nowSelectedIndex >= 0)
            {
                if (nowSelectedIndex >= listBoxSelectedGroup.Items.Count)
                {
                    listBoxSelectedGroup.SelectedIndex = listBoxSelectedGroup.Items.Count - 1;
                }
                else
                {
                    listBoxSelectedGroup.SelectedIndex = nowSelectedIndex;
                }
            }

            // 檢查按鈕狀態
            chkBtnEnable();
            // 檢查狀態改變
            chkChange();
        }

        /// <summary>
        /// 處理系統組列表框的選中項更改事件。
        /// 用於更新按鈕的啟用狀態。
        /// </summary>
        /// <param name="sender">事件的触发者</param>
        /// <param name="e">事件参数</param>
        private void listBoxSystemGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkBtnEnable();
        }

        /// <summary>
        /// 處理已選組列表框的選中項更改事件。
        /// 用於更新按鈕的啟用狀態。
        /// </summary>
        /// <param name="sender">事件的触发者</param>
        /// <param name="e">事件参数</param>
        private void listBoxSelectedGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkBtnEnable();
        }

        /// <summary>
        /// 當文字框內容發生變化時觸發此事件處理程式。
        /// 根據文字框的內容長度，啟用或禁用新增自定義按鈕。
        /// </summary>
        /// <param name="sender">事件的傳送者物件</param>
        /// <param name="e">事件引數</param>
        private void textBoxCus_TextChanged(object sender, EventArgs e)
        {
            // 如果文字框有內容，則啟用按鈕，否則禁用按鈕
            buttonAddCustom.Enabled = textBoxCustom.Text.Length > 0;
        }

        /// <summary>
        /// 當點選“新增自定義”按鈕時觸發此事件處理程式。
        /// 將文字框內容新增到選定組列表框，並清空文字框，處理重複項和按鈕狀態。
        /// </summary>
        /// <param name="sender">事件的傳送者物件</param>
        /// <param name="e">事件引數</param>
        private void buttonAddCustom_Click(object sender, EventArgs e)
        {
            // 將文字框內容新增到選定組列表框中
            listBoxSelectedGroup.Items.Add(textBoxCustom.Text);
            // 清空文字框內容
            textBoxCustom.Text = "";
            // 移除重複專案
            RemoveDuplicateItems();
            // 檢查按鈕是否應啟用
            chkBtnEnable();
            // 檢查是否有更改
            chkChange();
        }

        /// <summary>
        /// 當窗體關閉時觸發此事件處理程式。
        /// 如果有未儲存的更改，提示使用者是否儲存更改。
        /// </summary>
        /// <param name="sender">事件的傳送者物件</param>
        /// <param name="e">窗體關閉事件引數</param>
        private void FormGroupSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 如果可以直接關閉，則無需進一步處理
            if (closeNow)
            {
                e.Cancel = false;
                return;
            }

            // 如果有未儲存的更改，提示使用者選擇操作
            if (buttonOK.Enabled)
            {
                DialogResult = MessageBox.Show(
                    "更改尚未保存，是否要保存更改？",
                    "更改未保存",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (DialogResult == DialogResult.Yes)
                {
                    closeNow = true;
                    buttonOK_Click(sender, e); // 呼叫儲存按鈕的點選事件
                }
                else if (DialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true; // 取消關閉操作
                }
            }
        }

        /// <summary>
        /// 當用戶在自定義文字框中輸入內容時觸發此事件處理程式。
        /// 僅允許輸入字母、數字、連字元、下劃線和空格，阻止其他字元輸入。
        /// </summary>
        /// <param name="sender">事件的傳送者物件</param>
        /// <param name="e">按鍵事件引數</param>
        private void textBoxCustom_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 如果輸入字元不是控制字元、字母或數字，也不是允許的特殊字元，則阻止輸入
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '-' &&
                e.KeyChar != '_' &&
                e.KeyChar != ' ')
            {
                e.Handled = true; // 阻止非法字元輸入
            }
        }

        /// <summary>
        /// 定時器 tick 事件處理器，用於停止等待動畫。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void timerStopWaitAni_Tick(object sender, EventArgs e)
        {
            // 停止等待动画
            waitAni(false);
        }

        /// <summary>
        /// 定時器 tick 事件處理器，用於更新等待動畫的顯示文字。
        /// </summary>
        /// <param name="sender">事件的傳送者物件。</param>
        /// <param name="e">事件引數。</param>
        private void timerWaitAni_Tick(object sender, EventArgs e)
        {
            // 更新等待標籤的文字內容
            labelWait.Text = slboot.UpdateChar();
        }
    }
}