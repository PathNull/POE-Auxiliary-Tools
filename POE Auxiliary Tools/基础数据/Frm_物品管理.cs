﻿using Core;
using Core.Common;
using Core.DevControlHandler;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Core.Popup;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_物品管理 : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        public Frm_物品管理()
        {
            InitializeComponent();
         
        }
        /// <summary>
        /// 获取所有物品类别
        /// </summary>
        public List<物品类别> GetProductType(string name="")
        {
            sbr.Clear();
            if (name == "")
            {
                sbr.Append("SELECT * FROM 物品类别  order by 类别名称");
            }
            else
            {
                sbr.Append($"SELECT * FROM 物品类别 WHERE 类别名称='{name}' order by 类别名称");
            }
           
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
                sbr.Append($"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id where 物品类别id={typeId} order by 类别名称,物品名称");
            }
            else
            {
                sbr.Append($"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id order by 类别名称,物品名称");
            }
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<物品>(dt);
            foreach (var item in list)
            {
                if (item.是否可用 == "是")
                {
                    item.是否可用 = "True";
                }
                else if (item.是否可用 == "否")
                {
                    item.是否可用 = "False";
                }
            }
            gridControl_wp.DataSource = list;
        }

        private void simpleButton_save_Click(object sender, EventArgs e)
        {
            var typeName = comboBoxEdit_lb.Text;
            var productName = textEdit_wpmc.Text;
            var sm = buttonEdit_sm.Text;
            var zd = textEdit_zdsl.Text;
            var sx = textEdit_ddsx.Text;
            var thlx = comboBoxEdit__thlx.Text;
            var ssId = textEdit_ssId.Text;
            var yxdd = comboBoxEdit_yxdd.Text;
            if (typeName == "")
            {
                dialogResult = Popup.Tips(this,"请选择类别！", "提示信息", PopUpType.Info);
                return;
            }
            if (productName == "")
            {
                dialogResult = Popup.Tips(this, "请输入物品名称！", "提示信息", PopUpType.Info);
                return;
            }
            if ((zd == ""|| Convert.ToInt64(zd)==0) && textEdit_zdsl.Enabled == true)
            {
                dialogResult = Popup.Tips(this, "请输入最低数量！", "提示信息", PopUpType.Info);
                return;
            }
            if ((sx == "" || Convert.ToInt64(sx) == 0) && textEdit_ddsx.Enabled == true)
            {
                dialogResult = Popup.Tips(this, "请输入堆叠上限！", "提示信息", PopUpType.Info);
                return;
            }
            if (thlx == "")
            {
                dialogResult = Popup.Tips(this, "请选择通货类型！", "提示信息", PopUpType.Info);
                return;
            }
            if (yxdd == "")
            {
                dialogResult = Popup.Tips(this, "请选择是否允许堆叠！", "提示信息", PopUpType.Info);
                return;
            }
            var typeId = ((ListItem)comboBoxEdit_lb.SelectedItem).Value;
            var btnText = simpleButton_save.Text;
            //处理最低数量和堆叠上限值
            if(textEdit_zdsl.Enabled == false)
            {
                zd = "0";
                sx = "0";
            }
            if (btnText == "添加")
            {
                var d = new Dictionary<string, string>();
                d.Add("物品名称", productName);
                if (!MainFrom.database.IsExist("物品", d))
                {
                    sbr.Clear();
                    sbr.Append("INSERT INTO 物品 (物品名称,说明,物品类别id,删除标记,是否可用,最低数量,堆叠上限,通货类型,搜索id,允许堆叠) VALUES ");
                    sbr.Append($"('{productName}','{sm}','{typeId}',0,'是',{zd},{sx},'{thlx}','{ssId}','{yxdd}')");
                    var cmdText = sbr.ToString();
                    MainFrom.database.ExecuteNonQuery(cmdText);
                    //Reset();
                    textEdit_wpmc.Text = "";
                }
                else
                {
                    dialogResult = Popup.Tips(this, "类别名称已存在！", "提示信息", PopUpType.Info);
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
                        sbr.Append($"最低数量='{zd}' ,");
                        sbr.Append($"堆叠上限='{sx}', ");
                        sbr.Append($"通货类型='{thlx}', ");
                        sbr.Append($"搜索id='{ssId}', ");
                        sbr.Append($"允许堆叠='{yxdd}' ");
                        sbr.Append($"WHERE id={model.id};");
                        var cmdText = sbr.ToString();
                        MainFrom.database.ExecuteNonQuery(cmdText);
                        Reset();
                    }
                    else
                    {
                        dialogResult = Popup.Tips(this, "类别名称已存在！", "提示信息", PopUpType.Info);
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
                    sbr.Append($"最低数量='{zd}' ,");
                    sbr.Append($"堆叠上限='{sx}', ");
                    sbr.Append($"通货类型='{thlx}', ");
                    sbr.Append($"搜索id='{ssId}', ");
                    sbr.Append($"允许堆叠='{yxdd}' ");
                    sbr.Append($"WHERE id={model.id};");
                    var cmdText = sbr.ToString();
                    MainFrom.database.ExecuteNonQuery(cmdText);
                    Reset();
                }
             
            }
            ReData();


        }

        public void Reset()
        {
            simpleButton_save.Text = "添加";
            comboBoxEdit_lb.SelectedIndex = -1;
            textEdit_wpmc.Text = "";
            buttonEdit_sm.Text = "";
            textEdit_zdsl.Text = "";
            textEdit_ddsx.Text = "";
            comboBoxEdit__thlx.Text = "";
            textEdit_ssId.Text = "";
            comboBoxEdit_yxdd.Text = "";
            ReData();
        }

        public 物品 GetSelectModel()
        {
            var row = gridView_wp.GetFocusedRow();
            if (row!=null)
            {
                物品 model = ObjectHandler.ConvertObject<物品>(row);
                return model;
            }
            else
            {
                return null;
            }
          
        }

        private void gridView_wp_DoubleClick(object sender, EventArgs e)
        {
            dialogResult = Popup.Tips(this, "确定要删除物品吗？", "提示信息", PopUpType.question);
            if (dialogResult.Equals(DialogResult.No))
            {
                return;
            }
            物品 model = GetSelectModel();
            sbr.Clear();
            sbr.Append("DELETE FROM 物品 ");
            sbr.Append($"WHERE 物品名称='{model.物品名称}';");
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
            DevComboBoxEditHandler.SetItemByText(comboBoxEdit_lb, model.类别名称);
            DevComboBoxEditHandler.SetItemByText(comboBoxEdit_yxdd, model.允许堆叠);
            textEdit_wpmc.Text = model.物品名称;
            buttonEdit_sm.Text = model.说明;
            textEdit_zdsl.Text = model.最低数量.ToString();
            textEdit_ddsx.Text = model.堆叠上限.ToString();
            comboBoxEdit__thlx.Text = model.通货类型;
            textEdit_ssId.Text = model.搜索id;
            simpleButton_save.Text = "修改";

        }
        //物品类别变化
        private void comboBoxEdit_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //判断是否能够堆叠，不能堆叠的禁用最低数量和堆叠上限
            var type = comboBoxEdit_lb.Text;
            var list = GetProductType(type);
            if (list[0].允许堆叠 == "否")
            {
                textEdit_zdsl.Text = "";
                textEdit_ddsx.Text = "";
                textEdit_zdsl.Enabled = false;
                textEdit_ddsx.Enabled = false;
            }
            else
            {
                textEdit_zdsl.Enabled = true;
                textEdit_ddsx.Enabled = true;
            }
        }

        private void simpleButton_cx_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void Frm_物品管理_Load(object sender, EventArgs e)
        {
            var list = new List<物品>();
            list.Add(new 物品() { 是否可用 = "是" });
            list.Add(new 物品() { 是否可用 = "否" });
            var list2 = new List<物品>();
            list.Add(new 物品() { 允许堆叠 = "是" });
            list.Add(new 物品() { 允许堆叠 = "否" });
            DevComboBoxEditHandler.BindData(comboBoxEdit_lb, GetProductType(), null, false, "类别名称", "id");
            DevComboBoxEditHandler.BindData(comboBoxEdit_cx, GetProductType(), null, true, "类别名称", "id");
            DevComboBoxEditHandler.BindData(comboBoxEdit_yxdd, list, null, false, "允许堆叠");
            comboBoxEdit_cx.SelectedIndex = 0;
        }
        public void TriggerLoadEvent()
        {
            this.OnLoad(EventArgs.Empty);
        }

        private void comboBoxEdit_yxdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit_yxdd.Text == "否")
            {
                textEdit_zdsl.Enabled= false;
                textEdit_ddsx.Enabled = false;
                textEdit_zdsl.Text = "";
                textEdit_ddsx.Text = "";
            }
            else
            {
                textEdit_zdsl.Enabled = true;
                textEdit_ddsx.Enabled = true;
                textEdit_zdsl.Text = "";
                textEdit_ddsx.Text = "";
            }
        }

        private void gridView_wp_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;
            e.Appearance.Options.UseBackColor = true;
        }

        private void gridView_wp_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Caption == "Selection")
            {
                // 列中包含CheckBox编辑器
                var model = gridView_wp.GetFocusedRow() as 物品;
                var value = model.是否可用 == "True" ? "否" : "是";
                sbr.Clear();
                sbr.Append("UPDATE 物品 SET ");
                sbr.Append($@"是否可用='{value}' ");
                sbr.Append($"WHERE 物品名称='{model.物品名称}';");
                var cmdText = sbr.ToString();
                MainFrom.database.ExecuteNonQuery(cmdText);
            }
        }

        
    }
}
