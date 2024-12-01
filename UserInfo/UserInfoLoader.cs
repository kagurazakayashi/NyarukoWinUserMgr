using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management;
using System.Net;

namespace UserInfo
{
    /// <summary>
    /// 用於載入本機使用者資訊的類別。
    /// </summary>
    public class UserLoader
    {
        // 初始化一個空的使用者列表
        public List<UserInfoType> users = new List<UserInfoType>();

        /// <summary>
        /// 從指定的計算機檢索所有使用者組。
        /// </summary>
        /// <param name="computerName">目標計算機的名稱或 IP 地址。如果為 null 或空，將使用當前計算機名稱。</param>
        /// <returns>包含使用者組資訊的二維陣列，每行包括域名和組名。如果發生錯誤，返回包含錯誤資訊的特殊格式。</returns>
        public static string[][] GetGroups(string computerName = "")
        {
            // 如果未提供計算機名稱，使用當前計算機名稱
            if (string.IsNullOrWhiteSpace(computerName))
            {
                computerName = Dns.GetHostName();
            }

            // 構建 WMI 查詢路徑
            string queryPath = $"\\\\{computerName}\\root\\cimv2";

            // 初始化 WMI 作用域
            ManagementScope scope = new ManagementScope(queryPath);
            try
            {
                // 連線到指定計算機的 WMI
                scope.Connect();

                // 定義查詢語句以獲取使用者組資訊
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Group");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                // 用於儲存使用者組資訊的列表
                List<string[]> groups = new List<string[]>();

                // 遍歷查詢結果並提取使用者組資訊
                foreach (ManagementObject group in searcher.Get())
                {
                    // 新增域名和組名至列表
                    groups.Add(new string[] { group["Domain"].ToString(), group["Name"].ToString() });
                }

                // 返回使用者組資訊的陣列
                return groups.ToArray();
            }
            catch (Exception ex)
            {
                // 返回包含錯誤資訊的特殊格式
                return new string[][] { new string[] { "%E%", ex.Message } };
            }
        }

        /// <summary>
        /// 獲取本機電腦中的所有使用者資訊。
        /// </summary>
        /// <returns>返回包含本機使用者資訊的列表。</returns>
        public void GetLocalUsers(string machineName = "")
        {
            try
            {
                // 獲取本機電腦名稱
                if (machineName.Length == 0)
                {
                    machineName = Environment.MachineName;
                }

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
                        var userInfo = new UserInfoType
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
                users.Add(new UserInfoType { ErrorInfo = ex.Message });
            }
        }
    }
}
