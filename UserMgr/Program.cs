using System;
using System.Windows.Forms;

namespace winusermgr
{
    /// <summary>
    /// 定義應用程式的名稱空間和入口類。
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主入口點。
        /// </summary>
        [STAThread] // 指定應用程式的執行緒模型為單執行緒單元（Single-Threaded Apartment）。
        static void Main()
        {
            // 啟用應用程式的視覺樣式，使控制元件的顯示效果更符合現代作業系統的風格。
            Application.EnableVisualStyles();
            
            // 設定應用程式預設使用相容的文字渲染方式。
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 啟動主視窗（Form1）。
            Application.Run(new MainWindow());
        }
    }
}
