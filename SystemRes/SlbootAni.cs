using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace Slboot
{
    public class SlbootAni
    {
        public Font aniFont = null;
        private PrivateFontCollection privateFonts;
        private int currentCharIndex = 0;
        // Unicode 私有域字元範圍
        private const int StartCodePoint = 0xE052;
        private const int EndCodePoint = 0xE0C6;
        public void Init(float emSize)
        {
            string windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            string fontPath = Path.Combine(windowsPath, "\\Boot\\Fonts\\segoe_slboot.ttf");
            if (!File.Exists(fontPath))
            {
                Console.WriteLine("找不到动画字体 " + fontPath);
                return;
            }
            privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile(fontPath);
            aniFont = new Font(privateFonts.Families[0], emSize, FontStyle.Regular, GraphicsUnit.Point);
        }

        public string UpdateChar()
        {
            int codePoint = StartCodePoint + (currentCharIndex % (EndCodePoint - StartCodePoint + 1));
            currentCharIndex++;
            return char.ConvertFromUtf32(codePoint);
        }
    }
}
