using Core.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public partial class MainFrom : BaseForm
    {
        public static SQLiteHandler database;
        public MainFrom()
        {
            InitializeComponent();
            database = new SQLiteHandler(Path.GetFullPath(@"../../database.db"));
            SQLiteHandler.DataBaceList.Add("database", database);


        }

        public void Open(string name)
        {
            var path = "POE_Auxiliary_Tools.Frm_" + name;
            Form form = (Form)Activator.CreateInstance(Type.GetType(path));
            //清除panel里面的其他窗体
            this.panelControl1.Controls.Clear();
            //将该子窗体设置成非顶级控件
            form.TopLevel = false;
            //将该子窗体的边框去掉
            form.FormBorderStyle = FormBorderStyle.None;
            //设置子窗体随容器大小自动调整
            form.Dock = DockStyle.Fill;
            //设置mdi父容器为当前窗口
            form.Parent = this.panelControl1;
            //子窗体显示
            form.Show();
        }

        private void 集市价格查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open("集市价格查询");
        }

  

        private void 物品类别管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open("物品类别管理");
        }

        private void 物品管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open("物品管理");
        }

        private void 查询历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open("查询历史");
        }
    }
}
