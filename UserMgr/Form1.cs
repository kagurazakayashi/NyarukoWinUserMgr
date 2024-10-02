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
using Slboot;

namespace winusermgr
{
    public partial class Form1 : Form
    {
        UserLoader userLoader = new UserLoader();
        SlbootAni slboot = new SlbootAni();
        public Form1()
        {
            InitializeComponent();
            toolStripButtonReload.Image = Shell32IconHelper.GetIconFromSysImageres(228).ToBitmap() ?? Shell32IconHelper.CreatePlaceholderBitmap();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            slboot.Init(20);
            if (slboot.aniFont != null)
            {
                labelWait.Font = slboot.aniFont;
            }
            string machineName = Environment.MachineName;
            toolStripComboBoxMachine.Items.Add(machineName);
            toolStripComboBoxMachine.Text = machineName;
            reloadData();
        }

        private void waitAni(bool enable)
        {
            toolStrip1.Enabled = !enable;
            UseWaitCursor = enable;
            labelWait.Visible = enable;
            dataGridUsers.Visible = !enable;
            if (enable && slboot.aniFont != null)
            {
                timerWaitAni.Enabled = enable;
            }
            else
            {
                timerWaitAni.Enabled = false;
                slboot.StopUpdateChar();
            }
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
            waitAni(true);
            // 載入本地使用者資訊
            userLoader.GetLocalUsers(toolStripComboBoxMachine.Text);
            // 檢查是否發生錯誤
            if (userLoader.users.Count == 1 && userLoader.users[0].ErrorInfo.Length > 0)
            {
                MessageBox.Show(userLoader.users[0].ErrorInfo, "加载用户信息失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridUsers.Rows.Clear();
            Text = toolStripComboBoxMachine.Text + " 的用户目录";
            foreach (UserInfo user in userLoader.users)
            {
                // $"⭐ Name: {user.Name}, FullName: {user.FullName}, Description: {user.Description}, AccountExpires: {user.AccountExpires}, Disabled: {user.Disabled}, PasswordNeverExpires: {user.PasswordNeverExpires}, UserMayNotChangePassword: {user.UserMayNotChangePassword}, Group: {string.Join(", ", user.Groups)}\n";
                dataGridUsers.Rows.Add(user.Name, user.FullName, user.Description, user.AccountExpires, user.Disabled, user.PasswordNeverExpires, user.UserMayNotChangePassword, string.Join(", ", user.Groups));
            }
            timerStopWaitAni.Enabled = true;
        }

        private void timerWaitAni_Tick(object sender, EventArgs e)
        {
            labelWait.Text = slboot.UpdateChar();
        }

        private void timerStopWaitAni_Tick(object sender, EventArgs e)
        {
            waitAni(false);
        }
    }
}
