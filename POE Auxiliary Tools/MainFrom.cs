using Core;
using Core.Common;
using Core.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static Core.Popup;

namespace POE_Auxiliary_Tools
{
    public partial class MainFrom : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        public static SQLiteHandler database;
        public static List<用户Token> tokenList = new List<用户Token>();
        public MainFrom()
        {
            InitializeComponent();
            database = new SQLiteHandler(Path.GetFullPath(@"../../database.db"));
            //database = new SQLiteHandler(Application.StartupPath + "\\database.db");
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

        private void MainFrom_Load(object sender, EventArgs e)
        {
            //获取POESESSID
            sbr.Clear();
            sbr.Append("SELECT POESESSID FROM 用户属性 ");
            DataTable _dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            MainFrom.tokenList = DataHandler.TableToListModel<用户Token>(_dt);


            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            Open("查询历史");

        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogResult = Popup.Tips(this, "确定要重置POESESSID吗？", "提示信息", PopUpType.question);
            if (dialogResult.Equals(DialogResult.No))
            {
                return;
            }
            //删除POESESSID
            sbr.Clear();
            sbr.Append("DELETE  FROM  用户属性 ");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);

            //弹出窗体提示输入Token
            POESESSID输入 to = new POESESSID输入();
            // 计算窗体在屏幕上的中央位置
            to.StartPosition = FormStartPosition.CenterScreen;
            to.ShowDialog();

        }
        
    }
    public class 用户Token
    {
        public string POESESSID { get; set; }
    }
}
