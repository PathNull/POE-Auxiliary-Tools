using CSRTMISYC.Core;
using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace POE_Auxiliary_Tools
{
    public partial class MainFrom : BaseForm
    {
        public MainFrom()
        {
            InitializeComponent();
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
            //XMLHandler.CreateXMLDocument("集市价格查询");
            //XMLHandler.CreateNode("集市价格查询","重铸石");
            //XMLHandler.CreateNode("集市价格查询","改造石");
            //XMLHandler.Read("集市价格查询");
        }
    }
}
