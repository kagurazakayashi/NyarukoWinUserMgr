using System.IO;
using System.Runtime.InteropServices;
using System.Text;
namespace SystemRes
{
    /// <summary>
    /// 用於操作 INI 檔案的類。
    /// </summary>
    public class INI
    {
        /// <summary>
        /// INI 檔案的路徑。
        /// </summary>
        public string inipath;

        // 宣告呼叫 Windows API 的方法，用於寫入 INI 檔案。
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 宣告呼叫 Windows API 的方法，用於讀取 INI 檔案。
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 建構函式，初始化 INI 檔案路徑。
        /// </summary>
        /// <param name="INIPath">INI 檔案的完整路徑。</param>
        public INI(string INIPath)
        {
            inipath = INIPath;
        }

        /// <summary>
        /// 向 INI 檔案的指定節和鍵寫入值。
        /// </summary>
        /// <param name="Section">節名稱（Section）。</param>
        /// <param name="Key">鍵名稱（Key）。</param>
        /// <param name="Value">要寫入的值。</param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            // 呼叫 Windows API 寫入 INI 檔案。
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }

        /// <summary>
        /// 從 INI 檔案中讀取指定節和鍵的值。
        /// </summary>
        /// <param name="Section">節名稱（Section）。</param>
        /// <param name="Key">鍵名稱（Key）。</param>
        /// <returns>返回鍵對應的值，如果不存在則返回空字串。</returns>
        public string IniReadValue(string Section, string Key)
        {
            // 用於儲存讀取的值的緩衝區。
            StringBuilder temp = new StringBuilder(500);
            // 呼叫 Windows API 讀取 INI 檔案。
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }

        /// <summary>
        /// 檢查 INI 檔案是否存在。
        /// </summary>
        /// <returns>如果檔案存在則返回 true，否則返回 false。</returns>
        public bool ExistINIFile()
        {
            // 使用 File.Exists 方法檢查檔案是否存在。
            return File.Exists(inipath);
        }
    }
}
