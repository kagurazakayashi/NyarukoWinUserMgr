<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormGroupSelect
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormGroupSelect))
        Me.listBoxSystemGroup = New System.Windows.Forms.ListBox()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.listBoxSelectedGroup = New System.Windows.Forms.ListBox()
        Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.buttonAdd = New System.Windows.Forms.Button()
        Me.buttonRemove = New System.Windows.Forms.Button()
        Me.labelWait = New System.Windows.Forms.Label()
        Me.timerStopWaitAni = New System.Windows.Forms.Timer(Me.components)
        Me.timerWaitAni = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.buttonCancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.buttonOK = New System.Windows.Forms.ToolStripMenuItem()
        Me.添加ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.buttonAddCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.buttonAddGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.buttonDelGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.bottonReloadM = New System.Windows.Forms.ToolStripMenuItem()
        Me.主机名ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripComboBoxMachine = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.bottonReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.要新建的组名称ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.textBoxCustom = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tableLayoutPanel1.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.tableLayoutPanel2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'listBoxSystemGroup
        '
        Me.listBoxSystemGroup.BackColor = System.Drawing.Color.LightBlue
        Me.listBoxSystemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listBoxSystemGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listBoxSystemGroup.FormattingEnabled = True
        Me.listBoxSystemGroup.ItemHeight = 21
        Me.listBoxSystemGroup.Location = New System.Drawing.Point(3, 24)
        Me.listBoxSystemGroup.Name = "listBoxSystemGroup"
        Me.listBoxSystemGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.listBoxSystemGroup.Size = New System.Drawing.Size(344, 498)
        Me.listBoxSystemGroup.TabIndex = 0
        '
        'tableLayoutPanel1
        '
        Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tableLayoutPanel1.ColumnCount = 3
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71.0!))
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel1.Controls.Add(Me.groupBox1, 0, 0)
        Me.tableLayoutPanel1.Controls.Add(Me.groupBox2, 2, 0)
        Me.tableLayoutPanel1.Controls.Add(Me.tableLayoutPanel2, 1, 0)
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 30)
        Me.tableLayoutPanel1.Margin = New System.Windows.Forms.Padding(10)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        Me.tableLayoutPanel1.RowCount = 1
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.Size = New System.Drawing.Size(784, 531)
        Me.tableLayoutPanel1.TabIndex = 6
        '
        'groupBox1
        '
        Me.groupBox1.BackColor = System.Drawing.Color.LightBlue
        Me.groupBox1.Controls.Add(Me.listBoxSystemGroup)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBox1.Location = New System.Drawing.Point(3, 3)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(350, 525)
        Me.groupBox1.TabIndex = 0
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "已有用户组"
        '
        'groupBox2
        '
        Me.groupBox2.BackColor = System.Drawing.Color.Pink
        Me.groupBox2.Controls.Add(Me.listBoxSelectedGroup)
        Me.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBox2.Location = New System.Drawing.Point(430, 3)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(351, 525)
        Me.groupBox2.TabIndex = 1
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "要显示的用户组"
        '
        'listBoxSelectedGroup
        '
        Me.listBoxSelectedGroup.BackColor = System.Drawing.Color.Pink
        Me.listBoxSelectedGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listBoxSelectedGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listBoxSelectedGroup.FormattingEnabled = True
        Me.listBoxSelectedGroup.ItemHeight = 21
        Me.listBoxSelectedGroup.Location = New System.Drawing.Point(3, 24)
        Me.listBoxSelectedGroup.Name = "listBoxSelectedGroup"
        Me.listBoxSelectedGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.listBoxSelectedGroup.Size = New System.Drawing.Size(345, 498)
        Me.listBoxSelectedGroup.TabIndex = 0
        '
        'tableLayoutPanel2
        '
        Me.tableLayoutPanel2.ColumnCount = 1
        Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel2.Controls.Add(Me.buttonAdd, 0, 0)
        Me.tableLayoutPanel2.Controls.Add(Me.buttonRemove, 0, 1)
        Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel2.Location = New System.Drawing.Point(359, 3)
        Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
        Me.tableLayoutPanel2.RowCount = 2
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel2.Size = New System.Drawing.Size(65, 525)
        Me.tableLayoutPanel2.TabIndex = 2
        '
        'buttonAdd
        '
        Me.buttonAdd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonAdd.BackColor = System.Drawing.Color.Pink
        Me.buttonAdd.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.buttonAdd.Enabled = False
        Me.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonAdd.Image = Global.GroupMgr.My.Resources.Resources._next
        Me.buttonAdd.Location = New System.Drawing.Point(3, 209)
        Me.buttonAdd.Name = "buttonAdd"
        Me.buttonAdd.Size = New System.Drawing.Size(59, 50)
        Me.buttonAdd.TabIndex = 0
        Me.buttonAdd.UseVisualStyleBackColor = False
        '
        'buttonRemove
        '
        Me.buttonRemove.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonRemove.BackColor = System.Drawing.Color.LightBlue
        Me.buttonRemove.Cursor = System.Windows.Forms.Cursors.PanWest
        Me.buttonRemove.Enabled = False
        Me.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonRemove.Location = New System.Drawing.Point(3, 265)
        Me.buttonRemove.Name = "buttonRemove"
        Me.buttonRemove.Size = New System.Drawing.Size(59, 50)
        Me.buttonRemove.TabIndex = 1
        Me.buttonRemove.UseVisualStyleBackColor = False
        '
        'labelWait
        '
        Me.labelWait.BackColor = System.Drawing.SystemColors.Control
        Me.labelWait.Dock = System.Windows.Forms.DockStyle.Fill
        Me.labelWait.Location = New System.Drawing.Point(0, 0)
        Me.labelWait.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelWait.Name = "labelWait"
        Me.labelWait.Size = New System.Drawing.Size(784, 561)
        Me.labelWait.TabIndex = 11
        Me.labelWait.Text = "正在加载"
        Me.labelWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.labelWait.UseWaitCursor = True
        '
        'timerStopWaitAni
        '
        Me.timerStopWaitAni.Interval = 1000
        '
        'timerWaitAni
        '
        Me.timerWaitAni.Interval = 33
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.buttonCancel, Me.buttonOK, Me.bottonReloadM, Me.添加ToolStripMenuItem, Me.buttonDelGroup})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 29)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'buttonCancel
        '
        Me.buttonCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.buttonCancel.Image = Global.GroupMgr.My.Resources.Resources.cancel
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.ShortcutKeys = System.Windows.Forms.Keys.F9
        Me.buttonCancel.Size = New System.Drawing.Size(130, 25)
        Me.buttonCancel.Text = "放弃变更(F9)"
        '
        'buttonOK
        '
        Me.buttonOK.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.buttonOK.Image = Global.GroupMgr.My.Resources.Resources.checkmark
        Me.buttonOK.Name = "buttonOK"
        Me.buttonOK.ShortcutKeys = System.Windows.Forms.Keys.F10
        Me.buttonOK.Size = New System.Drawing.Size(155, 25)
        Me.buttonOK.Text = "保存显示列(F10)"
        '
        '添加ToolStripMenuItem
        '
        Me.添加ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.要新建的组名称ToolStripMenuItem, Me.textBoxCustom, Me.ToolStripMenuItem2, Me.buttonAddCustom, Me.buttonAddGroup})
        Me.添加ToolStripMenuItem.Image = Global.GroupMgr.My.Resources.Resources.plus
        Me.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem"
        Me.添加ToolStripMenuItem.Size = New System.Drawing.Size(139, 25)
        Me.添加ToolStripMenuItem.Text = "创建用户组(&A)"
        '
        'buttonAddCustom
        '
        Me.buttonAddCustom.Enabled = False
        Me.buttonAddCustom.Image = Global.GroupMgr.My.Resources.Resources.podium_with_audience
        Me.buttonAddCustom.Name = "buttonAddCustom"
        Me.buttonAddCustom.Size = New System.Drawing.Size(214, 26)
        Me.buttonAddCustom.Text = "新建虚拟用户组(&E)"
        '
        'buttonAddGroup
        '
        Me.buttonAddGroup.Enabled = False
        Me.buttonAddGroup.Image = Global.GroupMgr.My.Resources.Resources.conference_call
        Me.buttonAddGroup.Name = "buttonAddGroup"
        Me.buttonAddGroup.Size = New System.Drawing.Size(214, 26)
        Me.buttonAddGroup.Text = "新建真实用户组(&G)"
        '
        'buttonDelGroup
        '
        Me.buttonDelGroup.Enabled = False
        Me.buttonDelGroup.Image = Global.GroupMgr.My.Resources.Resources.full_trash
        Me.buttonDelGroup.Name = "buttonDelGroup"
        Me.buttonDelGroup.Size = New System.Drawing.Size(140, 25)
        Me.buttonDelGroup.Text = "删除选择组(&D)"
        '
        'bottonReloadM
        '
        Me.bottonReloadM.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.主机名ToolStripMenuItem, Me.toolStripComboBoxMachine, Me.ToolStripMenuItem1, Me.bottonReload})
        Me.bottonReloadM.Image = Global.GroupMgr.My.Resources.Resources.refresh
        Me.bottonReloadM.Name = "bottonReloadM"
        Me.bottonReloadM.Size = New System.Drawing.Size(139, 25)
        Me.bottonReloadM.Text = "加载用户组(&C)"
        '
        '主机名ToolStripMenuItem
        '
        Me.主机名ToolStripMenuItem.Enabled = False
        Me.主机名ToolStripMenuItem.Name = "主机名ToolStripMenuItem"
        Me.主机名ToolStripMenuItem.Size = New System.Drawing.Size(199, 26)
        Me.主机名ToolStripMenuItem.Text = "主机名"
        '
        'toolStripComboBoxMachine
        '
        Me.toolStripComboBoxMachine.BackColor = System.Drawing.SystemColors.Control
        Me.toolStripComboBoxMachine.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.toolStripComboBoxMachine.Name = "toolStripComboBoxMachine"
        Me.toolStripComboBoxMachine.Size = New System.Drawing.Size(100, 23)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(196, 6)
        '
        'bottonReload
        '
        Me.bottonReload.Image = Global.GroupMgr.My.Resources.Resources.refresh
        Me.bottonReload.Name = "bottonReload"
        Me.bottonReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.bottonReload.Size = New System.Drawing.Size(199, 26)
        Me.bottonReload.Text = "连接/刷新(&R)"
        '
        '要新建的组名称ToolStripMenuItem
        '
        Me.要新建的组名称ToolStripMenuItem.Enabled = False
        Me.要新建的组名称ToolStripMenuItem.Name = "要新建的组名称ToolStripMenuItem"
        Me.要新建的组名称ToolStripMenuItem.Size = New System.Drawing.Size(214, 26)
        Me.要新建的组名称ToolStripMenuItem.Text = "要新建的组名称"
        '
        'textBoxCustom
        '
        Me.textBoxCustom.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.textBoxCustom.Name = "textBoxCustom"
        Me.textBoxCustom.Size = New System.Drawing.Size(100, 23)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(211, 6)
        '
        'FormGroupSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.Controls.Add(Me.labelWait)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "FormGroupSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "选择要在表格中显示的用户组列(支持批量选择)"
        Me.tableLayoutPanel1.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.tableLayoutPanel2.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents listBoxSystemGroup As ListBox
    Private WithEvents tableLayoutPanel1 As TableLayoutPanel
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents listBoxSelectedGroup As ListBox
    Private WithEvents tableLayoutPanel2 As TableLayoutPanel
    Private WithEvents buttonAdd As Button
    Private WithEvents buttonRemove As Button
    Private WithEvents labelWait As Label
    Private WithEvents timerStopWaitAni As Timer
    Private WithEvents timerWaitAni As Timer
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents buttonOK As ToolStripMenuItem
    Friend WithEvents buttonCancel As ToolStripMenuItem
    Friend WithEvents 添加ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents buttonAddCustom As ToolStripMenuItem
    Friend WithEvents buttonAddGroup As ToolStripMenuItem
    Friend WithEvents buttonDelGroup As ToolStripMenuItem
    Friend WithEvents bottonReloadM As ToolStripMenuItem
    Friend WithEvents 主机名ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripComboBoxMachine As ToolStripTextBox
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents bottonReload As ToolStripMenuItem
    Friend WithEvents 要新建的组名称ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents textBoxCustom As ToolStripTextBox
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
End Class
