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

namespace winusermgr
{
    public partial class FormGroupSelect : Form
    {
        Bitmap flippedBitmap;
        string[] systemGroups;

        public FormGroupSelect()
        {
            InitializeComponent();
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
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormGroupSelect_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Group");

            foreach (ManagementObject group in searcher.Get())
            {
                listBoxSystemGroup.Items.Add(group["Name"]);
                //Console.WriteLine($"Group Name: {group["Name"]}, Domain: {group["Domain"]}");
            }
            systemGroups = new string[listBoxSystemGroup.Items.Count];
            listBoxSystemGroup.Items.CopyTo(systemGroups, 0);
            chkChange();
        }

        private void chkChange()
        {
            if (listBoxSystemGroup.Items.Count != systemGroups.Length)
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
    }
}
