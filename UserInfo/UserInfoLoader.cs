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
            string queryPath = "\\\\" + computerName + "\\root\\cimv2";

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
        public void GetLocalUsers(string machineName)
        {
            try
            {
                // 如果未提供計算機名稱，則使用本機名稱
                if (string.IsNullOrEmpty(machineName))
                {
                    machineName = Environment.MachineName;
                }

                // 建立指向本機計算機的目錄條目
                DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + machineName + ",computer");

                // 清空使用者列表
                users.Clear();

                // 遍歷本機計算機的所有子節點
                foreach (DirectoryEntry user in localMachine.Children)
                {
                    // 檢查節點是否為使用者型別
                    if (user.SchemaClassName == "User")
                    {
                        // 建立 UserInfo 物件以儲存使用者資訊
                        UserInfoType userInfo = new UserInfoType
                        {
                            Name = user.Name, // 使用者名稱稱
                            FullName = GetPropertyValue(user, "FullName"), // 完整名稱
                            Description = GetPropertyValue(user, "Description"), // 描述
                            AccountExpires = GetPropertyValue(user, "AccountExpirationDate") ?? "Never", // 帳戶到期日期
                            Disabled = GetPropertyValue(user, "AccountDisabled") == "True", // 是否停用帳戶
                            PasswordNeverExpires = GetPropertyValue(user, "PasswordExpirationDate") == null, // 密碼是否永不過期
                            UserMayNotChangePassword = CheckUserFlags(user), // 使用者是否無法更改密碼
                            Groups = new List<string>(), // 所屬群組列表
                            ErrorInfo = string.Empty // 錯誤資訊
                        };

                        try
                        {
                            // 遍歷使用者所屬的群組
                            foreach (object group in (IEnumerable)user.Invoke("Groups"))
                            {
                                // 獲取群組名稱並新增到列表中
                                DirectoryEntry groupEntry = new DirectoryEntry(group);
                                userInfo.Groups.Add(groupEntry.Name);
                            }
                        }
                        catch (Exception ex)
                        {
                            // 如果獲取群組過程中出錯，新增錯誤提示
                            userInfo.ErrorInfo = ex.Message;
                        }

                        // 將使用者資訊新增到列表
                        users.Add(userInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                // 捕獲並顯示異常資訊
                users.Clear();
                users.Add(new UserInfoType { ErrorInfo = ex.Message });
            }
        }

        // 用於獲取屬性值，返回空字串表示沒有值
        private string GetPropertyValue(DirectoryEntry entry, string propertyName)
        {
            try
            {
                if (entry.Properties[propertyName].Value != null)
                {
                    return entry.Properties[propertyName].Value.ToString();
                }
            }
            catch
            {
                // 如果獲取屬性時發生異常，返回空字串
                return null;
            }
            return null;
        }

        // 用於檢查使用者標誌
        private bool CheckUserFlags(DirectoryEntry user)
{
    try
    {
        object userFlagsValue = user.Properties["UserFlags"]!=null?user.Properties["UserFlags"].Value:"";
        if (userFlagsValue != null)
        {
            string userFlags = userFlagsValue.ToString();
            // 判斷使用者是否無法更改密碼（65536對應的標誌位）
            return userFlags.Contains("65536");
        }
    }
    catch
    {
        // 如果出現異常，返回 false
        return false;
    }
    return false;
}

    }
}
