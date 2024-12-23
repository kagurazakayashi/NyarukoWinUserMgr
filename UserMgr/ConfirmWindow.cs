using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemRes;

namespace WinUserMgr
{
    public partial class ConfirmWindow : Form
    {
        private DraggingOpacity draggingOpacity;
        public event Action StartButtonClicked;
        public event Action CancelButtonClicked;

        public ConfirmWindow()
        {
            InitializeComponent();
            draggingOpacity = new DraggingOpacity(this);
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
        /// 視窗載入事件處理方法。
        /// </summary>
        /// <param name="sender">事件傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void ConfirmWindow_Load(object sender, EventArgs e)
        {
            // 在視窗載入時執行的邏輯
        }

        /// <summary>
        /// 視窗大小更改事件處理方法。
        /// </summary>
        /// <param name="sender">事件傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void ConfirmWindow_SizeChanged(object sender, EventArgs e)
        {
            // 當視窗大小發生變化時，標記拖動透明度的首次移動。
            draggingOpacity.firstMove = true;
        }

        /// <summary>
        /// 視窗移動事件處理方法。
        /// </summary>
        /// <param name="sender">事件傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void ConfirmWindow_Move(object sender, EventArgs e)
        {
            // 當視窗移動時，呼叫拖動透明度的移動方法。
            draggingOpacity.winMove();
        }

        /// <summary>
        /// 確認按鈕點選事件處理方法。
        /// </summary>
        /// <param name="sender">事件傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void toolStripButtonOK_Click(object sender, EventArgs e)
        {
            // 如果任務列表為空，提示使用者並返回。
            if (listBoxTasks.Items.Count == 0)
            {
                MessageBox.Show("以管理员权限运行本程序并修改表格，\n即可在这里实时看到将要修改的项目。", "没有要执行的任务", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 更新工具條狀態，設定背景顏色和狀態文字。
            toolStrip1.BackColor = Color.Orange;
            toolStripLabelStatus.Text = "正在执行操作，请稍候……";

            // 隱藏確認按鈕。
            toolStripButtonOK.Visible = false;

            // 觸發開始按鈕點選事件（由外部訂閱）。
            StartButtonClicked?.Invoke(); // -> MainWindowFunc.cs/StartButtonClicked()
        }

        /// <summary>
        /// 關閉按鈕點選事件處理方法。
        /// </summary>
        /// <param name="sender">事件傳送者。</param>
        /// <param name="e">事件引數。</param>
        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            // 顯示確認按鈕並隱藏關閉按鈕。
            toolStripButtonOK.Visible = true;
            toolStripButtonClose.Visible = false;
            toolStrip1.BackColor = Color.SkyBlue;

            // 觸發取消按鈕點選事件（由外部訂閱）。
            CancelButtonClicked?.Invoke(); // -> MainWindowFunc.cs/CancelButtonClicked()

            // 關閉視窗。
            Close();
        }

        /// <summary>
        /// 視窗關閉事件處理方法。
        /// 如果正在執行操作，取消關閉操作。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (toolStrip1.BackColor == Color.Orange)
            {
                e.Cancel = true;
            }
            else if (toolStrip1.BackColor == Color.Pink)
            {
                toolStripButtonClose_Click(sender, e);
            }
        }

        private void ConfirmWindow_ResizeEnd(object sender, EventArgs e)
        {
            Opacity = 1;
        }
    }
}
