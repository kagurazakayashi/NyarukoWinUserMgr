namespace winusermgr
{
    partial class FormGroupSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGroupSelect));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxSystemGroup = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxSelectedGroup = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.textBoxCustom = new System.Windows.Forms.TextBox();
            this.buttonAddCustom = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 502);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxSystemGroup);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 496);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已有用户组";
            // 
            // listBoxSystemGroup
            // 
            this.listBoxSystemGroup.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxSystemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxSystemGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSystemGroup.FormattingEnabled = true;
            this.listBoxSystemGroup.ItemHeight = 21;
            this.listBoxSystemGroup.Location = new System.Drawing.Point(3, 24);
            this.listBoxSystemGroup.Name = "listBoxSystemGroup";
            this.listBoxSystemGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxSystemGroup.Size = new System.Drawing.Size(344, 469);
            this.listBoxSystemGroup.TabIndex = 0;
            this.listBoxSystemGroup.SelectedIndexChanged += new System.EventHandler(this.listBoxSystemGroup_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxSelectedGroup);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(430, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 496);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "要显示的用户组";
            // 
            // listBoxSelectedGroup
            // 
            this.listBoxSelectedGroup.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxSelectedGroup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxSelectedGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSelectedGroup.ForeColor = System.Drawing.SystemColors.Highlight;
            this.listBoxSelectedGroup.FormattingEnabled = true;
            this.listBoxSelectedGroup.ItemHeight = 21;
            this.listBoxSelectedGroup.Location = new System.Drawing.Point(3, 24);
            this.listBoxSelectedGroup.Name = "listBoxSelectedGroup";
            this.listBoxSelectedGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxSelectedGroup.Size = new System.Drawing.Size(345, 469);
            this.listBoxSelectedGroup.TabIndex = 0;
            this.listBoxSelectedGroup.SelectedIndexChanged += new System.EventHandler(this.listBoxSelectedGroup_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonAdd, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonRemove, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(359, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(65, 496);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.PanEast;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Image = global::winusermgr.Properties.Resources.next;
            this.buttonAdd.Location = new System.Drawing.Point(3, 195);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(59, 50);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(3, 251);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(59, 50);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // textBoxCustom
            // 
            this.textBoxCustom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCustom.Location = new System.Drawing.Point(6, 517);
            this.textBoxCustom.MaxLength = 32;
            this.textBoxCustom.Name = "textBoxCustom";
            this.textBoxCustom.Size = new System.Drawing.Size(230, 28);
            this.textBoxCustom.TabIndex = 4;
            this.textBoxCustom.WordWrap = false;
            this.textBoxCustom.TextChanged += new System.EventHandler(this.textBoxCus_TextChanged);
            this.textBoxCustom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCustom_KeyPress);
            // 
            // buttonAddCustom
            // 
            this.buttonAddCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddCustom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAddCustom.Enabled = false;
            this.buttonAddCustom.Image = global::winusermgr.Properties.Resources.add_database;
            this.buttonAddCustom.Location = new System.Drawing.Point(242, 505);
            this.buttonAddCustom.Name = "buttonAddCustom";
            this.buttonAddCustom.Size = new System.Drawing.Size(185, 50);
            this.buttonAddCustom.TabIndex = 3;
            this.buttonAddCustom.Text = "添加虚拟组名(&A)";
            this.buttonAddCustom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAddCustom.UseVisualStyleBackColor = true;
            this.buttonAddCustom.Click += new System.EventHandler(this.buttonAddCustom_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOK.Enabled = false;
            this.buttonOK.Image = global::winusermgr.Properties.Resources.checkmark;
            this.buttonOK.Location = new System.Drawing.Point(433, 505);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(185, 50);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "保存列设置(&O)";
            this.buttonOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.Image = global::winusermgr.Properties.Resources.cancel;
            this.buttonCancel.Location = new System.Drawing.Point(624, 505);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(154, 50);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "放弃更改(&C)";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormGroupSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.textBoxCustom);
            this.Controls.Add(this.buttonAddCustom);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormGroupSelect";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择用户组列（列表支持多选）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGroupSelect_FormClosing);
            this.Load += new System.EventHandler(this.FormGroupSelect_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxSystemGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxSelectedGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonAddCustom;
        private System.Windows.Forms.TextBox textBoxCustom;
    }
}