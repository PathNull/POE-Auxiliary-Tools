namespace POE_Auxiliary_Tools
{
    partial class MainFrom:BaseForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.查询类工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.集市价格查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询历史ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基础数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.物品管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.物品类别管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图相关ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询类工具ToolStripMenuItem,
            this.基础数据ToolStripMenuItem,
            this.自动工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1208, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 查询类工具ToolStripMenuItem
            // 
            this.查询类工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.集市价格查询ToolStripMenuItem,
            this.查询历史ToolStripMenuItem});
            this.查询类工具ToolStripMenuItem.Name = "查询类工具ToolStripMenuItem";
            this.查询类工具ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.查询类工具ToolStripMenuItem.Text = "查询类工具";
            // 
            // 集市价格查询ToolStripMenuItem
            // 
            this.集市价格查询ToolStripMenuItem.Name = "集市价格查询ToolStripMenuItem";
            this.集市价格查询ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.集市价格查询ToolStripMenuItem.Text = "集市查询";
            this.集市价格查询ToolStripMenuItem.Click += new System.EventHandler(this.集市价格查询ToolStripMenuItem_Click);
            // 
            // 查询历史ToolStripMenuItem
            // 
            this.查询历史ToolStripMenuItem.Name = "查询历史ToolStripMenuItem";
            this.查询历史ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.查询历史ToolStripMenuItem.Text = "查询历史";
            this.查询历史ToolStripMenuItem.Click += new System.EventHandler(this.查询历史ToolStripMenuItem_Click);
            // 
            // 基础数据ToolStripMenuItem
            // 
            this.基础数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.物品管理ToolStripMenuItem,
            this.物品类别管理ToolStripMenuItem,
            this.重置ToolStripMenuItem});
            this.基础数据ToolStripMenuItem.Name = "基础数据ToolStripMenuItem";
            this.基础数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.基础数据ToolStripMenuItem.Text = "基础数据";
            // 
            // 物品管理ToolStripMenuItem
            // 
            this.物品管理ToolStripMenuItem.Name = "物品管理ToolStripMenuItem";
            this.物品管理ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.物品管理ToolStripMenuItem.Text = "物品属性";
            this.物品管理ToolStripMenuItem.Click += new System.EventHandler(this.物品管理ToolStripMenuItem_Click);
            // 
            // 物品类别管理ToolStripMenuItem
            // 
            this.物品类别管理ToolStripMenuItem.Name = "物品类别管理ToolStripMenuItem";
            this.物品类别管理ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.物品类别管理ToolStripMenuItem.Text = "物品类别";
            this.物品类别管理ToolStripMenuItem.Click += new System.EventHandler(this.物品类别管理ToolStripMenuItem_Click);
            // 
            // 重置ToolStripMenuItem
            // 
            this.重置ToolStripMenuItem.Name = "重置ToolStripMenuItem";
            this.重置ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.重置ToolStripMenuItem.Text = "重置POESESSID";
            this.重置ToolStripMenuItem.Click += new System.EventHandler(this.重置ToolStripMenuItem_Click);
            // 
            // 自动工具ToolStripMenuItem
            // 
            this.自动工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地图相关ToolStripMenuItem,
            this.装备相关ToolStripMenuItem});
            this.自动工具ToolStripMenuItem.Name = "自动工具ToolStripMenuItem";
            this.自动工具ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.自动工具ToolStripMenuItem.Text = "自动工具";
            // 
            // 地图相关ToolStripMenuItem
            // 
            this.地图相关ToolStripMenuItem.Name = "地图相关ToolStripMenuItem";
            this.地图相关ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.地图相关ToolStripMenuItem.Text = "地图相关";
            this.地图相关ToolStripMenuItem.Click += new System.EventHandler(this.地图相关ToolStripMenuItem_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 25);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1208, 244, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1208, 718);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1204, 714);
            this.panelControl1.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1208, 718);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1208, 718);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 743);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrom";
            this.Text = "by 要啥电动车";
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查询类工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 集市价格查询ToolStripMenuItem;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.ToolStripMenuItem 基础数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 物品类别管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 物品管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询历史ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重置ToolStripMenuItem;
    }
}