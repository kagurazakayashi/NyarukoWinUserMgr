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

namespace winusermgr
{
    public partial class FormGroupSelect : Form
    {
        private Bitmap flippedBitmap;
        private string[] systemGroups;
        private INI iniconf;
        private string linkMachineName = Environment.MachineName;

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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string[] removedGroups = chkSystemGroup();
            if (removedGroups.Length > 0)
            {
                if (MessageBox.Show($"有已经被移除的虚拟组名。\n如果这些虚拟组名不包含在此电脑的用户组列表中，它将被永久删除！\n以下是要永久删除的组名:\n\n{string.Join("\n", removedGroups)}\n\n是否继续？", $"永久删除 {removedGroups.Length} 个虚拟组名", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                if (saveConfig())
                {
                    DialogResult = DialogResult.OK;
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
            string[][] groups = UserLoader.GetGroups(linkMachineName);
            if (groups.Length == 0 || (groups.Length == 1 && groups[0].Length >= 1 && groups[0][0] == "%E%"))
            {
                string errinfo = "无法获取系统用户组列表";
                if (groups.Length == 1 && groups[0].Length >= 1 && groups[0][0].Length >= 2)
                {
                    errinfo = groups[0][1];
                }
                MessageBox.Show(errinfo, "无法获取系统用户组列表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
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
            chkChange();
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

        private void chkChange()
        {
            if (listBoxSystemGroup.Items.Count != systemGroups.Length || chkSystemGroup().Length > 0)
            {
                buttonOK.Enabled = true;
                return;
            }
            else
            {
                foreach (var item in listBoxSystemGroup.Items)
                {
                    if (!systemGroups.Contains(item.ToString()))
                    {
                        buttonOK.Enabled = true;
                        return;
                    }
                }
                foreach (var item in listBoxSelectedGroup.Items)
                {
                    if (!systemGroups.Contains(item.ToString()))
                    {
                        buttonOK.Enabled = true;
                        return;
                    }
                }
            }
            buttonOK.Enabled = false;
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
    }
}
