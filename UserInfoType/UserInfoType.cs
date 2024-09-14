using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserInfoType
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string AccountExpires { get; set; }
        public bool Disabled { get; set; }
        public bool PasswordNeverExpires { get; set; }
        public bool UserMayNotChangePassword { get; set; }
        public List<string> Groups { get; set; }
    }
}
