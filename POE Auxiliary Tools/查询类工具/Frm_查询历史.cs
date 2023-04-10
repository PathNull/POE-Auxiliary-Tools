using Core;
using Core.Common;
using Core.DevControlHandler;
using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using POE_Auxiliary_Tools.Model;
using POE_Auxiliary_Tools.查询类工具;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Core.Popup;
using DialogResult = System.Windows.Forms.DialogResult;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_查询历史 : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        List<查询记录> records = new List<查询记录>();
        double bl;
        List<集市物品> resultList = new List<集市物品>();
        public Frm_查询历史()
        {
            InitializeComponent();
            start.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            start.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            start.Properties.Mask.EditMask = "yyyy-MM-dd";
            start.Properties.Mask.UseMaskAsDisplayFormat = true;

            end.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            end.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            end.Properties.Mask.EditMask = "yyyy-MM-dd";
            end.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void Frm_查询历史_Load(object sender, EventArgs e)
        {
            //获取数据更新时间

            DevComboBoxEditHandler.BindData(history_type, GetProductType(), null, false, "类别名称", "id");
            
            textEdit_sl.TextChanged += TextEdit_sl_TextChanged;
         
            var data = DateTime.Now;
            this.start.EditValue = data.ToString("yyyy-MM-dd");
            this.end.EditValue = data.ToString("yyyy-MM-dd");


            //绑定物品类型
            DevComboBoxEditHandler.BindData(comboBoxEdit_wplx, GetProductType(), null, true, "类别名称", "id");
            comboBoxEdit_wplx.SelectedIndex = 0;

            comboBoxEdit_bdfd.SelectedIndex = 1;
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        private void ReData(double tj)
        {
            //价值排行相关
            List<查询记录> list = new List<查询记录>();
            DataTable dt = new DataTable();
            //获取最近一次DC比
            sbr.Clear();
            sbr.Append(@"SELECT   *  FROM  查询记录 
                        WHERE 物品名称='神圣石' order by 查询时间 desc  limit 0,1");
            dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            list = DataHandler.TableToListModel<查询记录>(dt);
            bl = Convert.ToDouble(list[0].价格);  //dc比例

            list = GetData(tj);

            foreach (var item in list)
            {
                if (item.通货类型 == "神圣石")
                {
                    var price = Convert.ToDouble(item.价格) * bl;
                    item.排序价格 = price;
                }
                else
                {
                    item.排序价格 = Convert.ToDouble(item.价格);
                }
            }
            list = list.OrderByDescending(x => x.排序价格).ToList();

            records = list;
        }
        //数量变更
        private void TextEdit_sl_TextChanged(object sender, EventArgs e)
        {
            
        }
        private List<查询记录> GetData(double tj)
        {
            DataTable dt = new DataTable();
            sbr.Clear();
            sbr.Append($@";with tb as (
                        SELECT * FROM (SELECT a.*,堆叠上限,是否可用,ROW_NUMBER()OVER(PARTITION BY a.物品名称 ORDER BY 查询时间 DESC) AS rn FROM 查询记录 as a 
                        LEFT JOIN 物品 as b on a.物品名称 = b.物品名称) 
                        where rn in (1,2)  and 是否可用='是' order by 物品名称
                        ),
                        tb2 as (
                        SELECT 
                            t1.*,t2.价格 as 上次价格,
                            CASE
                                WHEN ABS((CAST(t1.价格 AS float) - CAST(t2.价格 AS float)) / CAST(t2.价格 AS float)) > {tj} THEN '上次价格：'||t2.价格
                                ELSE NULL
                            END AS 标记
                        FROM 
                            (SELECT * FROM tb ORDER BY 查询时间 DESC) t1
                        LEFT JOIN 
                            (SELECT * FROM tb ORDER BY 查询时间 DESC) t2
                        ON 
                            t1.物品名称 = t2.物品名称 AND t1.查询时间 > t2.查询时间
                        GROUP BY 
                            t1.物品名称
		                        )
                        select * from tb2");
            dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            var list = DataHandler.TableToListModel<查询记录>(dt);
            return list;
        }
        /// <summary>
        /// 获取所有物品类别
        /// </summary>
        public List<物品类别> GetProductType()
        {
            sbr.Clear();
            sbr.Append("SELECT * FROM 物品类别 order by 类别名称");
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<物品类别>(dt);
            return list;
        }
        //查询
        private void query_Click(object sender, EventArgs e)
        {
            var start =Convert.ToDateTime(this.start.EditValue).ToString("yyyy-MM-dd")+" 00:00";
            var end = Convert.ToDateTime(this.end.EditValue).ToString("yyyy-MM-dd")+ " 23:59";
            var name = product.Text;
            sbr.Clear();
            sbr.Append($@"SELECT  * FROM  查询记录 where 物品名称='{name}' 
                    and 查询时间 BETWEEN '{start}' and '{end}' order by 查询时间 desc");
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<查询记录>(dt);
            gridControl1.DataSource = list;
        }
        //类别改变
        private void comboBoxEdit_wplx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit_wplx.Text == "<全部>")
            {
                gridControl2.DataSource = records;
            }
            else
            {
                var data = records.Where(x => x.物品类型 == comboBoxEdit_wplx.Text);
                gridControl2.DataSource = data;
            }
           
        }
        //选中价格排行记录
        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            var row = gridView2.GetFocusedRow();
            var model = ObjectHandler.ConvertObject<查询记录>(row);;
            var list = new List<查询记录>();
            double num;
            if (textEdit_sl.Text == "" || textEdit_sl.Text=="0")
            {
                num = Convert.ToDouble(model.堆叠上限);
                if (model.堆叠上限 == 0)
                {
                    num = 1;
                }
            }
            else
            {
                num = Convert.ToDouble(textEdit_sl.Text);
            }
            double totalPrice_hd;
            double totalPrice_ss;
            if (model.通货类型 == "混沌石")
            {
                //混沌石价格
                totalPrice_hd = num * Convert.ToDouble(model.价格);
                //神圣石价格
                totalPrice_ss = num * Convert.ToDouble(model.价格) / bl;
            }
            else
            {
                //混沌石价格
                totalPrice_hd = num * Convert.ToDouble(model.价格) * bl;
                //神圣石价格
                totalPrice_ss = num * Convert.ToDouble(model.价格);
            }
            model.价格_混沌石 = Math.Round(totalPrice_hd, 2).ToString();
            model.价格_神圣石 = Math.Round(totalPrice_ss, 2).ToString();
            model.上架数量 = num.ToString();
            list.Add(model);
            gridControl3.DataSource = list;
        }

        private void history_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            product.SelectedIndex = -1;
            //绑定物品名称
            sbr.Clear();
            sbr.Append($@"SELECT  distinct(物品名称) AS  物品名称  FROM  物品  WHERE 物品类别id='{((ListItem)history_type.SelectedItem).Value}' order by 物品名称");
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<查询记录>(dt);
            DevComboBoxEditHandler.BindData(product, list, null, false, "物品名称");
        }
        //双击价值排行记录，显示价格走势
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);
            //双击行的数据
            var row = view.GetFocusedRow();
            var model = ObjectHandler.ConvertObject<查询记录>(row); ;
            if (info.InRow)
            {
                Frm_价格走势 zs = new Frm_价格走势(model.物品名称);
                // 计算窗体在屏幕上的中央位置
                zs.StartPosition = FormStartPosition.CenterScreen;
                zs.ShowDialog();
            }
        }
        //价格历史标记出波动大的
        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                string price = view.GetRowCellDisplayText(e.RowHandle, view.Columns["标记"]);
                if (price.Contains("上次价格"))
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }
        //波动标记幅度变更
        private void comboBoxEdit_bdfd_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReData(GetTJ());
            if (comboBoxEdit_wplx.Text == "<全部>")
            {
                gridControl2.DataSource = records;
            }
            else
            {
                var data = records.Where(x => x.物品类型 == comboBoxEdit_wplx.Text);
                gridControl2.DataSource = data;
            }
        }
        private double GetTJ()
        {
            var text = comboBoxEdit_bdfd.Text;
            double tj = 0;
            switch (text)
            {
                case "波动超10%的标注":
                    tj = 0.1;
                    break;
                case "波动超20%的标注":
                    tj = 0.2;
                    break;
                case "波动超30%的标注":
                    tj = 0.3;
                    break;
                case "波动超40%的标注":
                    tj = 0.4;
                    break;
                case "波动超50%的标注":
                    tj = 0.5;
                    break;
                case "波动超60%的标注":
                    tj = 0.6;
                    break;
                case "波动超70%的标注":
                    tj = 0.7;
                    break;
                case "波动超80%的标注":
                    tj = 0.8;
                    break;
                case "波动超90%的标注":
                    tj = 0.9;
                    break;
                default:
                    break;
            }
            return tj;
        }
        //右键查询该物品价格
        private void ProductQuery_Click(object sender, EventArgs e)
        {
          
            var rows = gridView2.GetSelectedRows();
            int rowId = 0;
            if (rows.Length > 0)
            {
                rowId = rows[0];
                var model = DevGridControlHandler.GetSelectModel<查询记录>(gridView2);
                Thread thread = new Thread(() =>
                {
                    StartQuery(model);
                    if (gridControl2.InvokeRequired)
                    {
                        // 选中具有指定值的行
                       
                        Action SetSource = delegate { gridView2.FocusedRowHandle = gridView2.LocateByValue("物品名称", model.物品名称); };
                        gridControl2.Invoke(SetSource);
                    }
                    else
                    {
                        gridView2.FocusedRowHandle = gridView2.LocateByValue("物品名称", model.物品名称);
                    }
                        
                });
                thread.Start();
            }
        }
        // 在UI线程上显示消息框
        void ShowMessageBox(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => MessageBox.Show(this, "请求超时！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information)));
            }
            else
            {
                MessageBox.Show(this, "请求超时！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void StartQuery(查询记录 recordModel)
        {
            //需要查询的物品
            var keyResult = MarketQueryHandle.GetKeyList(recordModel.物品名称, recordModel.通货类型 == "混沌石" ? "chaos" : "divine");
            if (keyResult["错误的请求"] != null)
            {
          
                ShowMessageBox("请求超时");

            }
            //获取物品
            sbr.Clear();
            sbr.Append($@"SELECT * FROM 物品 LEFT JOIN 物品类别 
                    ON 物品.物品类别id= 物品类别.id WHERE 物品名称='{recordModel.物品名称}'");
            DataTable _dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            var list  = DataHandler.TableToListModel<物品>(_dt);
            resultList = new List<集市物品>();
            var model = MarketQueryHandle.GetPrice(resultList,keyResult, recordModel.物品名称, list[0].类别名称, list[0].允许堆叠, list[0].通货类型, (int)list[0].最低数量, 0);
            //请求错误
            if (model == null)
            {
                ShowMessageBox("请求超时");
            
            }

            ReData(GetTJ());

            if (comboBoxEdit_wplx.Text == "<全部>")
            {
                if (gridControl2.InvokeRequired)
                {
                    Action SetSource = delegate { gridControl2.DataSource = records; };
                    gridControl2.Invoke(SetSource);
                }
                else
                {
                    gridControl2.DataSource = records;
                }
              
            }
            else
            {
                var data = records.Where(x => x.物品类型 == comboBoxEdit_wplx.Text);
                if (gridControl2.InvokeRequired)
                {
                    Action SetSource = delegate { gridControl2.DataSource = data; };
                    gridControl2.Invoke(SetSource);
                }
                else
                {
                    gridControl2.DataSource = data;
                }
            }
           
        }
    }
}
