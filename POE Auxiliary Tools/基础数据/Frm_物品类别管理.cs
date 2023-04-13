using Core;
using Core.Common;
using Core.DevControlHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using static Core.Popup;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_物品类别管理 : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        public Frm_物品类别管理()
        {
            InitializeComponent();
          
         




        }
        public void TriggerLoadEvent()
        {
            this.OnLoad(EventArgs.Empty);
        }
        //点击物品类别记录

        private void gridView_lb_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            物品类别 model = GetSelectModel();
            textEdit_name.Text = model.类别名称;
            textEdit_sm.Text = model.说明;
            simpleButton_save.Text = "修改";
        }
        //保存或者修改
        private void simpleButton_save_Click(object sender, EventArgs e)
        {
            var typeName = textEdit_name.Text;
            var sm = textEdit_sm.Text;
            if (typeName == "")
            {
                dialogResult = Popup.Tips(this, "请输入类别名称！", "提示信息", PopUpType.Info);
                return;
            }
            var btnText = simpleButton_save.Text;
            if (btnText == "添加")
            {
                var d = new Dictionary<string, string>();
                d.Add("类别名称", typeName);
                if (!MainFrom.database.IsExist("物品类别", d))
                {
                    sbr.Clear();
                    sbr.Append("INSERT INTO 物品类别 (类别名称,说明,删除标记) VALUES ");
                    sbr.Append($"('{typeName}','{sm}',0);");
                    var cmdText = sbr.ToString();
                    MainFrom.database.ExecuteNonQuery(cmdText);
                }
                else
                {
                    dialogResult = Popup.Tips(this, "类别名称已存在！", "提示信息", PopUpType.Info);

                }
            }
            else
            {
                物品类别 model = GetSelectModel();
                if (model.类别名称 != typeName)
                {
                    var d = new Dictionary<string, string>();
                    d.Add("类别名称", typeName);
                    if (!MainFrom.database.IsExist("物品类别", d))
                    {
                        sbr.Clear();
                        sbr.Append("UPDATE 物品类别 SET ");
                        sbr.Append($"类别名称='{typeName}', ");
                        sbr.Append($"说明='{sm}' ");
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
                    sbr.Append("UPDATE 物品类别 SET ");
                    sbr.Append($"类别名称='{typeName}', ");
                    sbr.Append($"说明='{sm}'");
                    sbr.Append($"WHERE id={model.id};");
                    var cmdText = sbr.ToString();
                    MainFrom.database.ExecuteNonQuery(cmdText);
                    Reset();
                }
                 
               
            }
            Reset();
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void ReData()
        {
            sbr.Clear();
            sbr.Append("SELECT * FROM 物品类别 order by 类别名称");
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<物品类别>(dt);
            gridControl_lb.DataSource = list;
        }
        //双击删除记录
        private void gridView_lb_DoubleClick(object sender, EventArgs e)
        {
            物品类别 model = GetSelectModel();
            //判断是否是罗盘
            if (model.类别名称 == "充能罗盘")
            {
                dialogResult = Popup.Tips(this, "该类别无法删除！", "提示信息", PopUpType.Info);
                return;
            }
            //判断是否有关联的物品
            sbr.Clear();
            sbr.Append($@"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id  
                        WHERE 类别名称='{model.类别名称}'");
            DataTable dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            var list = DataHandler.TableToListModel<物品类别>(dt);
            if (list.Count > 0)
            {
                dialogResult = Popup.Tips(this, "该类别已经关联了物品，暂时无法删除！", "提示信息", PopUpType.Info);
                return;
            }

            dialogResult = Popup.Tips(this, "确定要删除物品类别吗？", "提示信息", PopUpType.question);
            if (dialogResult.Equals(DialogResult.No))
            {
                return;
            }
            
            sbr.Clear();
            sbr.Append("DELETE FROM 物品类别 ");
            sbr.Append($"WHERE 类别名称='{model.类别名称}';");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
            Reset();
        }
       
        public void Reset()
        {
            simpleButton_save.Text = "添加";
            textEdit_name.Text = "";
            textEdit_sm.Text = "";
            ReData();
        }

        public 物品类别 GetSelectModel()
        {
            var row = gridView_lb.GetSelectedRows();
            var obj = gridView_lb.GetRow(row[0]);
            物品类别 model = ObjectHandler.ConvertObject<物品类别>(obj);
            return model;
        }

        private void Frm_物品类别管理_Load(object sender, EventArgs e)
        {
            ReData();
        }
    }
}
