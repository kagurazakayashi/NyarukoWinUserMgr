using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Shell32IconHelper
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    public static extern int ExtractIconEx(
        string lpszFile,
        int nIconIndex,
        IntPtr[] phiconLarge,
        IntPtr[] phiconSmall,
        int nIcons);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool DestroyIcon(IntPtr hIcon);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    public static Icon GetIconFromSysImageres(int iconIndex, string imageresdll = "\\System32\\SHELL32.dll")
    {
        string windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        return ExtractIconFromDll(windowsPath + imageresdll, iconIndex);
    }
    static Icon ExtractIconFromDll(string dllPath, int iconIndex)
    {
        IntPtr[] largeIcons = new IntPtr[1];
        IntPtr[] smallIcons = new IntPtr[1];

        // 提取图标
        int iconsExtracted = ExtractIconEx(dllPath, iconIndex, largeIcons, null, 1);
        if (iconsExtracted > 0 && largeIcons[0] != IntPtr.Zero)
        {
            Icon icon = Icon.FromHandle(largeIcons[0]).Clone() as Icon;

            // 释放图标资源
            DestroyIcon(largeIcons[0]);

            return icon;
        }
        MessageBox.Show($"{Marshal.GetLastWin32Error()}", $"无法提取图标 {iconIndex} : {dllPath}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
    }
    public static Bitmap GetBitmapFromSysImageres(int iconIndex, string imageresdll = "\\System32\\SHELL32.dll")
    {
        Icon icon = GetIconFromSysImageres(iconIndex, imageresdll);
        if (icon != null)
        {
            return icon.ToBitmap();
        }
        return CreatePlaceholderBitmap();
    }
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
