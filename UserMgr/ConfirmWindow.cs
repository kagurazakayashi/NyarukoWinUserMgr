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

        private void ConfirmWindow_Load(object sender, EventArgs e)
        {
        }

        private void ConfirmWindow_SizeChanged(object sender, EventArgs e)
        {
            draggingOpacity.firstMove = true;
        }

        private void ConfirmWindow_Move(object sender, EventArgs e)
        {
            draggingOpacity.winMove();
        }

        private void toolStripButtonOK_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.Items.Count == 0)
            {
                MessageBox.Show("以管理员权限运行本程序并修改表格，\n即可在这里实时看到将要修改的项目。", "没有要执行的任务", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StartButtonClicked?.Invoke();
        }
    }
}
