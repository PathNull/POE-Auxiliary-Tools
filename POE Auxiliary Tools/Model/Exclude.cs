using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_of_Exile_Tool.Model
{
    /// <summary>
    /// 获取下标时需要排除的
    /// </summary>
    public class Exclude
    {
        public Exclude()
        {
            this.List = new List<string>();
            this.List.Add("物品类别");
            this.List.Add("\r\n需求");
            this.List.Add("\r\n护甲");
            this.List.Add("\r\n物品等级");
            this.List.Add("\r\n暴击几率");
            this.List.Add("\r\n闪避值");
            this.List.Add("\r\n能量护盾");
        }
        public List<string> List { get; set; }
    }
}
