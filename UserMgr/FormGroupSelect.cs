using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace winusermgr
{
    public partial class FormGroupSelect : Form
    {
        Bitmap flippedBitmap;
        Bitmap cancelBitmap;
        public FormGroupSelect()
        {
            InitializeComponent();
            Bitmap bitmap = Shell32IconHelper.GetBitmapFromSysImageres(137);
            buttonAdd.Image = bitmap;
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            flippedBitmap = new Bitmap(bitmap);
            flippedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            buttonRemove.Image = flippedBitmap;
            buttonOK.Image = Shell32IconHelper.GetBitmapFromSysImageres(301);
            cancelBitmap = Shell32IconHelper.GetBitmapFromSysImageres(131);
            buttonCancel.Image = cancelBitmap;
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
    }
}
