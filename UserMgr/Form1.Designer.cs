namespace winusermgr
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLockOFF = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxMachine = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonGroups = new System.Windows.Forms.ToolStripButton();
            this.toolStripLockON = new System.Windows.Forms.ToolStripButton();
            this.dataGridUsers = new System.Windows.Forms.DataGridView();
            this.timerStopWaitAni = new System.Windows.Forms.Timer(this.components);
            this.timerWaitAni = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxBG = new System.Windows.Forms.PictureBox();
            this.labelWait = new System.Windows.Forms.Label();
            this.udName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udAccountExpires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDisabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udPasswordNeverExpires = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udUserMayNotChangePassword = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udGroups = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBG)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLockOFF,
            this.toolStripLabel1,
            this.toolStripComboBoxMachine,
            this.toolStripButtonReload,
            this.toolStripSeparator1,
            this.toolStripButtonGroups,
            this.toolStripLockON});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(980, 38);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "主机";
            // 
            // toolStripLockOFF
            // 
            this.toolStripLockOFF.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLockOFF.Enabled = false;
            this.toolStripLockOFF.Image = global::winusermgr.Properties.Resources.unlock;
            this.toolStripLockOFF.Name = "toolStripLockOFF";
            this.toolStripLockOFF.Size = new System.Drawing.Size(154, 25);
            this.toolStripLockOFF.Text = "已使用管理员身份";
            this.toolStripLockOFF.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::winusermgr.Properties.Resources.data_configuration;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 25);
            this.toolStripLabel1.Text = "主机:";
            // 
            // toolStripComboBoxMachine
            // 
            this.toolStripComboBoxMachine.Name = "toolStripComboBoxMachine";
            this.toolStripComboBoxMachine.Size = new System.Drawing.Size(150, 28);
            this.toolStripComboBoxMachine.Text = "localhost";
            // 
            // toolStripButtonReload
            // 
            this.toolStripButtonReload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReload.Image")));
            this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReload.Name = "toolStripButtonReload";
            this.toolStripButtonReload.Size = new System.Drawing.Size(82, 25);
            this.toolStripButtonReload.Text = "加载(&R)";
            this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonGroups
            // 
            this.toolStripButtonGroups.Image = global::winusermgr.Properties.Resources.data_sheet;
            this.toolStripButtonGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGroups.Name = "toolStripButtonGroups";
            this.toolStripButtonGroups.Size = new System.Drawing.Size(145, 25);
            this.toolStripButtonGroups.Text = "选择用户组列(&S)";
            this.toolStripButtonGroups.Click += new System.EventHandler(this.toolStripButtonGroups_Click);
            // 
            // toolStripLockON
            // 
            this.toolStripLockON.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLockON.ForeColor = System.Drawing.Color.Crimson;
            this.toolStripLockON.Image = global::winusermgr.Properties.Resources._lock;
            this.toolStripLockON.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLockON.Name = "toolStripLockON";
            this.toolStripLockON.Size = new System.Drawing.Size(179, 25);
            this.toolStripLockON.Text = "解锁不可用的设置(&A)";
            this.toolStripLockON.ToolTipText = "解锁设置(&U)";
            this.toolStripLockON.Click += new System.EventHandler(this.toolStripLockON_Click);
            // 
            // dataGridUsers
            // 
            this.dataGridUsers.AllowUserToAddRows = false;
            this.dataGridUsers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            this.dataGridUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridUsers.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dataGridUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.udName,
            this.udFullName,
            this.udDescription,
            this.udAccountExpires,
            this.udDisabled,
            this.udPasswordNeverExpires,
            this.udUserMayNotChangePassword,
            this.udGroups});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridUsers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridUsers.EnableHeadersVisualStyles = false;
            this.dataGridUsers.GridColor = System.Drawing.Color.SkyBlue;
            this.dataGridUsers.Location = new System.Drawing.Point(0, 37);
            this.dataGridUsers.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridUsers.Name = "dataGridUsers";
            this.dataGridUsers.ReadOnly = true;
            this.dataGridUsers.RowTemplate.Height = 40;
            this.dataGridUsers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridUsers.Size = new System.Drawing.Size(653, 664);
            this.dataGridUsers.TabIndex = 1;
            this.dataGridUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // timerStopWaitAni
            // 
            this.timerStopWaitAni.Interval = 1000;
            this.timerStopWaitAni.Tick += new System.EventHandler(this.timerStopWaitAni_Tick);
            // 
            // timerWaitAni
            // 
            this.timerWaitAni.Interval = 33;
            this.timerWaitAni.Tick += new System.EventHandler(this.timerWaitAni_Tick);
            // 
            // pictureBoxBG
            // 
            this.pictureBoxBG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBG.Location = new System.Drawing.Point(305, 37);
            this.pictureBoxBG.Name = "pictureBoxBG";
            this.pictureBoxBG.Size = new System.Drawing.Size(675, 664);
            this.pictureBoxBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBG.TabIndex = 3;
            this.pictureBoxBG.TabStop = false;
            // 
            // labelWait
            // 
            this.labelWait.BackColor = System.Drawing.Color.LightBlue;
            this.labelWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWait.Location = new System.Drawing.Point(0, 0);
            this.labelWait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWait.Name = "labelWait";
            this.labelWait.Size = new System.Drawing.Size(980, 701);
            this.labelWait.TabIndex = 2;
            this.labelWait.Text = "正在加载";
            this.labelWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // udName
            // 
            this.udName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.udName.HeaderText = "用户名";
            this.udName.Name = "udName";
            this.udName.ReadOnly = true;
            // 
            // udFullName
            // 
            this.udFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.udFullName.HeaderText = "全名";
            this.udFullName.Name = "udFullName";
            this.udFullName.ReadOnly = true;
            // 
            // udDescription
            // 
            this.udDescription.HeaderText = "描述";
            this.udDescription.Name = "udDescription";
            this.udDescription.ReadOnly = true;
            // 
            // udAccountExpires
            // 
            this.udAccountExpires.HeaderText = "有效期";
            this.udAccountExpires.Name = "udAccountExpires";
            this.udAccountExpires.ReadOnly = true;
            // 
            // udDisabled
            // 
            this.udDisabled.HeaderText = "禁用";
            this.udDisabled.Name = "udDisabled";
            this.udDisabled.ReadOnly = true;
            this.udDisabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udDisabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udDisabled.Width = 70;
            // 
            // udPasswordNeverExpires
            // 
            this.udPasswordNeverExpires.HeaderText = "不过期";
            this.udPasswordNeverExpires.Name = "udPasswordNeverExpires";
            this.udPasswordNeverExpires.ReadOnly = true;
            this.udPasswordNeverExpires.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udPasswordNeverExpires.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udPasswordNeverExpires.Width = 70;
            // 
            // udUserMayNotChangePassword
            // 
            this.udUserMayNotChangePassword.HeaderText = "禁改密";
            this.udUserMayNotChangePassword.Name = "udUserMayNotChangePassword";
            this.udUserMayNotChangePassword.ReadOnly = true;
            this.udUserMayNotChangePassword.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udUserMayNotChangePassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udUserMayNotChangePassword.Width = 70;
            // 
            // udGroups
            // 
            this.udGroups.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.udGroups.HeaderText = "用户组";
            this.udGroups.Name = "udGroups";
            this.udGroups.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(980, 701);
            this.Controls.Add(this.dataGridUsers);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.labelWait);
            this.Controls.Add(this.pictureBoxBG);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Form1";
            this.Text = "用户管理器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonReload;
        private System.Windows.Forms.DataGridView dataGridUsers;
        private System.Windows.Forms.Timer timerStopWaitAni;
        private System.Windows.Forms.Timer timerWaitAni;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMachine;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonGroups;
        private System.Windows.Forms.ToolStripButton toolStripLockON;
        private System.Windows.Forms.ToolStripLabel toolStripLockOFF;
        private System.Windows.Forms.PictureBox pictureBoxBG;
        private System.Windows.Forms.Label labelWait;
        private System.Windows.Forms.DataGridViewTextBoxColumn udName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn udAccountExpires;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udDisabled;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udPasswordNeverExpires;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udUserMayNotChangePassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn udGroups;
    }
}

