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
    }
    public class 物品
    {
        public Int64 id { get; set; }
        public string 物品名称 { get; set; }

        public string 说明 { get; set; }
        public Int64 物品类别id   { get; set; }

        public string 类别名称 { get; set; }

        public string  是否可用 { get; set; }
    }
}
