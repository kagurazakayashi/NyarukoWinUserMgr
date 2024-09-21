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
        UserLoader userLoader = new UserLoader();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reloadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            reloadData();
        }

        private void reloadData()
        {
            // 載入本地使用者資訊
            userLoader.GetLocalUsers();
            // 檢查是否發生錯誤
            if (userLoader.users.Count == 1 && userLoader.users[0].ErrorInfo.Length > 0)
            {
                MessageBox.Show(userLoader.users[0].ErrorInfo, "加载用户信息失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridUsers.Rows.Clear();
            foreach (UserInfo user in userLoader.users)
            {
                // $"⭐ Name: {user.Name}, FullName: {user.FullName}, Description: {user.Description}, AccountExpires: {user.AccountExpires}, Disabled: {user.Disabled}, PasswordNeverExpires: {user.PasswordNeverExpires}, UserMayNotChangePassword: {user.UserMayNotChangePassword}, Group: {string.Join(", ", user.Groups)}\n";
                dataGridUsers.Rows.Add(user.Name, user.FullName, user.Description, user.AccountExpires, user.Disabled, user.PasswordNeverExpires, user.UserMayNotChangePassword, string.Join(", ", user.Groups));
            }
        }
    }
}
