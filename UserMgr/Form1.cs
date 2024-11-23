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
        private UserLoader userLoader = new UserLoader();
        private SlbootAni slboot = new SlbootAni();
        private int timerStopWaitAniEndMode = 0;
        private string linkMachineName = Environment.MachineName;
        private const int MoeWidth = 1500;

        public Form1()
        {
            InitializeComponent();
            //toolStripButtonReload.Image = Shell32IconHelper.GetBitmapFromSysImageres(176);
            //toolStripButtonGroups.Image = Shell32IconHelper.GetBitmapFromSysImageres(251);
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            if (screenWidth >= MoeWidth)
            {
                this.Width = MoeWidth;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AdjustPictureBox();
            bool isAdmin = UAC.IsRunAsAdministrator();
            toolStripLockON.Visible = !isAdmin;
            toolStripLockOFF.Visible = isAdmin;
            dataGridUsers.ReadOnly = !isAdmin;
            dataGridUsers.AllowUserToAddRows = isAdmin;
            dataGridUsers.AllowUserToDeleteRows = isAdmin;
            slboot.Init(20);
            if (slboot.aniFont != null)
            {
                labelWait.Font = slboot.aniFont;
            }
            toolStripComboBoxMachine.Items.Add(linkMachineName);
            toolStripComboBoxMachine.Text = linkMachineName;
            reloadData();
        }

        private void waitAni(bool enable)
        {
            if (!enable && timerStopWaitAniEndMode == -1)
            {
                return;
            }
            if (!enable && timerStopWaitAniEndMode == 1)
            {
                Application.Exit();
            }
            toolStrip1.Enabled = !enable;
            UseWaitCursor = enable;
            labelWait.Visible = enable;
            dataGridUsers.Visible = !enable;
            if (enable && slboot.aniFont != null)
            {
                timerWaitAni.Enabled = enable;
                timerStopWaitAni.Enabled = enable;
            }
            else
            {
                timerWaitAni.Enabled = false;
                timerStopWaitAni.Enabled = false;
                slboot.StopUpdateChar();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            linkMachineName = toolStripComboBoxMachine.Text;
            reloadData();
        }

        private void reloadData()
        {
            waitAni(true);
            // 載入本地使用者資訊
            userLoader.GetLocalUsers(linkMachineName);
            // 檢查是否發生錯誤
            if (userLoader.users.Count == 1 && userLoader.users[0].ErrorInfo.Length > 0)
            {
                MessageBox.Show(userLoader.users[0].ErrorInfo, "加载用户信息失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                linkMachineName = Environment.MachineName;
                userLoader.GetLocalUsers(linkMachineName);
            }
            dataGridUsers.Rows.Clear();
            Text = linkMachineName + " 的用户目录";
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

        private void toolStripButtonGroups_Click(object sender, EventArgs e)
        {
            FormGroupSelect formGroupSelect = new FormGroupSelect(linkMachineName);
            if (formGroupSelect.ShowDialog() == DialogResult.OK)
            {
                reloadData();
            }
        }

        private void toolStripLockON_Click(object sender, EventArgs e)
        {
            timerStopWaitAniEndMode = -1;
            waitAni(true);
            if (UAC.RestartAsAdministrator())
            {
                timerStopWaitAni.Interval = 3000;
                timerStopWaitAniEndMode = 1;
            }
            else
            {
                timerStopWaitAniEndMode = 0;
                waitAni(false);
            }
        }

        private void AdjustPictureBox()
        {
            int clientHeight = this.ClientSize.Height;
            int clientWidth = this.ClientSize.Width;
            pictureBoxBG.Height = clientHeight;
            pictureBoxBG.Width = clientHeight;
            pictureBoxBG.Left = this.ClientSize.Width - pictureBoxBG.Width;
            if (this.Width < MoeWidth)
            {
                dataGridUsers.Width = clientWidth;
                if (pictureBoxBG.Image == null)
                {
                    pictureBoxBG.Dispose();
                    pictureBoxBG.Image = null;
                }
            }
            else
            {
                dataGridUsers.Width = (int)(clientWidth * 0.8);
                if (pictureBoxBG.Image == null)
                {
                    pictureBoxBG.Image = Properties.Resources.app1;
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustPictureBox();
        }
    }
}
