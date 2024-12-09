using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace UserInfo
{
    /// <summary>
    /// 用於管理 Windows 使用者賬戶
    /// </summary>
    internal class UserInfoModifier
    {
        public string domain;

        /// <summary>
        /// 建構函式，初始化域名。
        /// </summary>
        /// <param name="domain">域名，如果為 null 則使用當前計算機名。</param>
        public UserInfoModifier(string domain = null)
        {
            this.domain = domain ?? Environment.MachineName;
        }

        /// <summary>
        /// 建立一個新使用者並設定其密碼。
        /// </summary>
        /// <param name="username">新使用者的使用者名稱。</param>
        /// <param name="password">新使用者的密碼。</param>
        /// <returns>返回使用者建立成功的訊息或錯誤訊息。</returns>
        public string CreateUser(string username, string password)
        {
            try
            {
                // 使用 WinNT 提供的方式連線到指定域。
                using (var entry = new DirectoryEntry($"WinNT://{domain}"))
                {
                    // 在域中新增一個新的使用者。
                    var user = entry.Children.Add(username, "user");

                    // 設定使用者的密碼。
                    user.Invoke("SetPassword", password);

                    // 提交使用者的更改，使其生效。
                    user.CommitChanges();
                }

                // 返回建立使用者成功的訊息。
                return "User created successfully.";
            }
            catch (Exception ex)
            {
                // 捕獲任何異常並返回異常訊息。
                return ex.Message;
            }
        }

        /// <summary>
        /// 刪除指定使用者名稱的使用者。
        /// </summary>
        /// <param name="username">要刪除的使用者名稱。</param>
        /// <returns>返回操作結果的訊息字串。如果成功，則返回 "User deleted successfully."；如果失敗，則返回異常訊息。</returns>
        public string DeleteUser(string username)
        {
            try
            {
                // 使用 WinNT 提供的方式訪問指定域中的目錄入口。
                using (var entry = new DirectoryEntry($"WinNT://{domain}"))
                {
                    // 在目錄中查詢指定使用者名稱的使用者物件，型別為 "user"。
                    var user = entry.Children.Find(username, "user");

                    // 從目錄的子項中移除找到的使用者物件。
                    entry.Children.Remove(user);
                }

                // 返回成功刪除使用者的訊息。
                return "User deleted successfully.";
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回異常訊息。
                return ex.Message;
            }
        }
    }
}
