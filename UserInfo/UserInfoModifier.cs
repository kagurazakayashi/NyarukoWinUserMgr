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
        /// 獲取指定使用者名稱的目錄條目。
        /// </summary>
        /// <param name="username">使用者名稱，表示要查詢的使用者。</param>
        /// <returns>返回與指定使用者名稱關聯的目錄條目。</returns>
        private DirectoryEntry GetUserEntry(string username)
        {
            // 定義使用者路徑，使用 WinNT 提供的路徑格式。
            // domain 是當前類中定義的域名變數，username 是方法引數。
            string userPath = $"WinNT://{domain}/{username},user";

            // 建立並返回一個新的 DirectoryEntry 物件，表示指定使用者的目錄條目。
            return new DirectoryEntry(userPath);
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

        /// <summary>
        /// 設定指定使用者的全名。
        /// </summary>
        /// <param name="username">要設定全名的使用者名稱。</param>
        /// <param name="fullName">要設定的全名。</param>
        /// <returns>返回操作結果："Success" 表示成功，否則返回異常訊息。</returns>
        public string SetFullName(string username, string fullName)
        {
            try
            {
                // 使用 GetUserEntry 方法獲取指定使用者名稱的使用者條目。
                using (var userEntry = GetUserEntry(username))
                {
                    // 設定使用者條目中的 "FullName" 屬性的值。
                    userEntry.Properties["FullName"].Value = fullName;

                    // 提交更改以儲存修改。
                    userEntry.CommitChanges();
                }

                // 如果成功執行，返回 "Success"。
                return "";
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回異常訊息。
                return ex.Message;
            }
        }

        /// <summary>
        /// 設定指定使用者的描述資訊。
        /// </summary>
        /// <param name="username">使用者名稱。</param>
        /// <param name="description">要設定的描述資訊。</param>
        /// <returns>返回操作結果。如果成功，返回 "Success"；如果失敗，返回錯誤資訊。</returns>
        public string SetDescription(string username, string description)
        {
            try
            {
                // 獲取指定使用者的條目。
                using (var userEntry = GetUserEntry(username))
                {
                    // 設定使用者條目的 "Description" 屬性值。
                    userEntry.Properties["Description"].Value = description;

                    // 提交更改以儲存設定。
                    userEntry.CommitChanges();
                }
                // 如果操作成功，返回 "Success"。
                return "";
            }
            catch (Exception ex)
            {
                // 如果操作失敗，返回異常的訊息。
                return ex.Message;
            }
        }
    }
}
