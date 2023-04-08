using Core;
using Core.Common;
using Core.DevControlHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_查询历史 : BaseForm
    {
        StringBuilder sbr = new StringBuilder();
        List<查询记录> records = new List<查询记录>();
        double bl;

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
            //绑定物品名称
            sbr.Clear();
            sbr.Append("SELECT  distinct(物品名称)  FROM  查询记录");
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<查询记录>(dt);
            DevComboBoxEditHandler.BindData(product, list, null, false, "物品名称");
            var data = DateTime.Now;
            this.start.EditValue = data.ToString("yyyy-MM-dd");
            this.end.EditValue = data.ToString("yyyy-MM-dd");

            //价值排行相关

            //绑定物品类型
            DevComboBoxEditHandler.BindData(comboBoxEdit_wplx, GetProductType(), null, true, "类别名称", "id");
            comboBoxEdit_wplx.SelectedIndex = 0;

            //获取最近一次DC比
            sbr.Clear();
            sbr.Append(@"SELECT   *  FROM  查询记录 
                        WHERE 物品名称='神圣石' order by 查询时间 desc  limit 0,1");
            dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            list = DataHandler.TableToListModel<查询记录>(dt);
            bl = Convert.ToDouble(list[0].价格);  //dc比例
            //所有物品的一条记录
            sbr.Clear();
            sbr.Append(@"SELECT * FROM (
                        SELECT a.*,堆叠上限,ROW_NUMBER()OVER(PARTITION BY a.物品名称 ORDER BY 查询时间 DESC) AS rn FROM 查询记录 as a 
                        LEFT JOIN 物品 as b on a.物品名称 = b.物品名称
                        ) 
                        where rn=1  ");
            dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            list = DataHandler.TableToListModel<查询记录>(dt);
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
            gridControl2.DataSource = list;
            records = list;
        }
        /// <summary>
        /// 获取所有物品类别
        /// </summary>
        public List<物品类别> GetProductType()
        {
            sbr.Clear();
            sbr.Append("SELECT * FROM 物品类别 ");
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
            //dc比例

            var row = gridView2.GetFocusedRow();
            var model = ObjectHandler.ConvertObject<查询记录>(row);
            var list = new List<查询记录>();
           
            if (textEdit_sl.Text != "")
            {
                var num = Convert.ToDouble(textEdit_sl.Text);
                double totalPrice_hd;
                double totalPrice_ss;
                if (model.通货类型 == "混沌石")
                {
                    //混沌石价格
                    totalPrice_hd = num * Convert.ToDouble(model.价格);
                    //神圣石价格
                    totalPrice_ss = num * Convert.ToDouble(model.价格)/bl;
                }
                else
                {
                    //混沌石价格
                    totalPrice_hd = num * Convert.ToDouble(model.价格)*bl;
                    //神圣石价格
                    totalPrice_ss = num * Convert.ToDouble(model.价格);
                }
                model.价格_混沌石 = totalPrice_hd.ToString();
                model.价格_神圣石 = totalPrice_ss.ToString();
            }
            list.Add(model);
            gridControl3.DataSource = list;
        }
    }
}
