using System.Collections.Generic;

namespace UserInfoType
{
    /// <summary>
    /// 表示使用者資訊的類。
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 獲取或設定使用者的名稱。
        /// </summary>
        public string Name { get; set; } // 使用者的名稱

        /// <summary>
        /// 獲取或設定使用者的全名。
        /// </summary>
        public string FullName { get; set; } // 使用者的全名

        /// <summary>
        /// 獲取或設定使用者的描述資訊。
        /// </summary>
        public string Description { get; set; } // 使用者的描述資訊

        /// <summary>
        /// 獲取或設定使用者賬號的過期時間。
        /// </summary>
        public string AccountExpires { get; set; } // 使用者賬號的過期時間

        /// <summary>
        /// 獲取或設定一個值，該值指示使用者賬號是否被停用。
        /// </summary>
        public bool Disabled { get; set; } // 是否停用賬號

        /// <summary>
        /// 獲取或設定一個值，該值指示使用者密碼是否永不過期。
        /// </summary>
        public bool PasswordNeverExpires { get; set; } // 密碼是否永不過期

        /// <summary>
        /// 獲取或設定一個值，該值指示使用者是否可以更改密碼。
        /// </summary>
        public bool UserMayNotChangePassword { get; set; } // 使用者是否可以更改密碼

        /// <summary>
        /// 獲取或設定使用者所屬的組列表。
        /// </summary>
        public List<string> Groups { get; set; } // 使用者所屬的組列表

        /// <summary>
        /// 獲取或設定使用者的錯誤資訊。
        /// </summary>
        public string ErrorInfo { get; set; } // 使用者的錯誤資訊
    }
}
