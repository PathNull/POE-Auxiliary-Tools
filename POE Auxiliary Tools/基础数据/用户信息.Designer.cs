namespace POE_Auxiliary_Tools.基础数据
{
    partial class 用户信息
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txt_sj = new DevExpress.XtraEditors.TextEdit();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.txt_posid = new DevExpress.XtraEditors.TextEdit();
            this.txt_name = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_sj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_posid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txt_sj);
            this.layoutControl1.Controls.Add(this.btn_save);
            this.layoutControl1.Controls.Add(this.txt_posid);
            this.layoutControl1.Controls.Add(this.txt_name);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(563, 79);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txt_sj
            // 
            this.txt_sj.Location = new System.Drawing.Point(186, 10);
            this.txt_sj.MaximumSize = new System.Drawing.Size(50, 0);
            this.txt_sj.MinimumSize = new System.Drawing.Size(50, 0);
            this.txt_sj.Name = "txt_sj";
            this.txt_sj.Size = new System.Drawing.Size(50, 20);
            this.txt_sj.StyleController = this.layoutControl1;
            this.txt_sj.TabIndex = 8;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(444, 37);
            this.btn_save.Name = "btn_save";
            this.btn_save.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btn_save.Size = new System.Drawing.Size(109, 28);
            this.btn_save.StyleController = this.layoutControl1;
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "确定";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_posid
            // 
            this.txt_posid.Location = new System.Drawing.Point(308, 10);
            this.txt_posid.Name = "txt_posid";
            this.txt_posid.Size = new System.Drawing.Size(245, 20);
            this.txt_posid.StyleController = this.layoutControl1;
            this.txt_posid.TabIndex = 6;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(63, 10);
            this.txt_name.MaximumSize = new System.Drawing.Size(90, 0);
            this.txt_name.MinimumSize = new System.Drawing.Size(90, 0);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(90, 20);
            this.txt_name.StyleController = this.layoutControl1;
            this.txt_name.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(563, 79);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(559, 75);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txt_name;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(147, 24);
            this.layoutControlItem2.Text = "论坛昵称";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txt_posid;
            this.layoutControlItem3.Location = new System.Drawing.Point(230, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(317, 24);
            this.layoutControlItem3.Text = "POESESSID";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(63, 14);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(434, 39);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_save;
            this.layoutControlItem1.Location = new System.Drawing.Point(434, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(113, 39);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txt_sj;
            this.layoutControlItem4.Location = new System.Drawing.Point(147, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(83, 24);
            this.layoutControlItem4.Text = "赛季";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(24, 14);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // 用户信息
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 79);
            this.Controls.Add(this.layoutControl1);
            this.Name = "用户信息";
            this.Text = "Frm_用户信息";
            this.Load += new System.EventHandler(this.Frm_用户信息_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_sj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_posid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txt_name;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txt_posid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txt_sj;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}