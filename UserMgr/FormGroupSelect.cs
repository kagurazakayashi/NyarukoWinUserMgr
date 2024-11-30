using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using static System.Windows.Forms.ListBox;
using UserInfoLoader;
using System.Collections;
using System.Threading;
using System.Reflection;

namespace winusermgr
{
    public partial class FormGroupSelect : Form
    {
        private Bitmap flippedBitmap;
        private string[] systemGroups;
        private INI iniconf;
        private string linkMachineName = Environment.MachineName;
        private string[] listBoxSystemGroupStartItems = null;
        private string[] listBoxSelectedGroupStartItems = null;
        private bool closeNow = false;
        private bool isDragging = false;
        private SlbootAni slboot = new SlbootAni();
        private int timerStopWaitAniEndMode = 0;
        private FormWindowState winState;

        public FormGroupSelect(string linkMachineName = "")
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(linkMachineName))
            {
                this.linkMachineName = linkMachineName;
            }
            iniconf = new INI(Application.StartupPath + "\\config.ini");
            //Bitmap bitmap = Shell32IconHelper.GetBitmapFromSysImageres(137);
            //buttonAdd.Image = bitmap;
            //bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            flippedBitmap = new Bitmap(buttonAdd.Image);
            flippedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            buttonRemove.Image = flippedBitmap;
            //buttonOK.Image = Shell32IconHelper.GetBitmapFromSysImageres(301);
            //cancelBitmap = Shell32IconHelper.GetBitmapFromSysImageres(131);
            //buttonCancel.Image = cancelBitmap;
            //Icon = Shell32IconHelper.GetIconFromSysImageres(111);
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            closeNow = true;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string[] removedGroups = chkSystemGroup();
            if (removedGroups.Length > 0)
            {
                if (MessageBox.Show($"有已经被移除的虚拟组名。\n如果这些虚拟组名不包含在此电脑的用户组列表中，它将被永久删除！\n以下是要永久删除的组名:\n\n{string.Join("\n", removedGroups)}\n\n是否继续？", $"永久删除 {removedGroups.Length} 个虚拟组名", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    if (e is FormClosingEventArgs)
                    {
                        closeNow = false;
                        (e as FormClosingEventArgs).Cancel = true;
                    }
                    return;
                }
            }
            if (saveConfig())
            {
                DialogResult = DialogResult.OK;
                if (!(e is FormClosingEventArgs))
                {
                    closeNow = true;
                    Close();
                }
            }
        }

        private bool saveConfig()
        {
            try
            {
                iniconf.IniWriteValue("Config", "GroupsCount", listBoxSelectedGroup.Items.Count.ToString());
                for (int i = 0; i < listBoxSelectedGroup.Items.Count; i++)
                {
                    iniconf.IniWriteValue("Groups", $"G{i}", listBoxSelectedGroup.Items[i].ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "配置写入失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    return saveConfig();
                }
            }
            return false;
        }

        private void loadConfig()
        {
            if (!iniconf.ExistINIFile())
            {
                return;
            }
            try
            {
                int count = int.Parse(iniconf.IniReadValue("Config", "GroupsCount"));
                for (int i = 0; i < count; i++)
                {
                    listBoxSelectedGroup.Items.Add(iniconf.IniReadValue("Groups", $"G{i}"));
                }
                RemoveDuplicateItems();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "配置读取失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    loadConfig();
                }
            }
        }

        private void FormGroupSelect_Load(object sender, EventArgs e)
        {
            slboot.Init(20);
            if (slboot.aniFont != null)
            {
                labelWait.Font = slboot.aniFont;
            }
            Thread thread = new Thread(getUserGroup);
            thread.IsBackground = true;
            thread.Start();
        }

        private void getUserGroup()
        {
            string[][] groups = UserLoader.GetGroups(linkMachineName);
            this.Invoke((Action)(() =>
            {
                if (groups.Length == 0 || (groups.Length == 1 && groups[0].Length >= 1 && groups[0][0] == "%E%"))
                {
                    string errinfo = "无法获取系统用户组列表";
                    if (groups.Length == 1 && groups[0].Length >= 1 && groups[0][0].Length >= 2)
                    {
                        errinfo = groups[0][1];
                    }
                    MessageBox.Show(errinfo, "无法获取系统用户组列表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    closeNow = true;
                    timerWaitAni.Enabled = false;
                    this.Close();
                    return;
                }
                foreach (var group in groups)
                {
                    listBoxSystemGroup.Items.Add(group[1]);
                    //Console.WriteLine($"Group Name: {group[1]}, Domain: {group[0]}");
                }
                systemGroups = new string[listBoxSystemGroup.Items.Count];
                listBoxSystemGroup.Items.CopyTo(systemGroups, 0);
                loadConfig();
                listBoxSystemGroupStartItems = listBoxSystemGroup.Items.Cast<string>().ToArray();
                listBoxSelectedGroupStartItems = listBoxSelectedGroup.Items.Cast<string>().ToArray();
                chkChange();
                timerStopWaitAni.Enabled = true;
            }));
        }

        private bool chkChange()
        {
            string[] listBoxSystemGroupNowItems = listBoxSystemGroup.Items.Cast<string>().ToArray();
            string[] listBoxSelectedGroupNowItems = listBoxSelectedGroup.Items.Cast<string>().ToArray();
            bool listBoxSystemGroupEqual = listBoxSystemGroupStartItems.SequenceEqual(listBoxSystemGroupNowItems);
            bool listBoxSelectedGroupEqual = listBoxSelectedGroupStartItems.SequenceEqual(listBoxSelectedGroupNowItems);
            bool allEqual = listBoxSystemGroupEqual && listBoxSelectedGroupEqual;
            buttonOK.Enabled = !allEqual;
            return allEqual;
        }

        private string[] chkSystemGroup()
        {
            List<string> list = new List<string>();
            foreach (var item in listBoxSystemGroup.Items)
            {
                if (!systemGroups.Contains(item.ToString()))
                {
                    list.Add(item.ToString());
                }
            }
            return list.ToArray();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBoxSystemGroup.SelectedIndex == -1)
            {
                return;
            }
            int nowSelectedIndex = listBoxSystemGroup.SelectedIndex;
            List<Object> selectedObjectCollection = listBoxSystemGroup.SelectedItems.Cast<object>().ToList();
            listBoxSelectedGroup.SelectedItems.Clear();
            foreach (var item in selectedObjectCollection)
            {
                listBoxSelectedGroup.Items.Add(item);
                listBoxSelectedGroup.SelectedItems.Add(item);
                listBoxSystemGroup.Items.Remove(item);
            }
            if (listBoxSystemGroup.Items.Count > 0 && nowSelectedIndex >= 0)
            {
                if (nowSelectedIndex >= listBoxSystemGroup.Items.Count)
                {
                    listBoxSystemGroup.SelectedIndex = listBoxSystemGroup.Items.Count - 1;
                }
                else
                {
                    listBoxSystemGroup.SelectedIndex = nowSelectedIndex;
                }
            }
            chkBtnEnable();
            chkChange();
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedGroup.SelectedIndex == -1)
            {
                return;
            }
            int nowSelectedIndex = listBoxSelectedGroup.SelectedIndex;
            List<Object> selectedObjectCollection = listBoxSelectedGroup.SelectedItems.Cast<object>().ToList();
            listBoxSystemGroup.SelectedItems.Clear();
            foreach (var item in selectedObjectCollection)
            {
                listBoxSystemGroup.Items.Add(item);
                listBoxSystemGroup.SelectedItems.Add(item);
                listBoxSelectedGroup.Items.Remove(item);
            }
            if (listBoxSelectedGroup.Items.Count > 0 && nowSelectedIndex >= 0)
            {
                if (nowSelectedIndex >= listBoxSelectedGroup.Items.Count)
                {
                    listBoxSelectedGroup.SelectedIndex = listBoxSelectedGroup.Items.Count - 1;
                }
                else
                {
                    listBoxSelectedGroup.SelectedIndex = nowSelectedIndex;
                }
            }
            chkBtnEnable();
            chkChange();
        }

        private void listBoxSystemGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkBtnEnable();
        }
        private void listBoxSelectedGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkBtnEnable();
        }


        private void chkBtnEnable()
        {
            buttonRemove.Enabled = listBoxSelectedGroup.SelectedIndex != -1;
            buttonAdd.Enabled = listBoxSystemGroup.SelectedIndex != -1;
        }

        private void textBoxCus_TextChanged(object sender, EventArgs e)
        {
            buttonAddCustom.Enabled = textBoxCustom.Text.Length > 0;
        }
        private void RemoveDuplicateItems()
        {
            for (int i = listBoxSelectedGroup.Items.Count - 1; i >= 0; i--)
            {
                for (int j = listBoxSystemGroup.Items.Count - 1; j >= 0; j--)
                {
                    if (listBoxSelectedGroup.Items[i].ToString() == listBoxSystemGroup.Items[j].ToString())
                    {
                        listBoxSystemGroup.Items.RemoveAt(j);
                    }
                }
            }
        }

        private void buttonAddCustom_Click(object sender, EventArgs e)
        {
            listBoxSelectedGroup.Items.Add(textBoxCustom.Text);
            textBoxCustom.Text = "";
            RemoveDuplicateItems();
            chkBtnEnable();
            chkChange();
        }

        private void FormGroupSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeNow)
            {
                e.Cancel = false;
                return;
            }
            if (buttonOK.Enabled)
            {
                DialogResult = MessageBox.Show("更改尚未保存，是否要保存更改？", "更改未保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    closeNow = true;
                    buttonOK_Click(sender, e);
                }
                else if (DialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void textBoxCustom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '-' &&
                e.KeyChar != '_' &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void timerStopWaitAni_Tick(object sender, EventArgs e)
        {
            waitAni(false);
        }

        private void timerWaitAni_Tick(object sender, EventArgs e)
        {
            labelWait.Text = slboot.UpdateChar();
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
            UseWaitCursor = enable;
            labelWait.Visible = enable;
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
    }
}
