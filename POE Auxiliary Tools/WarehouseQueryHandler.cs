using Core.Common;
using Core.Web;
using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using POE_Auxiliary_Tools.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace POE_Auxiliary_Tools
{
    /// <summary>
    /// 仓库查询助手
    /// </summary>
    public static class WarehouseQueryHandler
    {
        /// <summary>
        /// 获取所有仓库标签
        /// </summary>
        /// <param name="forumName">论坛名称</param>
        /// <returns></returns>
        public static List<仓库标签> GetWarehouseLabel(string forumName)
        {
            List<仓库标签> labelList = new List<仓库标签>();
            var url = $"https://poe.game.qq.com/character-window/get-stash-items?accountName={forumName}&realm=pc&league=S{Program.baseInfo.赛季}赛季&tabs=1&tabIndex=0";
            var list = HttpUitls.Get(url, MainFrom.tokenList[0].POESESSID);
            JObject jsonObject = JObject.Parse(list);
            foreach (var item in jsonObject["tabs"])
            {
                var label = item["n"].ToString();
                labelList.Add(new 仓库标签() { 标签名称=label });
            }
            return labelList;
        }
        /// <summary>
        /// 获取标签下的物品
        /// </summary>
        /// <param name="forumName">论坛名称</param>
        /// <param name="labelIndex">标签页下标</param>
        /// <returns></returns>

        public static List<仓库物品> GetGoodsByLabel(string forumName,int labelIndex)
        {
            List<仓库物品> result = new List<仓库物品>();
            var url = $"https://poe.game.qq.com/character-window/get-stash-items?accountName={forumName}&realm=pc&league=S{Program.baseInfo.赛季}%E8%B5%9B%E5%AD%A3&tabs=0&tabIndex={labelIndex}";
            var list = HttpUitls.Get(url, MainFrom.tokenList[0].POESESSID);
            JObject jsonObject = JObject.Parse(list);
            foreach (var item in jsonObject["items"])
            {
                var icon = item["icon"]==null?"":item["icon"].ToString(); //物品图标
                var baseType = item["baseType"] == null ? "" : item["baseType"].ToString();   //物品类型
                List<string> enchantMods = new List<string>();
                //物品说明
                if (item["enchantMods"]!=null)
                {
                    foreach (var em in item["enchantMods"])
                    {
                        enchantMods.Add(em.ToString());
                    }
                }
                //装备属性
                List<string> explicitMods = new List<string>();
                if (item["explicitMods"] != null) 
                {
                    foreach (var em in item["explicitMods"])
                    {
                        explicitMods.Add(em.ToString());
                    }
                }
                //堆叠数量
                var count = 1;
                if (item["properties"] != null)
                {
                    foreach (var em in item["properties"])
                    {
                        if (em["name"].ToString()=="堆叠数量")
                        {
                            var val = em["values"].ToArray()[0][0].ToString().Split('/')[0];
                            count = Convert.ToInt32(val);
                        }
                    }
                }
                //数据库读取物品单价
                StringBuilder sbr = new StringBuilder();
                sbr.Clear();
                sbr.Append("SELECT  * FROM 查询记录 WHERE 物品名称='神圣石' ORDER BY 查询时间 DESC");
                DataTable dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
                double dc = 0;//DC比例
                if (dt.Rows.Count > 0)
                {
                    dc = Math.Round(Convert.ToDouble(dt.Rows[0]["价格"]),2);
                }
                //查询该物品的单价
                sbr.Clear();
                sbr.Append($"SELECT  * FROM 查询记录 WHERE 物品名称='{baseType}' ORDER BY 查询时间 DESC");
                DataTable tb = MainFrom.database.ExecuteDataTable(sbr.ToString());
                double price = 0;
                if (tb.Rows.Count > 0)
                {
                    if (tb.Rows[0]["通货类型"].ToString()=="混沌石")
                    {
                        price = Math.Round(Convert.ToDouble(tb.Rows[0]["价格"]), 2);
                    }
                    else
                    {
                        price = Math.Round(Convert.ToDouble(tb.Rows[0]["价格"])*dc, 2);
                    }
                }
                
                result.Add(
                    new 仓库物品() {
                        图标地址 = icon,
                        物品名称=baseType,
                        物品说明= enchantMods,
                        装备属性= explicitMods,
                        堆叠数量= count,
                        物品单价= price==-1?"":price.ToString(), 
                        物品总价_混沌石 = price == 0 ? "" : Math.Round((price*count),2).ToString(),
                        物品总价_神圣石 = price == 0 ? "" : Math.Round((price*count/dc),2).ToString(),
                        排序 = Math.Round((price * count), 2),
                        说明= enchantMods.Count==0?"": enchantMods[0],
                    });
            }
            return result.OrderByDescending(x=>x.排序).ToList();
        }
    }
}
