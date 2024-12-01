using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace SystemRes
{
    /// <summary>
    /// 提供從系統資原始檔中提取圖示
    /// </summary>
    public class Shell32IconHelper
    {
        /// <summary>
        /// 呼叫 Win32 API LoadLibraryEx，從指定的 DLL 檔案載入模組。
        /// </summary>
        /// <param name="lpFileName">DLL 檔案的路徑。</param>
        /// <param name="hFile">檔案控制代碼，通常為 IntPtr.Zero。</param>
        /// <param name="dwFlags">載入選項標誌。</param>
        /// <returns>載入模組的控制代碼，如果失敗返回 IntPtr.Zero。</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

        /// <summary>
        /// 呼叫 Win32 API ExtractIconEx，從指定的 DLL 檔案中提取圖示。
        /// </summary>
        /// <param name="lpszFile">DLL 檔案的路徑。</param>
        /// <param name="nIconIndex">圖示索引。</param>
        /// <param name="phiconLarge">大圖示控制代碼陣列。</param>
        /// <param name="phiconSmall">小圖示控制代碼陣列。</param>
        /// <param name="nIcons">要提取的圖示數量。</param>
        /// <returns>實際提取的圖示數量。</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ExtractIconEx(
            string lpszFile,
            int nIconIndex,
            IntPtr[] phiconLarge,
            IntPtr[] phiconSmall,
            int nIcons);

        /// <summary>
        /// 呼叫 Win32 API DestroyIcon，銷燬指定的圖示控制代碼。
        /// </summary>
        /// <param name="hIcon">要銷燬的圖示控制代碼。</param>
        /// <returns>如果成功銷燬圖示返回 true，否則返回 false。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// 用於儲存檔案資訊的結構體。
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon; // 圖示控制代碼
            public int iIcon; // 圖示索引
            public uint dwAttributes; // 檔案屬性
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName; // 檔案顯示名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName; // 檔案型別名
        }

        /// <summary>
        /// 從系統的資原始檔中提取圖示。
        /// </summary>
        /// <param name="iconIndex">圖示索引。</param>
        /// <param name="imageresdll">資原始檔路徑，預設為 "\\System32\\SHELL32.dll" 。</param>
        /// <returns>提取的圖示物件。</returns>
        public static Icon GetIconFromSysImageres(int iconIndex, string imageresdll = "\\System32\\SHELL32.dll")
        {
            string windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            return ExtractIconFromDll(windowsPath + imageresdll, iconIndex);
        }

        /// <summary>
        /// 從指定的 DLL 檔案中提取圖示。
        /// </summary>
        /// <param name="dllPath">DLL 檔案路徑。</param>
        /// <param name="iconIndex">圖示索引。</param>
        /// <returns>提取的圖示物件，如果提取失敗返回 null。</returns>
        static Icon ExtractIconFromDll(string dllPath, int iconIndex)
        {
            IntPtr[] largeIcons = new IntPtr[1];
            IntPtr[] smallIcons = new IntPtr[1];
            int iconsExtracted = ExtractIconEx(dllPath, iconIndex, largeIcons, null, 1);
            if (iconsExtracted > 0 && largeIcons[0] != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(largeIcons[0]).Clone() as Icon;
                DestroyIcon(largeIcons[0]);
                return icon;
            }
            MessageBox.Show($"{Marshal.GetLastWin32Error()}", $"无法提取图标 {iconIndex} : {dllPath}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        /// <summary>
        /// 從系統的資原始檔中提取圖示並轉換為點陣圖。
        /// </summary>
        /// <param name="iconIndex">圖示索引。</param>
        /// <param name="imageresdll">資原始檔路徑，預設為 "\\System32\\SHELL32.dll" 。</param>
        /// <returns>提取的圖示點陣圖物件。</returns>
        public static Bitmap GetBitmapFromSysImageres(int iconIndex, string imageresdll = "\\System32\\SHELL32.dll")
        {
            Icon icon = GetIconFromSysImageres(iconIndex, imageresdll);
            if (icon != null)
            {
                return icon.ToBitmap();
            }
            return CreatePlaceholderBitmap();
        }

        /// <summary>
        /// 建立一個佔位的灰色點陣圖，當無法提取圖示時使用。
        /// </summary>
        /// <returns>佔位點陣圖物件。</returns>
        public static Bitmap CreatePlaceholderBitmap()
        {
            Bitmap placeholder = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.Clear(Color.Gray);
                g.DrawString("?", new Font("Arial", 16), Brushes.White, new PointF(4, 4));
            }
            return placeholder;
        }
    }
}