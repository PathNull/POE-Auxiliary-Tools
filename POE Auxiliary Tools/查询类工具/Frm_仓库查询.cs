using Core;
using Core.DevControlHandler;
using DevExpress.XtraEditors;
using EntRail;
using POE_Auxiliary_Tools.Model;
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

namespace POE_Auxiliary_Tools
{
    public partial class Frm_仓库查询 : BaseForm
    {
        List<充能罗盘> lpList = new List<充能罗盘>();
        public Frm_仓库查询()
        {
            InitializeComponent();
            UI.InitView(gridView_label, false);
            UI.InitView(gridView_statistics, false);
            UI.InitView(gridView_productCount, false);
        }
        public void TriggerLoadEvent()
        {
            this.OnLoad(EventArgs.Empty);
        }

        private void Frm_仓库查询_Load(object sender, EventArgs e)
        {
            var list =  WarehouseQueryHandler.GetWarehouseLabel(Program.baseInfo.论坛名称);
            UI.LinkData(gridView_label, list);
        }
        //点击获取物品
        private void simpleButton_getGoods_Click(object sender, EventArgs e)
        {
            UI.LinkData(gridView_statistics, new List<仓库物品>());
            var rows = gridView_label.GetSelectedRows();
            if (rows==null)
            {
                XtraMessageBox.Show("请先选择仓库标签页！");
                return;
            }
            List<仓库物品> result = new List<仓库物品>();
            Random rd = new Random();
            foreach (var index in rows)
            {
                var row = gridView_label.GetRow(index) as 仓库标签;
                var list = WarehouseQueryHandler.GetGoodsByLabel(Program.baseInfo.论坛名称, index);
                result = result.Concat(list).ToList();

                Thread.Sleep(rd.Next(3000, 5000));
            }
            //全部物品
            UI.LinkData(gridView_statistics, result);
            //六分仪
            lpList = new List<充能罗盘>();
            var lfyList = result.Where(x => x.物品名称 == "充能罗盘");
            var disList = lfyList.DistinctBy(x => x.物品说明[0]);
            foreach (var item in disList)
            {
                lpList.Add(new 充能罗盘() { 罗盘类型 = item.物品说明[0], 数量 = lfyList.Count(x => x.物品说明[0] == item.物品说明[0]) });
            }
            UI.LinkData(gridView_productCount, lpList);
        }

        private void textEdit_lfyQuery_TextChanged(object sender, EventArgs e)
        {
            var text = textEdit_lfyQuery.Text;
            UI.LinkData(gridView_productCount, lpList.Where(x => x.罗盘类型.Contains(text)).ToList());
        }
    }
}
