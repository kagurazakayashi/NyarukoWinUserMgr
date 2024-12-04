namespace WinUserMgr
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxMachine = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridUsers = new System.Windows.Forms.DataGridView();
            this.timerStopWaitAni = new System.Windows.Forms.Timer(this.components);
            this.timerWaitAni = new System.Windows.Forms.Timer(this.components);
            this.labelWait = new System.Windows.Forms.Label();
            this.timerOpenGroup = new System.Windows.Forms.Timer(this.components);
            this.udName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udAccountExpires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chpwd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDisabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udPasswordNeverExpires = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udUserMayNotChangePassword = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AllGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxPwdCount = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxPwdType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLockOFF = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGroups = new System.Windows.Forms.ToolStripButton();
            this.toolStripLockON = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPWGen = new System.Windows.Forms.ToolStripButton();
            this.pictureBoxBG = new System.Windows.Forms.PictureBox();
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
            this.toolStripLockON,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripComboBoxPwdCount,
            this.toolStripLabel3,
            this.toolStripComboBoxPwdType,
            this.toolStripButtonPWGen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1284, 38);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "主机";
            // 
            // toolStripComboBoxMachine
            // 
            this.toolStripComboBoxMachine.Name = "toolStripComboBoxMachine";
            this.toolStripComboBoxMachine.Size = new System.Drawing.Size(150, 28);
            this.toolStripComboBoxMachine.Text = "localhost";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // dataGridUsers
            // 
            this.dataGridUsers.AllowUserToAddRows = false;
            this.dataGridUsers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightBlue;
            this.dataGridUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridUsers.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dataGridUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.udName,
            this.udFullName,
            this.udDescription,
            this.udAccountExpires,
            this.chpwd,
            this.udDisabled,
            this.udPasswordNeverExpires,
            this.udUserMayNotChangePassword,
            this.AllGroup});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridUsers.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridUsers.EnableHeadersVisualStyles = false;
            this.dataGridUsers.GridColor = System.Drawing.Color.SkyBlue;
            this.dataGridUsers.Location = new System.Drawing.Point(0, 37);
            this.dataGridUsers.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridUsers.Name = "dataGridUsers";
            this.dataGridUsers.ReadOnly = true;
            this.dataGridUsers.RowTemplate.Height = 40;
            this.dataGridUsers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridUsers.Size = new System.Drawing.Size(957, 524);
            this.dataGridUsers.TabIndex = 1;
            this.dataGridUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridUsers.CurrentCellChanged += new System.EventHandler(this.dataGridUsers_CurrentCellChanged);
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
            // labelWait
            // 
            this.labelWait.BackColor = System.Drawing.Color.LightBlue;
            this.labelWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWait.Location = new System.Drawing.Point(0, 0);
            this.labelWait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWait.Name = "labelWait";
            this.labelWait.Size = new System.Drawing.Size(1284, 561);
            this.labelWait.TabIndex = 2;
            this.labelWait.Text = "正在加载";
            this.labelWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelWait.UseWaitCursor = true;
            // 
            // timerOpenGroup
            // 
            this.timerOpenGroup.Interval = 3000;
            this.timerOpenGroup.Tick += new System.EventHandler(this.timerOpenGroup_Tick);
            // 
            // udName
            // 
            this.udName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.udName.HeaderText = "用户名";
            this.udName.Name = "udName";
            this.udName.ReadOnly = true;
            this.udName.Width = 83;
            // 
            // udFullName
            // 
            this.udFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.udFullName.HeaderText = "全名";
            this.udFullName.Name = "udFullName";
            this.udFullName.ReadOnly = true;
            this.udFullName.Width = 67;
            // 
            // udDescription
            // 
            this.udDescription.HeaderText = "描述";
            this.udDescription.Name = "udDescription";
            this.udDescription.ReadOnly = true;
            // 
            // udAccountExpires
            // 
            this.udAccountExpires.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.udAccountExpires.HeaderText = "有效期";
            this.udAccountExpires.Name = "udAccountExpires";
            this.udAccountExpires.ReadOnly = true;
            this.udAccountExpires.Width = 83;
            // 
            // chpwd
            // 
            this.chpwd.HeaderText = "改密";
            this.chpwd.Name = "chpwd";
            this.chpwd.ReadOnly = true;
            this.chpwd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chpwd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chpwd.Width = 80;
            // 
            // udDisabled
            // 
            this.udDisabled.HeaderText = "禁用";
            this.udDisabled.Name = "udDisabled";
            this.udDisabled.ReadOnly = true;
            this.udDisabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udDisabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udDisabled.Width = 80;
            // 
            // udPasswordNeverExpires
            // 
            this.udPasswordNeverExpires.HeaderText = "不过期";
            this.udPasswordNeverExpires.Name = "udPasswordNeverExpires";
            this.udPasswordNeverExpires.ReadOnly = true;
            this.udPasswordNeverExpires.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udPasswordNeverExpires.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udPasswordNeverExpires.Width = 80;
            // 
            // udUserMayNotChangePassword
            // 
            this.udUserMayNotChangePassword.HeaderText = "禁改密";
            this.udUserMayNotChangePassword.Name = "udUserMayNotChangePassword";
            this.udUserMayNotChangePassword.ReadOnly = true;
            this.udUserMayNotChangePassword.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udUserMayNotChangePassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udUserMayNotChangePassword.Width = 80;
            // 
            // AllGroup
            // 
            this.AllGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.AllGroup.HeaderText = "所属全部用户组";
            this.AllGroup.Name = "AllGroup";
            this.AllGroup.ReadOnly = true;
            this.AllGroup.Width = 147;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 25);
            this.toolStripLabel2.Text = "密码";
            // 
            // toolStripComboBoxPwdCount
            // 
            this.toolStripComboBoxPwdCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPwdCount.Name = "toolStripComboBoxPwdCount";
            this.toolStripComboBoxPwdCount.Size = new System.Drawing.Size(75, 28);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(26, 25);
            this.toolStripLabel3.Text = "位";
            // 
            // toolStripComboBoxPwdType
            // 
            this.toolStripComboBoxPwdType.Items.AddRange(new object[] {
            "a-z,0-9",
            "A-Z,a-z,0-9",
            "A-Z,a-z,0-9,sym"});
            this.toolStripComboBoxPwdType.Name = "toolStripComboBoxPwdType";
            this.toolStripComboBoxPwdType.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripLockOFF
            // 
            this.toolStripLockOFF.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLockOFF.Enabled = false;
            this.toolStripLockOFF.Image = global::WinUserMgr.Properties.Resources.unlock;
            this.toolStripLockOFF.Name = "toolStripLockOFF";
            this.toolStripLockOFF.Size = new System.Drawing.Size(154, 25);
            this.toolStripLockOFF.Text = "已使用管理员身份";
            this.toolStripLockOFF.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::WinUserMgr.Properties.Resources.data_configuration;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 25);
            this.toolStripLabel1.Text = "主机:";
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
            // toolStripButtonGroups
            // 
            this.toolStripButtonGroups.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGroups.Image")));
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
            this.toolStripLockON.Image = global::WinUserMgr.Properties.Resources._lock;
            this.toolStripLockON.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLockON.Name = "toolStripLockON";
            this.toolStripLockON.Size = new System.Drawing.Size(179, 25);
            this.toolStripLockON.Text = "解锁不可用的设置(&A)";
            this.toolStripLockON.ToolTipText = "解锁设置(&U)";
            this.toolStripLockON.Click += new System.EventHandler(this.toolStripLockON_Click);
            // 
            // toolStripButtonPWGen
            // 
            this.toolStripButtonPWGen.Enabled = false;
            this.toolStripButtonPWGen.Image = global::WinUserMgr.Properties.Resources.key;
            this.toolStripButtonPWGen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPWGen.Name = "toolStripButtonPWGen";
            this.toolStripButtonPWGen.Size = new System.Drawing.Size(94, 25);
            this.toolStripButtonPWGen.Text = "建立密码";
            this.toolStripButtonPWGen.Click += new System.EventHandler(this.toolStripButtonPWGen_Click);
            // 
            // pictureBoxBG
            // 
            this.pictureBoxBG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBG.BackColor = System.Drawing.Color.LightBlue;
            this.pictureBoxBG.Location = new System.Drawing.Point(609, 37);
            this.pictureBoxBG.Name = "pictureBoxBG";
            this.pictureBoxBG.Size = new System.Drawing.Size(675, 524);
            this.pictureBoxBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBG.TabIndex = 3;
            this.pictureBoxBG.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.dataGridUsers);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.labelWait);
            this.Controls.Add(this.pictureBoxBG);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MainWindow";
            this.Text = "用户管理器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Move += new System.EventHandler(this.MainWindow_Move);
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
        private System.Windows.Forms.Timer timerOpenGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn udName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn udAccountExpires;
        private System.Windows.Forms.DataGridViewTextBoxColumn chpwd;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udDisabled;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udPasswordNeverExpires;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udUserMayNotChangePassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPwdCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPwdType;
        private System.Windows.Forms.ToolStripButton toolStripButtonPWGen;
    }
}

