using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;

namespace SystemRes
{
    /// <summary>
    /// SlbootAni 類用於管理與動畫字型相關的操作。
    /// 包括初始化字型、更新字元序列以及停止字元更新。
    /// </summary>
    public class SlbootAni
    {
        /// <summary>
        /// 動畫字型物件，用於繪製字型。
        /// </summary>
        public Font aniFont = null;

        /// <summary>
        /// 私有字型集合，用於載入自定義字型檔案。
        /// </summary>
        private PrivateFontCollection privateFonts;

        /// <summary>
        /// 當前字元索引，用於跟蹤動畫字元的序列。
        /// </summary>
        private int currentCharIndex = 0;

        /// <summary>
        /// 動畫字元的起始 Unicode 碼點。
        /// </summary>
        private const int StartCodePoint = 0xE052;

        /// <summary>
        /// 動畫字元的結束 Unicode 碼點。
        /// </summary>
        private const int EndCodePoint = 0xE0C6;

        /// <summary>
        /// 初始化動畫字型。
        /// </summary>
        /// <param name="emSize">字型大小（以點為單位）。</param>
        public void Init(float emSize)
        {
            // 獲取 Windows 系統目錄路徑
            string windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            // 動畫字型的路徑
            string fontPath = windowsPath + "\\Boot\\Fonts\\segoe_slboot.ttf";

            // 檢查字型檔案是否存在
            if (!File.Exists(fontPath))
            {
                Console.WriteLine(windowsPath + " 找不到动画字体 " + fontPath);
                return;
            }

            // 載入私有字型集合
            privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile(fontPath);

            // 使用私有字型集合初始化字型物件
            aniFont = new Font(privateFonts.Families[0], emSize, FontStyle.Regular, GraphicsUnit.Point);
        }

        /// <summary>
        /// 更新動畫字元，並返回當前字元。
        /// 每次呼叫都會更新到下一個字元。
        /// </summary>
        /// <returns>當前動畫字元。</returns>
        public string UpdateChar()
        {
            // 根據當前索引計算 Unicode 碼點
            int codePoint = StartCodePoint + (currentCharIndex % (EndCodePoint - StartCodePoint + 1));

            // 更新字元索引
            currentCharIndex++;

            // 返回對應的字元
            return char.ConvertFromUtf32(codePoint);
        }

        /// <summary>
        /// 停止字元更新，將當前字元索引重置為 0。
        /// </summary>
        public void StopUpdateChar()
        {
            currentCharIndex = 0;
        }
    }
}