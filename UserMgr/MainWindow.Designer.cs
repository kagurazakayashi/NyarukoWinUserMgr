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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.dataGridUsers = new System.Windows.Forms.DataGridView();
            this.udName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udAccountExpires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chpwd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udDisabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udPasswordNeverExpires = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.udUserMayNotChangePassword = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AllGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerStopWaitAni = new System.Windows.Forms.Timer(this.components);
            this.timerWaitAni = new System.Windows.Forms.Timer(this.components);
            this.labelWait = new System.Windows.Forms.Label();
            this.timerOpenGroup = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripButtonOK = new System.Windows.Forms.ToolStripMenuItem();
            this.连接到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.主机名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxMachine = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReload2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.密码生成器PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.密码位数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxPwdCount = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.密码构成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxPwdType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPWGen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonPWGenC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUDIDGen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLockOFF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLockON = new System.Windows.Forms.ToolStripMenuItem();
            this.新建或删除账户UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpenAccL = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpenAccM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemOpenAccC = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpREADME = new System.Windows.Forms.ToolStripMenuItem();
            this.helpCommits = new System.Windows.Forms.ToolStripMenuItem();
            this.helpIssues = new System.Windows.Forms.ToolStripMenuItem();
            this.helpReleases = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.dEBUGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxBG = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBG)).BeginInit();
            this.SuspendLayout();
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
            this.chpwd,
            this.udDisabled,
            this.udPasswordNeverExpires,
            this.udUserMayNotChangePassword,
            this.AllGroup});
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
            this.dataGridUsers.Location = new System.Drawing.Point(0, 29);
            this.dataGridUsers.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridUsers.Name = "dataGridUsers";
            this.dataGridUsers.RowTemplate.Height = 40;
            this.dataGridUsers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridUsers.Size = new System.Drawing.Size(957, 532);
            this.dataGridUsers.TabIndex = 1;
            this.dataGridUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridUsers.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridUsers_CellEndEdit);
            this.dataGridUsers.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridUsers_CellValidating);
            this.dataGridUsers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridUsers_CellValueChanged);
            this.dataGridUsers.CurrentCellChanged += new System.EventHandler(this.dataGridUsers_CurrentCellChanged);
            this.dataGridUsers.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridUsers_CurrentCellDirtyStateChanged);
            this.dataGridUsers.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridUsers_UserDeletedRow);
            this.dataGridUsers.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridUsers_UserDeletingRow);
            this.dataGridUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridUsers_KeyDown);
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
            this.udFullName.Width = 67;
            // 
            // udDescription
            // 
            this.udDescription.HeaderText = "描述";
            this.udDescription.Name = "udDescription";
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
            this.chpwd.HeaderText = "改密码";
            this.chpwd.Name = "chpwd";
            this.chpwd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chpwd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chpwd.Width = 80;
            // 
            // udDisabled
            // 
            this.udDisabled.HeaderText = "禁用";
            this.udDisabled.Name = "udDisabled";
            this.udDisabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udDisabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udDisabled.Width = 80;
            // 
            // udPasswordNeverExpires
            // 
            this.udPasswordNeverExpires.HeaderText = "不过期";
            this.udPasswordNeverExpires.Name = "udPasswordNeverExpires";
            this.udPasswordNeverExpires.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udPasswordNeverExpires.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udPasswordNeverExpires.Width = 80;
            // 
            // udUserMayNotChangePassword
            // 
            this.udUserMayNotChangePassword.HeaderText = "禁改密";
            this.udUserMayNotChangePassword.Name = "udUserMayNotChangePassword";
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
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOK,
            this.连接到ToolStripMenuItem,
            this.toolStripButtonGroups,
            this.密码生成器PToolStripMenuItem,
            this.toolStripLockOFF,
            this.toolStripLockON,
            this.新建或删除账户UToolStripMenuItem,
            this.HelpHToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1284, 29);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripButtonOK
            // 
            this.toolStripButtonOK.Image = global::WinUserMgr.Properties.Resources.paid;
            this.toolStripButtonOK.Name = "toolStripButtonOK";
            this.toolStripButtonOK.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.toolStripButtonOK.Size = new System.Drawing.Size(139, 25);
            this.toolStripButtonOK.Text = "核对变更(F10)";
            this.toolStripButtonOK.Click += new System.EventHandler(this.toolStripButtonOK_Click);
            // 
            // 连接到ToolStripMenuItem
            // 
            this.连接到ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.主机名ToolStripMenuItem,
            this.toolStripComboBoxMachine,
            this.toolStripSeparator4,
            this.toolStripButtonReload2});
            this.连接到ToolStripMenuItem.Image = global::WinUserMgr.Properties.Resources.refresh;
            this.连接到ToolStripMenuItem.Name = "连接到ToolStripMenuItem";
            this.连接到ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.连接到ToolStripMenuItem.Size = new System.Drawing.Size(146, 25);
            this.连接到ToolStripMenuItem.Text = "连接到/刷新(&C)";
            // 
            // 主机名ToolStripMenuItem
            // 
            this.主机名ToolStripMenuItem.Enabled = false;
            this.主机名ToolStripMenuItem.Name = "主机名ToolStripMenuItem";
            this.主机名ToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.主机名ToolStripMenuItem.Text = "主机名";
            // 
            // toolStripComboBoxMachine
            // 
            this.toolStripComboBoxMachine.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripComboBoxMachine.Name = "toolStripComboBoxMachine";
            this.toolStripComboBoxMachine.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(196, 6);
            // 
            // toolStripButtonReload2
            // 
            this.toolStripButtonReload2.Image = global::WinUserMgr.Properties.Resources.refresh;
            this.toolStripButtonReload2.Name = "toolStripButtonReload2";
            this.toolStripButtonReload2.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.toolStripButtonReload2.Size = new System.Drawing.Size(199, 26);
            this.toolStripButtonReload2.Text = "连接/刷新(&R)";
            this.toolStripButtonReload2.Click += new System.EventHandler(this.toolStripButtonReload_Click);
            // 
            // toolStripButtonGroups
            // 
            this.toolStripButtonGroups.Image = global::WinUserMgr.Properties.Resources.data_sheet;
            this.toolStripButtonGroups.Name = "toolStripButtonGroups";
            this.toolStripButtonGroups.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.toolStripButtonGroups.Size = new System.Drawing.Size(162, 25);
            this.toolStripButtonGroups.Text = "选择用户组列(F6)";
            this.toolStripButtonGroups.Click += new System.EventHandler(this.toolStripButtonGroups_Click);
            // 
            // 密码生成器PToolStripMenuItem
            // 
            this.密码生成器PToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.密码位数ToolStripMenuItem,
            this.toolStripComboBoxPwdCount,
            this.toolStripSeparator5,
            this.密码构成ToolStripMenuItem,
            this.toolStripComboBoxPwdType,
            this.toolStripSeparator6,
            this.toolStripButtonPWGen,
            this.toolStripButtonPWGenC,
            this.toolStripSeparator1,
            this.toolStripButtonUDIDGen});
            this.密码生成器PToolStripMenuItem.Image = global::WinUserMgr.Properties.Resources.key;
            this.密码生成器PToolStripMenuItem.Name = "密码生成器PToolStripMenuItem";
            this.密码生成器PToolStripMenuItem.Size = new System.Drawing.Size(138, 25);
            this.密码生成器PToolStripMenuItem.Text = "密码生成器(&P)";
            // 
            // 密码位数ToolStripMenuItem
            // 
            this.密码位数ToolStripMenuItem.Enabled = false;
            this.密码位数ToolStripMenuItem.Name = "密码位数ToolStripMenuItem";
            this.密码位数ToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
            this.密码位数ToolStripMenuItem.Text = "密码位数";
            // 
            // toolStripComboBoxPwdCount
            // 
            this.toolStripComboBoxPwdCount.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripComboBoxPwdCount.Name = "toolStripComboBoxPwdCount";
            this.toolStripComboBoxPwdCount.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(238, 6);
            // 
            // 密码构成ToolStripMenuItem
            // 
            this.密码构成ToolStripMenuItem.Enabled = false;
            this.密码构成ToolStripMenuItem.Name = "密码构成ToolStripMenuItem";
            this.密码构成ToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
            this.密码构成ToolStripMenuItem.Text = "密码构成";
            // 
            // toolStripComboBoxPwdType
            // 
            this.toolStripComboBoxPwdType.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripComboBoxPwdType.Items.AddRange(new object[] {
            "a-z,0-9",
            "A-Z,a-z,0-9",
            "A-Z,a-z,0-9,sym"});
            this.toolStripComboBoxPwdType.Name = "toolStripComboBoxPwdType";
            this.toolStripComboBoxPwdType.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(238, 6);
            // 
            // toolStripButtonPWGen
            // 
            this.toolStripButtonPWGen.Enabled = false;
            this.toolStripButtonPWGen.Image = global::WinUserMgr.Properties.Resources.key;
            this.toolStripButtonPWGen.Name = "toolStripButtonPWGen";
            this.toolStripButtonPWGen.Size = new System.Drawing.Size(241, 26);
            this.toolStripButtonPWGen.Text = "填入生成的密码(&P)";
            this.toolStripButtonPWGen.Click += new System.EventHandler(this.toolStripButtonPWGen_Click);
            // 
            // toolStripButtonPWGenC
            // 
            this.toolStripButtonPWGenC.Image = global::WinUserMgr.Properties.Resources.inspection;
            this.toolStripButtonPWGenC.Name = "toolStripButtonPWGenC";
            this.toolStripButtonPWGenC.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.toolStripButtonPWGenC.Size = new System.Drawing.Size(241, 26);
            this.toolStripButtonPWGenC.Text = "复制生成的密码(&C)";
            this.toolStripButtonPWGenC.Click += new System.EventHandler(this.toolStripButtonPWGenC_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(238, 6);
            // 
            // toolStripButtonUDIDGen
            // 
            this.toolStripButtonUDIDGen.Image = global::WinUserMgr.Properties.Resources.portrait_mode;
            this.toolStripButtonUDIDGen.Name = "toolStripButtonUDIDGen";
            this.toolStripButtonUDIDGen.Size = new System.Drawing.Size(241, 26);
            this.toolStripButtonUDIDGen.Text = "生成并复制 UDID (&U)";
            this.toolStripButtonUDIDGen.Click += new System.EventHandler(this.toolStripButtonUDIDGen_Click);
            // 
            // toolStripLockOFF
            // 
            this.toolStripLockOFF.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLockOFF.Enabled = false;
            this.toolStripLockOFF.Image = global::WinUserMgr.Properties.Resources.unlock;
            this.toolStripLockOFF.Name = "toolStripLockOFF";
            this.toolStripLockOFF.Size = new System.Drawing.Size(166, 25);
            this.toolStripLockOFF.Text = "已使用管理员身份";
            // 
            // toolStripLockON
            // 
            this.toolStripLockON.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLockON.Image = global::WinUserMgr.Properties.Resources._lock;
            this.toolStripLockON.Name = "toolStripLockON";
            this.toolStripLockON.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.toolStripLockON.Size = new System.Drawing.Size(203, 25);
            this.toolStripLockON.Text = "解锁不可用的设置(F11)";
            this.toolStripLockON.Click += new System.EventHandler(this.toolStripLockON_Click);
            // 
            // 新建或删除账户UToolStripMenuItem
            // 
            this.新建或删除账户UToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOpenAccL,
            this.ToolStripMenuItemOpenAccM,
            this.toolStripSeparator3,
            this.ToolStripMenuItemOpenAccC});
            this.新建或删除账户UToolStripMenuItem.Image = global::WinUserMgr.Properties.Resources.portrait_mode;
            this.新建或删除账户UToolStripMenuItem.Name = "新建或删除账户UToolStripMenuItem";
            this.新建或删除账户UToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.新建或删除账户UToolStripMenuItem.Size = new System.Drawing.Size(125, 25);
            this.新建或删除账户UToolStripMenuItem.Text = "系统设置(&Q)";
            // 
            // ToolStripMenuItemOpenAccL
            // 
            this.ToolStripMenuItemOpenAccL.Image = global::WinUserMgr.Properties.Resources.shell32_220_16;
            this.ToolStripMenuItemOpenAccL.Name = "ToolStripMenuItemOpenAccL";
            this.ToolStripMenuItemOpenAccL.Size = new System.Drawing.Size(288, 26);
            this.ToolStripMenuItemOpenAccL.Text = "本地账户(&L)";
            this.ToolStripMenuItemOpenAccL.Click += new System.EventHandler(this.ToolStripMenuItemOpenAccL_Click);
            // 
            // ToolStripMenuItemOpenAccM
            // 
            this.ToolStripMenuItemOpenAccM.Image = global::WinUserMgr.Properties.Resources.shell32_279;
            this.ToolStripMenuItemOpenAccM.Name = "ToolStripMenuItemOpenAccM";
            this.ToolStripMenuItemOpenAccM.Size = new System.Drawing.Size(288, 26);
            this.ToolStripMenuItemOpenAccM.Text = "Microsoft 账户(&M)";
            this.ToolStripMenuItemOpenAccM.Click += new System.EventHandler(this.ToolStripMenuItemOpenAccM_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(285, 6);
            // 
            // ToolStripMenuItemOpenAccC
            // 
            this.ToolStripMenuItemOpenAccC.Image = global::WinUserMgr.Properties.Resources.mmc_128;
            this.ToolStripMenuItemOpenAccC.Name = "ToolStripMenuItemOpenAccC";
            this.ToolStripMenuItemOpenAccC.Size = new System.Drawing.Size(288, 26);
            this.ToolStripMenuItemOpenAccC.Text = "本地用户和组MMC控制台(&C)";
            this.ToolStripMenuItemOpenAccC.Click += new System.EventHandler(this.ToolStripMenuItemOpenAccC_Click);
            // 
            // HelpHToolStripMenuItem
            // 
            this.HelpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpREADME,
            this.helpCommits,
            this.helpIssues,
            this.helpReleases,
            this.toolStripMenuItem1,
            this.dEBUGToolStripMenuItem});
            this.HelpHToolStripMenuItem.Image = global::WinUserMgr.Properties.Resources.questions;
            this.HelpHToolStripMenuItem.Name = "HelpHToolStripMenuItem";
            this.HelpHToolStripMenuItem.Size = new System.Drawing.Size(92, 25);
            this.HelpHToolStripMenuItem.Text = "帮助(&H)";
            // 
            // helpREADME
            // 
            this.helpREADME.Image = global::WinUserMgr.Properties.Resources.globe;
            this.helpREADME.Name = "helpREADME";
            this.helpREADME.Size = new System.Drawing.Size(197, 26);
            this.helpREADME.Text = "在线文档(&H)";
            this.helpREADME.Click += new System.EventHandler(this.helpREADME_Click);
            // 
            // helpCommits
            // 
            this.helpCommits.Image = global::WinUserMgr.Properties.Resources.globe;
            this.helpCommits.Name = "helpCommits";
            this.helpCommits.Size = new System.Drawing.Size(197, 26);
            this.helpCommits.Text = "变更历史记录(&C)";
            this.helpCommits.Click += new System.EventHandler(this.helpCommits_Click);
            // 
            // helpIssues
            // 
            this.helpIssues.Image = global::WinUserMgr.Properties.Resources.globe;
            this.helpIssues.Name = "helpIssues";
            this.helpIssues.Size = new System.Drawing.Size(197, 26);
            this.helpIssues.Text = "报告问题(&I)";
            this.helpIssues.Click += new System.EventHandler(this.helpIssues_Click);
            // 
            // helpReleases
            // 
            this.helpReleases.Image = global::WinUserMgr.Properties.Resources.globe;
            this.helpReleases.Name = "helpReleases";
            this.helpReleases.Size = new System.Drawing.Size(197, 26);
            this.helpReleases.Text = "下载最新版(&R)";
            this.helpReleases.Click += new System.EventHandler(this.helpReleases_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 6);
            // 
            // dEBUGToolStripMenuItem
            // 
            this.dEBUGToolStripMenuItem.Name = "dEBUGToolStripMenuItem";
            this.dEBUGToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.dEBUGToolStripMenuItem.Text = "测试模式";
            this.dEBUGToolStripMenuItem.Click += new System.EventHandler(this.dEBUGToolStripMenuItem_Click);
            // 
            // pictureBoxBG
            // 
            this.pictureBoxBG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBG.BackColor = System.Drawing.Color.LightBlue;
            this.pictureBoxBG.Location = new System.Drawing.Point(609, 29);
            this.pictureBoxBG.Name = "pictureBoxBG";
            this.pictureBoxBG.Size = new System.Drawing.Size(675, 532);
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
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.labelWait);
            this.Controls.Add(this.pictureBoxBG);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Move += new System.EventHandler(this.MainWindow_Move);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridUsers;
        private System.Windows.Forms.Timer timerStopWaitAni;
        private System.Windows.Forms.Timer timerWaitAni;
        private System.Windows.Forms.PictureBox pictureBoxBG;
        private System.Windows.Forms.Label labelWait;
        private System.Windows.Forms.Timer timerOpenGroup;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonOK;
        private System.Windows.Forms.ToolStripMenuItem 连接到ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 主机名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMachine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonReload2;
        private System.Windows.Forms.ToolStripMenuItem 密码生成器PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 密码位数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPwdCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 密码构成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPwdType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonPWGen;
        private System.Windows.Forms.ToolStripMenuItem toolStripLockOFF;
        private System.Windows.Forms.ToolStripMenuItem toolStripLockON;
        private System.Windows.Forms.ToolStripMenuItem 新建或删除账户UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpenAccL;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpenAccM;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonGroups;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpenAccC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonPWGenC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonUDIDGen;
        private System.Windows.Forms.ToolStripMenuItem HelpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dEBUGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpREADME;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpCommits;
        private System.Windows.Forms.ToolStripMenuItem helpIssues;
        private System.Windows.Forms.ToolStripMenuItem helpReleases;
        private System.Windows.Forms.DataGridViewTextBoxColumn udName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn udDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn udAccountExpires;
        private System.Windows.Forms.DataGridViewTextBoxColumn chpwd;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udDisabled;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udPasswordNeverExpires;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udUserMayNotChangePassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllGroup;
    }
}

