using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemRes
{
    public class DraggingOpacity
    {
        private bool isDragging = false;
        private Form window;

        public DraggingOpacity(Form window)
        {
            this.window = window;
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
                        this.window.Opacity = 0.6;    // 將視窗透明度設為 0.6。
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
