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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace POE_Auxiliary_Tools
{
    public static class MarketQueryHandle
    {

        //获取物品key
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">物品名称</param>
        /// <param name="ssid">搜索id</param>
        /// <param name="priceType">通货类型</param>
        /// <returns></returns>
        public static JObject GetKeyList(string name, string ssid,string priceType)
        {
            StringBuilder sbr = new StringBuilder();
            string url = "https://poe.game.qq.com/api/trade/search/S22%E8%B5%9B%E5%AD%A3";
            Hashtable ht = new Hashtable();//将参数打包成json格式的数据
            ht.Add("name", name);

            //判断是不是罗盘
            //sbr.Clear();
            //sbr.Append("SELECT * FROM 充能罗盘属性");
            //DataTable _dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            //var _list = DataHandler.TableToListModel<充能罗盘属性>(_dt);
            //var obj = _list.SingleOrDefault(x => x.罗盘名称 == name);
            //if (obj != null)
            //{
            //    //是罗盘
            //    ht.Add("tjId", obj.搜索条件);
            //}
            //判断有没有搜索id
            if (ssid != "")
            {
                ht.Add("tjId", ssid);
            }



            string list = HttpUitls.DoPost(url, MainFrom.tokenList[0].POESESSID, ht, priceType);  //HttpRequest是自定义的一个类
            //判断请求是否错误
            if (list.Contains("错误的请求") || list.Contains("Too Many Requests") || list.Contains("未找到") || list.Contains("超时"))
            {
                if (list.Contains("Too Many Requests"))
                {
                    Thread.Sleep(10000);
                }
                //请求错误
                JObject err = new JObject();
                err = JObject.Parse("{'错误的请求':'400'}");
                return err;
            } else if (list.Contains("解析"))
            {
                JObject err = new JObject();
                err = JObject.Parse("{'错误的请求':'400'}");
                return err;
            }
            JObject jsonObject = JObject.Parse(list);
            var id = jsonObject["id"];
            var result = jsonObject["result"];

            return jsonObject;
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonObject">物品key list对象</param>
        /// <param name="name">物品名称</param>
        /// <param name="productType">物品类型</param>
        /// <param name="isheap">允许堆叠</param>
        /// <param name="tongHuo">交易通货</param>
        /// <param name="minimum">最少数量</param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static 集市物品 GetPrice(List<集市物品> resultList,JObject jsonObject, string name, string productType, string isHeap, string tongHuo, int minimum, int i)
        {
            var data = DateTime.Now;//当前时间
            var url2 = "https://poe.game.qq.com/api/trade/fetch/";
            var id = jsonObject["id"];
            var result = jsonObject["result"];
            var len = result.Count();
            int endNum;//需要满足的记录数
            if (name == "神圣石")
            {
                endNum = 20;
            }
            else
            {
                endNum = 10;
            }

            int j = i + 10;
            int k = i;
            if (j > len)
            {
                j = len;
            }
            for (k = i; k < j; k++)
            {
                url2 += result[k].ToString() + ",";
            }
            url2 = url2.Substring(0, url2.Length - 1);
            url2 += "?query=" + id.ToString();
            var list2 = HttpUitls.Get(url2, MainFrom.tokenList[0].POESESSID);

            //处理错误
            if (list2.Contains("未找到") || list2.Contains("Too Many Requests") || list2.Contains("错误") || list2.Contains("超时"))
            {
                if (list2.Contains("Too Many Requests"))
                {
                    Thread.Sleep(10000);
                }
                return new 集市物品() { err="请求错误或超时！"};
            }


            JObject jsonObject2 = JObject.Parse(list2);
            foreach (var item in jsonObject2["result"])
            {
                try
                {
                    var m = item["item"]["note"];
                    var TransactionType = item["item"]["note"].ToString().Split(' ')[0];
                    var thType = item["item"]["note"].ToString().Split(' ')[2];//通货类型
                    var thTypeName = "";
                    var 上架时间 = Convert.ToDateTime(item["listing"]["indexed"].ToString());
                    switch (thType)
                    {
                        //混沌石
                        case "chaos":
                            thTypeName = "混沌石";
                            break;
                        //神圣石
                        case "divine":
                            thTypeName = "神圣石";
                            break;
                        default:
                            break;
                    }
                    if (TransactionType == "=a/b/o" && thTypeName == tongHuo)
                    {
                        var model = new 集市物品();
                        model.名称 = item["item"]["typeLine"].ToString();
                        model.交易通货 = item["listing"]["price"]["currency"].ToString();
                        model.总价格 = Convert.ToInt32(item["listing"]["price"]["amount"]);
                        if (isHeap == "是")
                        {
                            try
                            {
                                //允许堆叠
                                var t = item["item"]["properties"][0]["values"][0][0].ToString().Replace(" ", "");
                                var sl = t.Split('/');
                                堆叠数量 count = new 堆叠数量();
                                count.数量 = Convert.ToInt32(sl[0]);
                                count.堆叠上限 = Convert.ToInt32(sl[1]);
                                model.数量 = count;
                                model.单价 = Math.Round((double)model.总价格 / (double)count.数量, 2);

                                //记录查询缓存
                                var cache = new 查询缓存() { 上架时间 = 上架时间.ToString(), 查询时间 = DateTime.Now.ToString(), 价格 = model.单价.ToString(), 物品名称 = name, 物品类型 = productType, 通货类型 = tongHuo, 数量 = count.数量 };
                                SaveCacheRecord(cache);
                                if ((data - 上架时间).TotalMinutes > 30 && (data - 上架时间).TotalMinutes < 2880)
                                {
                                    if (count.数量 >= minimum)
                                    {
                                        resultList.Add(model);
                                    }
                                }
                            }
                            catch (NullReferenceException ex)
                            {
                                return new 集市物品() { err = "检查物品是否允许堆叠！" };
                            }
                        }
                        else
                        {
                            model.单价 = (double)model.总价格 / 1;
                            //记录查询缓存
                            var cache = new 查询缓存() { 上架时间 = 上架时间.ToString(), 查询时间 = DateTime.Now.ToString(), 价格 = model.单价.ToString(), 物品名称 = name, 物品类型 = productType, 通货类型 = tongHuo, 数量 = 1 };
                            SaveCacheRecord(cache);
                            if ((data - 上架时间).TotalMinutes > 30)
                            {

                                resultList.Add(model);
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
               
            }
            var c = jsonObject["result"].Count();
            if (resultList.Count < endNum && jsonObject["result"].Count() > k)
            {
                return GetPrice(resultList,jsonObject, name, productType, isHeap, tongHuo, minimum, k);
            }
            if (resultList.Count > 0)
            {
                //计算平均数
                var prices = resultList.Select(item => item.单价).ToList().OrderBy(x=>x).ToList();
                var medianPrice = prices[prices.Count / 2];
                var averagePrice =Math.Round(prices.Where(x => x <= medianPrice).Average(),2);
                
                //返回结果
                集市物品 obj = new 集市物品() { 名称 = name, 单价 = averagePrice };

                //记录查询结果
                SaveQueryRecord(name, averagePrice.ToString(), tongHuo, productType);
                return obj;
            }
            else
            {
                集市物品 obj = new 集市物品() { 名称 = name, 单价 = -1 };
                return obj;
            }


        }
        public static void SaveQueryRecord(string name, string price, string type, string productType)
        {
            StringBuilder sbr = new StringBuilder();
            sbr.Clear();
            sbr.Append($@"INSERT INTO 查询记录 (查询时间,物品名称,价格,通货类型,物品类型) VALUES ");
            sbr.Append($"('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{name}','{price}','{type}','{productType}');");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);

        }
        public static void SaveCacheRecord(查询缓存 model)
        {
            StringBuilder sbr = new StringBuilder();
            sbr.Clear();
            sbr.Append($@"INSERT INTO 查询缓存 (查询时间,物品名称,价格,通货类型,物品类型,上架时间,数量) VALUES ");
            sbr.Append($"('{Convert.ToDateTime(model.查询时间).ToString("yyyy-MM-dd HH:mm:ss")}','{model.物品名称}','{model.价格}','{model.通货类型}','{model.物品类型}','{Convert.ToDateTime(model.上架时间).ToString("yyyy-MM-dd HH:mm:ss")}','{model.数量}');");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
        }
        public static void DeleteRecord(string name)
        {
            StringBuilder sbr = new StringBuilder();
            sbr.Clear();
            sbr.Append($@"DELETE FROM 查询缓存 WHERE 物品名称='{name}' ");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
        }
    }
}
