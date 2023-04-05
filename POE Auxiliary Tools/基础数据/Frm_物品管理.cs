using Core;
using Core.Common;
using Core.DevControlHandler;
using DevExpress.XtraEditors;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Model.Common.Popup;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_物品管理 : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        public Frm_物品管理()
        {
            InitializeComponent();
            var list = new List<物品>();
            list.Add(new 物品() { 是否可用 = "是" });
            list.Add(new 物品() { 是否可用 = "否" });
            DevComboBoxEditHandler.BindData(comboBoxEdit_lb, GetProductType(), null, false, "类别名称","id");
            DevComboBoxEditHandler.BindData(comboBoxEdit_cx, GetProductType(), null, true, "类别名称", "id");
            DevComboBoxEditHandler.BindData(comboBoxEdit_sfky, list, null, false, "是否可用");
            comboBoxEdit_cx.SelectedIndex = 0;
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
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void ReData()
        {
            var typeId = ((ListItem)comboBoxEdit_cx.SelectedItem).Value;
            sbr.Clear();
            if (comboBoxEdit_cx.Text != "<全部>")
            {
                sbr.Append($"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id where 物品类别id={typeId} order by 类别名称");
            }
            else
            {
                sbr.Append($"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id order by 类别名称");
            }
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<物品>(dt);
            gridControl_wp.DataSource = list;
        }

        private void simpleButton_save_Click(object sender, EventArgs e)
        {
            var typeName = comboBoxEdit_lb.Text;
            var productName = textEdit_wpmc.Text;
            var isEnable = comboBoxEdit_sfky.Text;
            var sm = buttonEdit_sm.Text;
            if (typeName == "")
            {
                dialogResult = Popup.Tips("请选择类别！", "提示信息", PopUpType.Info);
                return;
            }
            if (productName == "")
            {
                dialogResult = Popup.Tips("请输入物品名称！", "提示信息", PopUpType.Info);
                return;
            }
            var typeId = ((ListItem)comboBoxEdit_lb.SelectedItem).Value;
            var btnText = simpleButton_save.Text;
            if (btnText == "添加")
            {
                var d = new Dictionary<string, string>();
                d.Add("物品名称", productName);
                if (!MainFrom.database.IsExist("物品", d))
                {
                    sbr.Clear();
                    sbr.Append("INSERT INTO 物品 (物品名称,说明,物品类别id,删除标记,是否可用) VALUES ");
                    sbr.Append($"('{productName}','{sm}','{typeId}',0,'是');");
                    var cmdText = sbr.ToString();
                    MainFrom.database.ExecuteNonQuery(cmdText);
                    Reset();
                }
                else
                {
                    dialogResult = Popup.Tips("类别名称已存在！", "提示信息", PopUpType.Info);
                    return;

                }
              
            }
            else
            {
                
                物品 model = GetSelectModel();
                if(model.物品名称!= productName)
                {
                    var d = new Dictionary<string, string>();
                    d.Add("物品名称", productName);
                    if (!MainFrom.database.IsExist("物品", d))
                    {
                        sbr.Clear();
                        sbr.Append("UPDATE 物品 SET ");
                        sbr.Append($"物品名称='{productName}', ");
                        sbr.Append($"说明='{sm}', ");
                        sbr.Append($"物品类别id='{typeId}', ");
                        sbr.Append($"是否可用='{isEnable}' ");
                        sbr.Append($"WHERE id={model.id};");
                        var cmdText = sbr.ToString();
                        MainFrom.database.ExecuteNonQuery(cmdText);
                        Reset();
                    }
                    else
                    {
                        dialogResult = Popup.Tips("类别名称已存在！", "提示信息", PopUpType.Info);
                        Reset();
                        return;

                    }
                }
                else
                {
                    sbr.Clear();
                    sbr.Append("UPDATE 物品 SET ");
                    sbr.Append($"物品名称='{productName}', ");
                    sbr.Append($"说明='{sm}', ");
                    sbr.Append($"物品类别id='{typeId}' ,");
                    sbr.Append($"是否可用='{isEnable}' ");
                    sbr.Append($"WHERE id={model.id};");
                    var cmdText = sbr.ToString();
                    MainFrom.database.ExecuteNonQuery(cmdText);
                    Reset();
                }
             
            }
           
        }

        public void Reset()
        {
            simpleButton_save.Text = "添加";
            comboBoxEdit_lb.SelectedIndex = -1;
            textEdit_wpmc.Text = "";
            buttonEdit_sm.Text = "";
            ReData();
        }

        public 物品 GetSelectModel()
        {
            var row = gridView_wp.GetSelectedRows();
            if (row.Length > 0)
            {
                var obj = gridView_wp.GetRow(row[0]);
                物品 model = ObjectHandler.ConvertObject<物品>(obj);
                return model;
            }
            else
            {
                return null;
            }
          
        }

        private void gridView_wp_DoubleClick(object sender, EventArgs e)
        {
            dialogResult = Popup.Tips("确定要删除物品吗？", "提示信息", PopUpType.question);
            if (dialogResult.Equals(DialogResult.No))
            {
                return;
            }
            物品 model = GetSelectModel();
            sbr.Clear();
            sbr.Append("DELETE FROM 物品 ");
            sbr.Append($"WHERE 物品名称={model.物品名称};");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
            Reset();
        }
        //查询
        private void comboBoxEdit_cx_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReData();
        }
        //点击物品记录
        private void gridView_wp_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            物品 model = GetSelectModel();
            DevComboBoxEditHandler.SetItemByText(comboBoxEdit_lb,model.类别名称);
            DevComboBoxEditHandler.SetItemByText(comboBoxEdit_sfky, model.是否可用);
            textEdit_wpmc.Text = model.物品名称;
            buttonEdit_sm.Text = model.说明;
            simpleButton_save.Text = "修改";
        }
    }
}
