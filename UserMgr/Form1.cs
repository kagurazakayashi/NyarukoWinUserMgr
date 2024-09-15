using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UserInfoType;
using UserInfoLoader;

namespace winusermgr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 載入本地使用者資訊
            List<UserInfo> users = UserLoader.GetLocalUsers();
            // 檢查是否發生錯誤
            if (users.Count == 1 && users[0].ErrorInfo.Length > 0)
            {
                MessageBox.Show(users[0].ErrorInfo, "加载用户信息失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // DEBUG
            string infoStr = "";
            foreach (UserInfo user in users)
            {
                infoStr += $"⭐ Name: {user.Name}, FullName: {user.FullName}, Description: {user.Description}, AccountExpires: {user.AccountExpires}, Disabled: {user.Disabled}, PasswordNeverExpires: {user.PasswordNeverExpires}, UserMayNotChangePassword: {user.UserMayNotChangePassword}, Group: {string.Join(", ", user.Groups)}\n";
            }
            MessageBox.Show(infoStr, "本地账户资料", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
