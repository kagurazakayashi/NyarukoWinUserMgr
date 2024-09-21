using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using UserInfoType;

namespace UserInfoLoader
{
    /// <summary>
    /// 用於載入本機使用者資訊的類別。
    /// </summary>
    public class UserLoader
    {
        // 初始化一個空的使用者列表
        public List<UserInfo> users = new List<UserInfo>();

        /// <summary>
        /// 獲取本機電腦中的所有使用者資訊。
        /// </summary>
        /// <returns>返回包含本機使用者資訊的列表。</returns>
        public void GetLocalUsers()
        {
            try
            {
                // 獲取本機電腦名稱
                string machineName = Environment.MachineName;

                // 建立指向本機電腦的目錄條目
                DirectoryEntry localMachine = new DirectoryEntry($"WinNT://{machineName},computer");

                // 清空使用者列表
                users.Clear();

                // 遍歷本機電腦的所有子節點
                foreach (DirectoryEntry user in localMachine.Children)
                {
                    // 檢查節點是否為使用者類型
                    if (user.SchemaClassName == "User")
                    {
                        // 建立 UserInfo 對象以存儲使用者資訊
                        var userInfo = new UserInfo
                        {
                            Name = user.Name, // 使用者名稱
                            FullName = user.Properties["FullName"]?.Value?.ToString(), // 完整名稱
                            Description = user.Properties["Description"]?.Value?.ToString(), // 描述
                            AccountExpires = user.Properties["AccountExpirationDate"]?.Value?.ToString() ?? "Never", // 帳戶到期日期
                            Disabled = user.Properties["AccountDisabled"]?.Value != null && (bool)user.Properties["AccountDisabled"].Value, // 是否禁用帳戶
                            PasswordNeverExpires = user.Properties["PasswordExpirationDate"]?.Value == null, // 密碼是否永不過期
                            UserMayNotChangePassword = user.Properties["UserFlags"]?.Value?.ToString().Contains("65536") ?? false, // 使用者是否無法更改密碼
                            Groups = new List<string>(), // 所屬群組列表
                            ErrorInfo = string.Empty // 錯誤訊息
                        };

                        try
                        {
                            // 遍歷使用者所屬的群組
                            foreach (object group in (IEnumerable)user.Invoke("Groups"))
                            {
                                // 獲取群組名稱並添加到列表中
                                DirectoryEntry groupEntry = new DirectoryEntry(group);
                                userInfo.Groups.Add(groupEntry.Name);
                            }
                        }
                        catch (Exception ex)
                        {
                            // 若獲取群組過程出錯，添加錯誤提示
                            userInfo.ErrorInfo = ex.Message;
                        }

                        // 將使用者資訊添加到列表
                        users.Add(userInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                // 捕獲並顯示異常訊息
                users.Clear();
                users.Add(new UserInfo { ErrorInfo = ex.Message });
            }
        }
    }
}
