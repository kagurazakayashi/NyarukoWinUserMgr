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
using System.Threading;
using System.Reflection;

namespace winusermgr
{
    public partial class Form1 : Form
    {
        private UserLoader userLoader = new UserLoader();
        private SlbootAni slboot = new SlbootAni();
        private int timerStopWaitAniEndMode = 0;
        private string linkMachineName = Environment.MachineName;
        private const int WidthMoe = 1300;
        private const int WidthNormal = 800;
        private bool resizeing = false;
        private FormWindowState winState;
        private bool isDragging = false;

        public Form1()
        {
            InitializeComponent();
            winState = WindowState;
            //toolStripButtonReload.Image = Shell32IconHelper.GetBitmapFromSysImageres(176);
            //toolStripButtonGroups.Image = Shell32IconHelper.GetBitmapFromSysImageres(251);
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            if (screenWidth < WidthMoe)
            {
                this.Width = WidthNormal;
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0112:
                    if ((m.WParam.ToInt32() & 0xFFF0) == 0xF010)
                    {
                        isDragging = true;
                        Opacity = 0.6;
                    }
                    break;
                //case 0x0216: Dragging
                case 0x0232:
                    if (isDragging)
                    {
                        isDragging = false;
                        Opacity = 1;
                    }
                    break;
            }
            if (WindowState != FormWindowState.Normal)
            {
                Opacity = 1;
            }
            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AdjustPictureBox(true);
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
            Thread thread = new Thread(reloadDataThread);
            thread.IsBackground = true;
            thread.Start();
        }

        private void reloadDataThread()
        {
            // 載入本地使用者資訊
            userLoader.GetLocalUsers(linkMachineName);
            this.Invoke((Action)(() =>
            {
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
            }));
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
            //FormGroupSelect formGroupSelect = new FormGroupSelect(toolStripComboBoxMachine.Text); //DEBUG
            if (formGroupSelect.ShowDialog() == DialogResult.OK)
            {
                reloadData();
            }
        }

        private void toolStripLockON_Click(object sender, EventArgs e)
        {
            timerStopWaitAniEndMode = -1;
            waitAni(true);
            Thread thread = new Thread(getAdmin);
            thread.IsBackground = true;
            thread.Start();
        }
        private void getAdmin()
        {
            bool toAdmin = UAC.RestartAsAdministrator();
            this.Invoke((Action)(() =>
            {
                if (toAdmin)
                {
                    timerStopWaitAni.Interval = 3000;
                    timerStopWaitAniEndMode = 1;
                }
                else
                {
                    timerStopWaitAniEndMode = 0;
                    waitAni(false);
                }
            }));
        }

        private void AdjustPictureBox(bool loadImg = true)
        {
            int clientHeight = this.ClientSize.Height;
            int clientWidth = this.ClientSize.Width;
            pictureBoxBG.Height = clientHeight;
            pictureBoxBG.Width = clientHeight;
            pictureBoxBG.Left = this.ClientSize.Width - pictureBoxBG.Width;
            if (this.Width < WidthMoe)
            {
                dataGridUsers.Width = clientWidth;
                if (pictureBoxBG.Image != null)
                {
                    pictureBoxBG.Image = null;
                    pictureBoxBG.Visible = false;
                }
            }
            else
            {
                dataGridUsers.Width = (int)(clientWidth * 0.8);
                if (loadImg && pictureBoxBG.Image == null)
                {
                    pictureBoxBG.Image = Properties.Resources.app1;
                    pictureBoxBG.Visible = true;
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            AdjustPictureBox();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBoxBG.Dispose();
            pictureBoxBG = null;
        }
    }
}
