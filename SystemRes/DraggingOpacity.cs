using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemRes
{
    /// <summary>
    /// 用於實現窗體拖動時調整不透明度的功能。
    /// </summary>
    public class DraggingOpacity
    {
        // 表示是否正在拖動窗體的標誌
        private bool isDragging = false;

        // 需要調整不透明度的目標窗體
        private Form window;

        // 用於標記是否是第一次移動窗體
        public bool firstMove = true;

        /// <summary>
        /// 初始化 DraggingOpacity 類的新例項。
        /// </summary>
        /// <param name="window">需要調整不透明度的窗體物件。</param>
        public DraggingOpacity(Form window)
        {
            // 初始化窗体引用
            this.window = window;
        }

        /// <summary>
        /// 調整窗體不透明度的方法。
        /// 如果是第一次移動，不會改變窗體的不透明度；
        /// 否則設定窗體的不透明度
        /// </summary>
        public void winMove()
        {
            // 判斷是否是第一次移動窗體
            if (firstMove)
            {
                // 第一次移動，設定標誌位為 false
                firstMove = false;
            }
            else
            {
                // 如果當前窗體不透明度不是 0.6，則將其設定為 0.6
                if (this.window.Opacity != 0.6)
                    this.window.Opacity = 0.6;
            }
        }

        /// <summary>
        /// 此方法專注於處理視窗拖動和狀態變更時的透明度。
        /// </summary>
        /// <param name="m">參考型別的 Message 結構，表示 Windows 訊息。</param>
        public void wndProc(Message m)
        {
            // 處理接收到的 Windows 訊息。
            switch (m.Msg)
            {
                // 處理 WM_SYSCOMMAND 訊息，0x0112 表示此訊息的值。
                case 0x0112:
                    // 檢查 WParam 的低 4 位是否等於 SC_MOVE（0xF010），表示視窗正在被拖動。
                    if ((m.WParam.ToInt32() & 0xFFF0) == 0xF010)
                    {
                        isDragging = true; // 設定正在拖動的標誌。
                    }
                    break;

                // 處理 WM_EXITSIZEMOVE 訊息，0x0232 表示視窗大小調整或拖動結束。
                case 0x0232:
                    if (isDragging)
                    {
                        isDragging = false; // 重置正在拖動的標誌。
                        this.window.Opacity = 1;        // 恢復視窗的透明度。
                    }
                    break;
            }

            // 如果視窗的狀態不是正常（可能是最小化或最大化），恢復透明度。
            if (this.window.WindowState != FormWindowState.Normal)
            {
                this.window.Opacity = 1;
            }
        }
    }
}
