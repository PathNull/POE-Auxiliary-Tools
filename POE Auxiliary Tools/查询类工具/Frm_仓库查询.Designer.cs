﻿namespace POE_Auxiliary_Tools
{
    partial class Frm_仓库查询
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
            this.simpleButton_cx = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_lfyQuery = new DevExpress.XtraEditors.TextEdit();
            this.gridControl_productCount = new DevExpress.XtraGrid.GridControl();
            this.gridView_productCount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton_getGoods = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl_statistics = new DevExpress.XtraGrid.GridControl();
            this.gridView_statistics = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl_label = new DevExpress.XtraGrid.GridControl();
            this.gridView_label = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_lfyQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_productCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_productCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_statistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_statistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButton_cx);
            this.layoutControl1.Controls.Add(this.textEdit_lfyQuery);
            this.layoutControl1.Controls.Add(this.gridControl_productCount);
            this.layoutControl1.Controls.Add(this.simpleButton_getGoods);
            this.layoutControl1.Controls.Add(this.gridControl_statistics);
            this.layoutControl1.Controls.Add(this.gridControl_label);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1166, 679);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButton_cx
            // 
            this.simpleButton_cx.Location = new System.Drawing.Point(575, 478);
            this.simpleButton_cx.Margin = new System.Windows.Forms.Padding(2);
            this.simpleButton_cx.MaximumSize = new System.Drawing.Size(74, 22);
            this.simpleButton_cx.Name = "simpleButton_cx";
            this.simpleButton_cx.Size = new System.Drawing.Size(72, 22);
            this.simpleButton_cx.StyleController = this.layoutControl1;
            this.simpleButton_cx.TabIndex = 9;
            this.simpleButton_cx.Text = "查询";
            // 
            // textEdit_lfyQuery
            // 
            this.textEdit_lfyQuery.Location = new System.Drawing.Point(417, 478);
            this.textEdit_lfyQuery.Margin = new System.Windows.Forms.Padding(2);
            this.textEdit_lfyQuery.MaximumSize = new System.Drawing.Size(161, 19);
            this.textEdit_lfyQuery.Name = "textEdit_lfyQuery";
            this.textEdit_lfyQuery.Size = new System.Drawing.Size(154, 19);
            this.textEdit_lfyQuery.StyleController = this.layoutControl1;
            this.textEdit_lfyQuery.TabIndex = 8;
            this.textEdit_lfyQuery.TextChanged += new System.EventHandler(this.textEdit_lfyQuery_TextChanged);
            // 
            // gridControl_productCount
            // 
            this.gridControl_productCount.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl_productCount.Location = new System.Drawing.Point(345, 504);
            this.gridControl_productCount.MainView = this.gridView_productCount;
            this.gridControl_productCount.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl_productCount.Name = "gridControl_productCount";
            this.gridControl_productCount.Size = new System.Drawing.Size(816, 170);
            this.gridControl_productCount.TabIndex = 7;
            this.gridControl_productCount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_productCount});
            // 
            // gridView_productCount
            // 
            this.gridView_productCount.DetailHeight = 280;
            this.gridView_productCount.GridControl = this.gridControl_productCount;
            this.gridView_productCount.Name = "gridView_productCount";
            this.gridView_productCount.OptionsView.ColumnAutoWidth = false;
            this.gridView_productCount.OptionsView.ShowGroupPanel = false;
            this.gridView_productCount.OptionsView.ShowIndicator = false;
            // 
            // simpleButton_getGoods
            // 
            this.simpleButton_getGoods.Location = new System.Drawing.Point(5, 26);
            this.simpleButton_getGoods.Margin = new System.Windows.Forms.Padding(2);
            this.simpleButton_getGoods.MaximumSize = new System.Drawing.Size(88, 22);
            this.simpleButton_getGoods.Name = "simpleButton_getGoods";
            this.simpleButton_getGoods.Size = new System.Drawing.Size(79, 22);
            this.simpleButton_getGoods.StyleController = this.layoutControl1;
            this.simpleButton_getGoods.TabIndex = 6;
            this.simpleButton_getGoods.Text = "获取物品列表";
            this.simpleButton_getGoods.Click += new System.EventHandler(this.simpleButton_getGoods_Click);
            // 
            // gridControl_statistics
            // 
            this.gridControl_statistics.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl_statistics.Location = new System.Drawing.Point(345, 26);
            this.gridControl_statistics.MainView = this.gridView_statistics;
            this.gridControl_statistics.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl_statistics.Name = "gridControl_statistics";
            this.gridControl_statistics.Size = new System.Drawing.Size(816, 448);
            this.gridControl_statistics.TabIndex = 5;
            this.gridControl_statistics.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_statistics});
            // 
            // gridView_statistics
            // 
            this.gridView_statistics.DetailHeight = 280;
            this.gridView_statistics.GridControl = this.gridControl_statistics;
            this.gridView_statistics.Name = "gridView_statistics";
            this.gridView_statistics.OptionsView.ColumnAutoWidth = false;
            this.gridView_statistics.OptionsView.ShowGroupPanel = false;
            this.gridView_statistics.OptionsView.ShowIndicator = false;
            // 
            // gridControl_label
            // 
            this.gridControl_label.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl_label.Location = new System.Drawing.Point(5, 52);
            this.gridControl_label.MainView = this.gridView_label;
            this.gridControl_label.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl_label.MaximumSize = new System.Drawing.Size(334, 0);
            this.gridControl_label.Name = "gridControl_label";
            this.gridControl_label.Size = new System.Drawing.Size(330, 622);
            this.gridControl_label.TabIndex = 4;
            this.gridControl_label.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_label});
            // 
            // gridView_label
            // 
            this.gridView_label.DetailHeight = 280;
            this.gridView_label.GridControl = this.gridControl_label;
            this.gridView_label.Name = "gridView_label";
            this.gridView_label.OptionsSelection.CheckBoxSelectorField = "check_label";
            this.gridView_label.OptionsSelection.MultiSelect = true;
            this.gridView_label.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView_label.OptionsView.ColumnAutoWidth = false;
            this.gridView_label.OptionsView.ShowGroupPanel = false;
            this.gridView_label.OptionsView.ShowIndicator = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1166, 679);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(340, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(826, 679);
            this.layoutControlGroup2.Text = "物品统计";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl_statistics;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(820, 452);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl_productCount;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 478);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(820, 174);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(306, 452);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(514, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textEdit_lfyQuery;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 452);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(230, 26);
            this.layoutControlItem5.Text = "六分仪类型";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton_cx;
            this.layoutControlItem6.Location = new System.Drawing.Point(230, 452);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(340, 679);
            this.layoutControlGroup1.Text = "仓库标签";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl_label;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(334, 626);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton_getGoods;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(83, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(251, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Frm_仓库查询
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 679);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_仓库查询";
            this.Text = "Frm_仓库查询";
            this.Load += new System.EventHandler(this.Frm_仓库查询_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_lfyQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_productCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_productCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_statistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_statistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControl_label;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_label;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.GridControl gridControl_statistics;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_statistics;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton_getGoods;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl gridControl_productCount;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_productCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit textEdit_lfyQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton simpleButton_cx;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}