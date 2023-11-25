using AutoHotkey.Interop;
using Core;
using Core.Common;
using Core.SQLite;
using Path_of_Exile_Tool;
using POE_Auxiliary_Tools.基础数据;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static Core.Popup;

namespace POE_Auxiliary_Tools
{
    public partial class MainFrom : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        public static SQLiteHandler database;
        public static List<用户Token> tokenList = new List<用户Token>();
        //所有要运行的子窗体
        //private Frm_物品管理 frm_物品管理;
        //private Frm_物品类别管理 frm_物品类别管理;
        //private Frm_查询历史 frm_查询历史;
        //private Frm_集市价格查询 frm_集市价格查询;
        public  static List<Form> formList = new List<Form>();
        public MainFrom()
        {
            InitializeComponent();
#if DEBUG
            database = new SQLiteHandler(Path.GetFullPath(@"../../Database/database.db"));
#else
            database = new SQLiteHandler(Application.StartupPath + "\\Database\\database.db");
#endif
            if (SQLiteHandler.DataBaceList.Count==0)
                SQLiteHandler.DataBaceList.Add("database", database);

            OpenAll();//打开所有窗体 
        }
        public static string[] GetFormNames()
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取所有窗体类型
            Type[] formTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(System.Windows.Forms.Form))).ToArray();

            // 获取窗体类名
            string[] formNames = formTypes.Where(x=>x.Name.StartsWith("Frm_")).Select(t => t.Name).ToArray();

            return formNames;
        }
        private void ShowForm(string name)
        {
           
            foreach (var item in formList)
            {
                if (item.Name== "Frm_"+name)
                {
                    item.Visible = true;
                }
                else
                {
                    item.Visible = false;
                }
            }
        }
        private void ReShowForm(Form frm)
        {
            //清除panel里面的其他窗体
            //this.panelControl1.Controls.Clear();
            //将该子窗体设置成非顶级控件
            frm.TopLevel = false;
            //将该子窗体的边框去掉
            frm.FormBorderStyle = FormBorderStyle.None;
            //设置子窗体随容器大小自动调整
            frm.Dock = DockStyle.Fill;
            //设置mdi父容器为当前窗口
            frm.Parent = this.panelControl1;
            frm.Visible = false;
            //子窗体显示
            frm.Show();
        }
        public void OpenAll()
        {
            var lsit = GetFormNames();
            foreach (var item in lsit)
            {
                if (item != "Frm_价格走势"&& item != "Frm_仓库查询")
                {
                    var path = "POE_Auxiliary_Tools." + item;
                    var p = Type.GetType(path);
                    Form form = (Form)Activator.CreateInstance(p);
                    //清除panel里面的其他窗体
                    //this.panelControl1.Controls.Clear();
                    //将该子窗体设置成非顶级控件
                    form.TopLevel = false;
                    //将该子窗体的边框去掉
                    form.FormBorderStyle = FormBorderStyle.None;
                    //设置子窗体随容器大小自动调整
                    form.Dock = DockStyle.Fill;
                    //设置mdi父容器为当前窗口
                    form.Parent = this.panelControl1;
                    form.Visible = false;
                    //子窗体显示
                    form.Show();
                    formList.Add(form);
                }
            }
        }
        public void Open(string name)
        {
            var path = "POE_Auxiliary_Tools.Frm_" + name;
            Form form = (Form)Activator.CreateInstance(Type.GetType(path));
            //清除panel里面的其他窗体
            foreach (var item in formList)
            {
                item.Visible = false;
            }
            //将该子窗体设置成非顶级控件
            form.TopLevel = false;
            //将该子窗体的边框去掉
            form.FormBorderStyle = FormBorderStyle.None;
            //设置子窗体随容器大小自动调整
            form.Dock = DockStyle.Fill;
            //设置mdi父容器为当前窗口
            form.Parent = this.panelControl1;
            form.Visible = false;
            //子窗体显示
            form.Show();

           
        }

        private void 集市价格查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //(formList.SingleOrDefault(x => x.Name == "Frm_集市价格查询") as Frm_集市价格查询).TriggerLoadEvent(); 
            ShowForm("集市价格查询");
        }
        private void 物品类别管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (formList.SingleOrDefault(x => x.Name == "Frm_物品类别管理") as Frm_物品类别管理).TriggerLoadEvent();
            ShowForm("物品类别管理");
        }
        private void 物品管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (formList.SingleOrDefault(x => x.Name == "Frm_物品管理") as Frm_物品管理).TriggerLoadEvent();
            ShowForm("物品管理");
        }
        private void 查询历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (formList.SingleOrDefault(x => x.Name == "Frm_查询历史") as Frm_查询历史).TriggerLoadEvent();
            ShowForm("查询历史");
        }
        private void 地图相关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (formList.SingleOrDefault(x => x.Name == "Frm_地图类工具") as Frm_地图类工具).TriggerLoadEvent();
            ShowForm("地图类工具");
        }
        private void 装备相关ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            (formList.SingleOrDefault(x => x.Name == "Frm_装备类工具") as Frm_装备类工具).TriggerLoadEvent();
            ShowForm("装备类工具");
        }
        private void 仓库查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //(formList.SingleOrDefault(x => x.Name == "Frm_仓库查询") as Frm_仓库查询).TriggerLoadEvent();
            Open("仓库查询");
        }
        private void MainFrom_Load(object sender, EventArgs e)
        {
            



            //获取分辨率
            var resolutionRatio = Screen.PrimaryScreen.Bounds;


          


            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            ShowForm("查询历史");

            //检查用户信息
            sbr.Clear();
            sbr.Append($"SELECT * FROM 用户属性");
            var t = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(t);
            var list = DataHandler.TableToListModel<用户信息Mode>(dt);
            if (list.Count == 0)
            {
                var frm = new 用户信息();
                frm.StartPosition = FormStartPosition.CenterScreen;
                // 禁用最小化按钮和最大化按钮
                frm.MinimizeBox = false;
                frm.MaximizeBox = false;
                frm.ShowDialog();
            }
      
            //获取POESESSID
            sbr.Clear();
            sbr.Append("SELECT POESESSID FROM 用户属性 ");
            DataTable _dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            MainFrom.tokenList = DataHandler.TableToListModel<用户Token>(_dt);

            //baseInfo
            sbr.Clear();
            sbr.Append($"SELECT * FROM 用户属性");
            var s = sbr.ToString();
            DataTable dt2 = MainFrom.database.ExecuteDataTable(t);
            var list2 = DataHandler.TableToListModel<用户信息Mode>(dt);
            if (list2.Count>0)
            {
                Program.baseInfo.论坛名称 = list2[0].论坛名称;
                Program.baseInfo.POESESSID = list2[0].POESESSID;
                Program.baseInfo.赛季 = list2[0].赛季;
            }
           

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
            MainFrom.tokenList =new List<用户Token>();
            //弹出窗体提示输入Token
            POESESSID输入 to = new POESESSID输入();
            // 计算窗体在屏幕上的中央位置
            to.StartPosition = FormStartPosition.CenterScreen;
            to.ShowDialog();

        }

        private void 自动按键ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (formList.SingleOrDefault(x => x.Name == "Frm_自动按键") as Frm_自动按键).TriggerLoadEvent();
            ShowForm("自动按键");
        }

        private void 老版工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (formList.SingleOrDefault(x => x.Name == "Frm_老版工具") as Frm_老版工具).TriggerLoadEvent();
            ShowForm("老版工具");
        }

        private void 用户信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new 用户信息();
            frm.StartPosition = FormStartPosition.CenterScreen;
            // 禁用最小化按钮和最大化按钮
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.ShowDialog();
        }
    }
    public class 用户Token
    {
        public string POESESSID { get; set; }
    }
}
