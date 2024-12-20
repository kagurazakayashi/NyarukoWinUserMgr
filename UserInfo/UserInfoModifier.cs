using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace UserInfo
{
    /// <summary>
    /// 用於管理 Windows 使用者賬戶
    /// </summary>
    public class UserInfoModifier
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
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
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
                return "";
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
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
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
                return "";
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
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
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

                // 如果成功執行，返回 ""。
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
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
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
                // 如果操作成功，返回 ""。
                return "";
            }
            catch (Exception ex)
            {
                // 如果操作失敗，返回異常的訊息。
                return ex.Message;
            }
        }

        /// <summary>
        /// 設定使用者下次登入時是否需要更改密碼。
        /// </summary>
        /// <param name="username">使用者名稱。</param>
        /// <param name="required">布林值，指示是否要求更改密碼。如果為 true，則設定為需要更改密碼；如果為 false，則取消該要求。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string SetPasswordChangeOnNextLogon(string username, bool required)
        {
            try
            {
                // 獲取使用者的目錄條目（如 Active Directory 條目）
                using (var userEntry = GetUserEntry(username))
                {
                    // 獲取使用者標誌屬性值
                    int userFlags = (int)userEntry.Properties["UserFlags"].Value;

                    // 如果 required 為 true，則設定標誌位（0x1000）表示需要更改密碼
                    if (required)
                        userFlags |= 0x1000; // 按位或操作，設定特定位為 1
                    else
                        userFlags &= ~0x1000; // 按位與和按位取反操作，清除特定位

                    // 更新使用者標誌屬性值
                    userEntry.Properties["UserFlags"].Value = userFlags;

                    // 提交更改到目錄
                    userEntry.CommitChanges();
                }

                // 返回操作成功
                return "";
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回異常訊息
                return ex.Message;
            }
        }

        /// <summary>
        /// 設定使用者密碼是否不能被更改。
        /// </summary>
        /// <param name="username">使用者名稱。</param>
        /// <param name="cannotChange">指示密碼是否不能被更改的布林值。傳遞 true 表示密碼不能被更改，false 表示可以更改。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string SetPasswordCannotBeChanged(string username, bool cannotChange)
        {
            try
            {
                // 使用使用者條目進行操作
                using (var userEntry = GetUserEntry(username))
                {
                    // 獲取使用者標誌（UserFlags）屬性值
                    int userFlags = (int)userEntry.Properties["UserFlags"].Value;

                    // 如果 cannotChange 為 true，設定標誌位 0x40（禁止更改密碼）
                    if (cannotChange)
                        userFlags |= 0x40; // 按位或操作，啟用標誌位
                    else
                        userFlags &= ~0x40; // 按位與操作，清除標誌位

                    // 更新使用者標誌屬性
                    userEntry.Properties["UserFlags"].Value = userFlags;

                    // 提交更改到目錄
                    userEntry.CommitChanges();
                }

                // 返回操作成功訊息
                return "";
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回異常訊息
                return ex.Message;
            }
        }

        /// <summary>
        /// 設定使用者密碼是否永不過期。
        /// </summary>
        /// <param name="username">使用者的使用者名稱。</param>
        /// <param name="neverExpires">一個布林值，指示密碼是否應設定為永不過期。
        /// 如果為 true，則設定為永不過期；否則，取消該設定。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string SetPasswordNeverExpires(string username, bool neverExpires)
        {
            try
            {
                // 使用 GetUserEntry 方法獲取指定使用者的目錄條目
                using (var userEntry = GetUserEntry(username))
                {
                    // 獲取使用者的 UserFlags 屬性值，並轉換為整數
                    int userFlags = (int)userEntry.Properties["UserFlags"].Value;

                    // 如果需要設定密碼永不過期，則將標誌位 0x10000 設定為 1
                    if (neverExpires)
                        userFlags |= 0x10000; // 使用按位或運算子設定標誌位
                    else
                        userFlags &= ~0x10000; // 使用按位與和取反運算子清除標誌位

                    // 將更新後的標誌位值寫回 UserFlags 屬性
                    userEntry.Properties["UserFlags"].Value = userFlags;

                    // 提交對目錄條目的更改
                    userEntry.CommitChanges();
                }

                // 如果操作成功，返回 ""
                return "";
            }
            catch (Exception ex)
            {
                // 如果發生異常，返回異常訊息
                return ex.Message;
            }
        }

        /// <summary>
        /// 設定使用者賬戶的啟用或停用狀態。
        /// </summary>
        /// <param name="username">要操作的使用者名稱。</param>
        /// <param name="disabled">一個布林值，指示賬戶是否應被停用（true 表示停用，false 表示啟用）。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string SetAccountDisabled(string username, bool disabled)
        {
            try
            {
                // 使用 GetUserEntry 方法獲取指定使用者名稱的使用者條目。
                using (var userEntry = GetUserEntry(username))
                {
                    // 獲取使用者當前的 UserFlags 屬性值。
                    int userFlags = (int)userEntry.Properties["UserFlags"].Value;

                    // 如果需要停用賬戶，將 UserFlags 的第 1 位設定為 1。
                    if (disabled)
                        userFlags |= 0x2; // 透過按位或操作設定第 1 位。
                    else
                        userFlags &= ~0x2; // 透過按位與操作清除第 1 位。

                    // 更新使用者條目的 UserFlags 屬性值。
                    userEntry.Properties["UserFlags"].Value = userFlags;

                    // 提交對使用者條目的更改。
                    userEntry.CommitChanges();
                }

                // 返回操作成功的訊息。
                return "";
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回異常訊息。
                return ex.Message;
            }
        }

        /// <summary>
        /// 設定使用者賬號是否被鎖定。
        /// </summary>
        /// <param name="username">要操作的使用者名稱。</param>
        /// <param name="locked">指定是否鎖定賬號。如果需要重置密碼，傳入 false。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string SetAccountLocked(string username, bool locked)
        {
            try
            {
                // 使用 GetUserEntry 方法獲取使用者條目物件。
                using (var userEntry = GetUserEntry(username))
                {
                    // 呼叫使用者條目的 SetPassword 方法，設定密碼為 null，解除可能存在的鎖定。
                    userEntry.Invoke("SetPassword", new object[] { null });

                    // 提交更改以應用更新。
                    userEntry.CommitChanges();
                }

                // 返回操作成功的訊息。
                return "";
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回異常資訊。
                return ex.Message;
            }
        }

        /// <summary>
        /// 將指定的使用者新增到指定的組。
        /// </summary>
        /// <param name="username">使用者名稱，表示要新增的使用者。</param>
        /// <param name="groupName">組名，表示使用者要加入的組。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string AddToGroup(string username, string groupName)
        {
            try
            {
                // 使用 GetUserEntry 方法獲取使用者條目。
                // 透過 using 語句確保在使用完後資源會被自動釋放。
                using (var userEntry = GetUserEntry(username))
                // 建立一個目錄條目物件，表示指定的組。
                using (var groupEntry = new DirectoryEntry($"WinNT://{domain}/{groupName},group"))
                {
                    // 呼叫組的 Add 方法，將使用者條目的路徑新增到組。
                    groupEntry.Invoke("Add", new object[] { userEntry.Path });
                }
                // 如果成功，返回 ""。
                return "";
            }
            catch (Exception ex)
            {
                // 如果發生異常，返回異常的訊息。
                return ex.Message;
            }
        }



        /// <summary>
        /// 設定使用者密碼。
        /// </summary>
        /// <param name="username">使用者名稱。</param>
        /// <param name="newPassword">新密碼。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string SetUserPassword(string username, string newPassword)
        {
            try
            {
                // 使用 GetUserEntry 方法獲取使用者條目。
                using (var userEntry = GetUserEntry(username))
                {
                    // 呼叫 SetPassword 方法設定使用者密碼。
                    userEntry.Invoke("SetPassword", new object[] { newPassword });

                    // 提交更改以儲存密碼。
                    userEntry.CommitChanges();
                }

                // 如果成功，返回空字符串。
                return "";
            }
            catch (Exception ex)
            {
                // 如果發生異常，返回異常的訊息。
                return ex.Message;
            }
        }

        /// <summary>
        /// 從指定的組中移除使用者。
        /// </summary>
        /// <param name="username">要移除的使用者名稱。</param>
        /// <param name="groupName">組的名稱。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string RemoveFromGroup(string username, string groupName)
        {
            try
            {
                // 獲取使用者的目錄條目
                using (var userEntry = GetUserEntry(username))
                // 獲取組的目錄條目
                using (var groupEntry = new DirectoryEntry($"WinNT://{domain}/{groupName},group"))
                {
                    // 呼叫組條目的 Remove 方法，移除指定使用者
                    groupEntry.Invoke("Remove", new object[] { userEntry.Path });
                }
                // 如果成功，返回 ""
                return "";
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回異常訊息
                return ex.Message;
            }
        }

        /// <summary>
        /// 建立一個組。
        /// </summary>
        /// <param name="groupName">組名稱。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string CreateGroup(string groupName)
        {
            try
            {
                // 透過 WinNT 協議連線到指定域的目錄。
                using (var entry = new DirectoryEntry($"WinNT://{domain}"))
                {
                    // 建立一個新的群組節點。
                    var group = entry.Children.Add(groupName, "group");
                    // 提交變更以生效。
                    group.CommitChanges();
                }
                // 返回建立成功的資訊。
                return "";
            }
            catch (Exception ex)
            {
                // 如果發生異常，返回異常資訊。
                return ex.Message;
            }
        }

        /// <summary>
        /// 刪除指定名稱的組。
        /// </summary>
        /// <param name="groupName">要刪除的組名。</param>
        /// <returns>操作結果的字串。如果成功，返回 ""；如果發生異常，返回異常訊息。</returns>
        public string DeleteGroup(string groupName)
        {
            try
            {
                // 使用 DirectoryEntry 对象连接到指定域（WinNT:// 格式用于访问 Windows NT 域）。
                using (var entry = new DirectoryEntry($"WinNT://{domain}"))
                {
                    // 查找具有指定名称的组对象。
                    var group = entry.Children.Find(groupName, "group");

                    // 从 DirectoryEntry 的子对象集合中移除该组。
                    entry.Children.Remove(group);
                }
                // 如果删除成功，返回空字符串。
                return "";
            }
            catch (Exception ex)
            {
                // 捕获异常并返回异常消息。
                return ex.Message;
            }
        }

    }
}
