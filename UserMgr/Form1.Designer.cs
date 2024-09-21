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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.dataGridUsers = new System.Windows.Forms.DataGridView();
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
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonReload});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(996, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonReload
            // 
            this.toolStripButtonReload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReload.Image")));
            this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReload.Name = "toolStripButtonReload";
            this.toolStripButtonReload.Size = new System.Drawing.Size(68, 22);
            this.toolStripButtonReload.Text = "刷新(&R)";
            this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
            // 
            // dataGridUsers
            // 
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
            this.dataGridUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridUsers.Location = new System.Drawing.Point(0, 25);
            this.dataGridUsers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridUsers.Name = "dataGridUsers";
            this.dataGridUsers.RowTemplate.Height = 23;
            this.dataGridUsers.Size = new System.Drawing.Size(996, 463);
            this.dataGridUsers.TabIndex = 1;
            this.dataGridUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // udName
            // 
            this.udName.HeaderText = "用户名";
            this.udName.Name = "udName";
            // 
            // udFullName
            // 
            this.udFullName.HeaderText = "全名";
            this.udFullName.Name = "udFullName";
            // 
            // udDescription
            // 
            this.udDescription.HeaderText = "描述";
            this.udDescription.Name = "udDescription";
            // 
            // udAccountExpires
            // 
            this.udAccountExpires.HeaderText = "有效期";
            this.udAccountExpires.Name = "udAccountExpires";
            // 
            // udDisabled
            // 
            this.udDisabled.HeaderText = "禁用";
            this.udDisabled.Name = "udDisabled";
            this.udDisabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udDisabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // udPasswordNeverExpires
            // 
            this.udPasswordNeverExpires.HeaderText = "密码不过期";
            this.udPasswordNeverExpires.Name = "udPasswordNeverExpires";
            this.udPasswordNeverExpires.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udPasswordNeverExpires.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // udUserMayNotChangePassword
            // 
            this.udUserMayNotChangePassword.HeaderText = "禁改密码";
            this.udUserMayNotChangePassword.Name = "udUserMayNotChangePassword";
            this.udUserMayNotChangePassword.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udUserMayNotChangePassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // udGroups
            // 
            this.udGroups.HeaderText = "用户组";
            this.udGroups.Name = "udGroups";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 488);
            this.Controls.Add(this.dataGridUsers);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("黑体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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

