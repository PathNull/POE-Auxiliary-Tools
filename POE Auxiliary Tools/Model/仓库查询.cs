using EntRail.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Auxiliary_Tools.Model
{
    public class 仓库查询
    {

    }
    public class 仓库标签
    {
        public string 标签名称 { get; set;}
        [View(ColumnOption.Hidden)]

        public string check_label { get; set;}
    }
    public class 仓库物品
    {
        [View("icon", ColumnOption.Hidden)]
        public string 图标地址 { get; set; }
        [View("物品名称")]
        public string 物品名称 { get; set; }
        [View("enchantMods", ColumnOption.Hidden)]
        public List<string> 物品说明 { get; set; }
        [View("explicitMods", ColumnOption.Hidden)]
        public List<string> 装备属性 { get; set; }
        [View("堆叠数量")]
        public int 堆叠数量 { get;set; }
        [View("物品单价")]
        public string 物品单价 { get;set; }
        [View("物品总价(混沌石)")]
        public string 物品总价_混沌石 { get; set; }
        [View("物品总价(神圣石)")]
        public string 物品总价_神圣石 { get; set; }
        [View(ColumnOption.Hidden)]
        public double 排序 { get;set; }

        public string 说明 { get; set; }
    }
    public class 充能罗盘 { 
        public string 罗盘类型 { get; set;}

        public int 数量 { get;set;}

        [View("总价(混沌石)")]
        public string 物品总价_混沌石 { get; set; }
        [View("总价(神圣石)")]
        public string 物品总价_神圣石 { get; set; }
    }

}
