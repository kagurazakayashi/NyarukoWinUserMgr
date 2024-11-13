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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLockOFF = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxMachine = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonGroups = new System.Windows.Forms.ToolStripButton();
            this.toolStripLockON = new System.Windows.Forms.ToolStripButton();
            this.dataGridUsers = new System.Windows.Forms.DataGridView();
            this.udName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udAccountExpires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDisabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udPasswordNeverExpires = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udUserMayNotChangePassword = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udGroups = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerStopWaitAni = new System.Windows.Forms.Timer(this.components);
            this.labelWait = new System.Windows.Forms.Label();
            this.timerWaitAni = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
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
            this.toolStrip1.Size = new System.Drawing.Size(980, 28);
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
            this.toolStripLockON.ForeColor = System.Drawing.SystemColors.Highlight;
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
            this.dataGridUsers.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.dataGridUsers.Location = new System.Drawing.Point(0, 28);
            this.dataGridUsers.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridUsers.Name = "dataGridUsers";
            this.dataGridUsers.ReadOnly = true;
            this.dataGridUsers.RowTemplate.Height = 23;
            this.dataGridUsers.Size = new System.Drawing.Size(980, 673);
            this.dataGridUsers.TabIndex = 1;
            this.dataGridUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // udName
            // 
            this.udName.HeaderText = "用户名";
            this.udName.Name = "udName";
            this.udName.ReadOnly = true;
            // 
            // udFullName
            // 
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
            // 
            // udPasswordNeverExpires
            // 
            this.udPasswordNeverExpires.HeaderText = "不过期";
            this.udPasswordNeverExpires.Name = "udPasswordNeverExpires";
            this.udPasswordNeverExpires.ReadOnly = true;
            this.udPasswordNeverExpires.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udPasswordNeverExpires.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // udUserMayNotChangePassword
            // 
            this.udUserMayNotChangePassword.HeaderText = "禁改密";
            this.udUserMayNotChangePassword.Name = "udUserMayNotChangePassword";
            this.udUserMayNotChangePassword.ReadOnly = true;
            this.udUserMayNotChangePassword.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udUserMayNotChangePassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // udGroups
            // 
            this.udGroups.HeaderText = "用户组";
            this.udGroups.Name = "udGroups";
            this.udGroups.ReadOnly = true;
            // 
            // timerStopWaitAni
            // 
            this.timerStopWaitAni.Interval = 1000;
            this.timerStopWaitAni.Tick += new System.EventHandler(this.timerStopWaitAni_Tick);
            // 
            // labelWait
            // 
            this.labelWait.BackColor = System.Drawing.SystemColors.Window;
            this.labelWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWait.Location = new System.Drawing.Point(0, 0);
            this.labelWait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWait.Name = "labelWait";
            this.labelWait.Size = new System.Drawing.Size(980, 701);
            this.labelWait.TabIndex = 2;
            this.labelWait.Text = "正在加载";
            this.labelWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerWaitAni
            // 
            this.timerWaitAni.Interval = 33;
            this.timerWaitAni.Tick += new System.EventHandler(this.timerWaitAni_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 701);
            this.Controls.Add(this.dataGridUsers);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.labelWait);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Form1";
            this.Text = "用户管理器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonReload;
        private System.Windows.Forms.DataGridView dataGridUsers;
        private System.Windows.Forms.Timer timerStopWaitAni;
        private System.Windows.Forms.Label labelWait;
        private System.Windows.Forms.Timer timerWaitAni;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMachine;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn udName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn udAccountExpires;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udDisabled;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udPasswordNeverExpires;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udUserMayNotChangePassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn udGroups;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonGroups;
        private System.Windows.Forms.ToolStripButton toolStripLockON;
        private System.Windows.Forms.ToolStripLabel toolStripLockOFF;
    }
}

