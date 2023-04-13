using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Auxiliary_Tools.Model
{
    public class 集市物品
    {

        public string 名称 { get; set; }

        public 堆叠数量 数量 { get; set; }

        public int 总价格 { get; set; }

        public double 单价 { get; set; }

        public string 交易通货 { get; set; }

        public string err { get; set; }

    }
    public class 查询物品
    {
        public string 类别名称 { get; set; }
        public string 物品名称 { get; set; }
    }
    public enum 交易通货
    {
        混沌石 = 1,
        崇高石 = 2,
        神圣石 = 3,
    }
    public class 堆叠数量
    {
        public int 数量 { get; set; }

        public int 堆叠上限 { get; set; }
    }
    public class 查询缓存
    {
        public string 查询时间 { get; set; }
        public string 物品名称 { get; set; }
        public string 价格 { get; set; }
        public string 上架时间 { get; set; }
        public string 通货类型 { get; set; }
        public string 物品类型 { get; set; }
        public Int64 数量 { get; set; }

        public Double 总价 { get; set; }
    }
}
