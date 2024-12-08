namespace WinUserMgr
{
    partial class ConfirmWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOK = new System.Windows.Forms.ToolStripButton();
            this.listBoxTasks = new System.Windows.Forms.ListBox();
            this.toolStripLabelStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOK,
            this.toolStripLabelStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 403);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(984, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "主机";
            // 
            // toolStripButtonOK
            // 
            this.toolStripButtonOK.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonOK.Image = global::WinUserMgr.Properties.Resources.flash_on;
            this.toolStripButtonOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOK.Name = "toolStripButtonOK";
            this.toolStripButtonOK.Size = new System.Drawing.Size(113, 25);
            this.toolStripButtonOK.Text = "开始执行(&E)";
            this.toolStripButtonOK.Click += new System.EventHandler(this.toolStripButtonOK_Click);
            // 
            // listBoxTasks
            // 
            this.listBoxTasks.BackColor = System.Drawing.Color.Azure;
            this.listBoxTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTasks.FormattingEnabled = true;
            this.listBoxTasks.ItemHeight = 21;
            this.listBoxTasks.Location = new System.Drawing.Point(0, 0);
            this.listBoxTasks.Margin = new System.Windows.Forms.Padding(10);
            this.listBoxTasks.Name = "listBoxTasks";
            this.listBoxTasks.ScrollAlwaysVisible = true;
            this.listBoxTasks.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxTasks.Size = new System.Drawing.Size(984, 403);
            this.listBoxTasks.TabIndex = 2;
            // 
            // toolStripLabelStatus
            // 
            this.toolStripLabelStatus.Name = "toolStripLabelStatus";
            this.toolStripLabelStatus.Size = new System.Drawing.Size(0, 25);
            // 
            // ConfirmWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 441);
            this.Controls.Add(this.listBoxTasks);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimizeBox = false;
            this.Name = "ConfirmWindow";
            this.Text = "任务确认（实时更新）";
            this.Load += new System.EventHandler(this.ConfirmWindow_Load);
            this.SizeChanged += new System.EventHandler(this.ConfirmWindow_SizeChanged);
            this.Move += new System.EventHandler(this.ConfirmWindow_Move);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOK;
        public System.Windows.Forms.ListBox listBoxTasks;
        public System.Windows.Forms.ToolStripLabel toolStripLabelStatus;
    }
}