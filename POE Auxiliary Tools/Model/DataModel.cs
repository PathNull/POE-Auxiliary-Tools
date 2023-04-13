using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Auxiliary_Tools
{
    public class 物品类别
    {
        public Int64 id { get; set; }
        public string 类别名称 { get; set; }

        public string 说明 { get; set; }

        public string 允许堆叠 { get; set; }
    }
    public class 物品
    {
        public Int64 id { get; set; }
        public string 物品名称 { get; set; }

        public string 说明 { get; set; }
        public Int64 物品类别id   { get; set; }

        public string 类别名称 { get; set; }

        public string  是否可用 { get; set; }

        public Int64 最低数量 { get; set; }
        public Int64 堆叠上限 { get; set; }

        public string 通货类型 { get; set; }

        public string 允许堆叠 { get; set; }

        public string 搜索id { get; set; }
    }
    public class 查询记录
    {
        public Int64 id { get; set; }
        public string 查询时间 { get; set; }

        public string 物品名称 { get; set; }

        public string 物品类型 { get; set; }

        public string 价格 { get; set; }

        public string 通货类型 { get; set; }

        public double 排序价格 { get; set; }

        public Int64 堆叠上限 { get; set; }

        public string 上架数量 { get; set; }
         
        public string 价格_神圣石 { get; set; }

        public string 价格_混沌石 { get; set; }

        public string 标记 { get; set; }

        public string 上次价格 { get; set; }
    }
}
