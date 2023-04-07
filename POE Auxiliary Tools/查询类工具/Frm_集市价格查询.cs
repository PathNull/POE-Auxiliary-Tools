using Core;
using Core.Common;
using Core.DevControlHandler;
using Core.Web;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_集市价格查询 : BaseForm
    {
        public double scale;
        List<集市物品> resultList = new List<集市物品>();
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        Thread thread;
      
        public Frm_集市价格查询()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            var list = GetProductType();
            foreach (var item in list)
            {
                var t = new CheckEdit() { Name = "editorName", Text = item.类别名称,AutoSize=true};
                t.CheckedChanged += SelectProductType;
                flowLayoutPanel2.Controls.Add(t);
                t.Show();
            }

        }

        /// <summary>
        /// 获取所有物品类别
        /// </summary>
        public List<物品类别> GetProductType()
        {
            sbr.Clear();
            sbr.Append("SELECT * FROM 物品类别 ");
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<物品类别>(dt);
            return list;
        }

        private  void SelectProductType(object sender, System.EventArgs e)
        {
            var isQuery = false;
            sbr.Clear();
            sbr.Append($"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id");
            var index = 0;
            foreach (Control item in flowLayoutPanel2.Controls)
            {
                if (item != null)
                {
                    if(item is CheckEdit)
                    {
                       
                        var obj = (CheckEdit)item;
                        if (obj.Checked)
                        {
                            if (index == 0)
                            {
                                sbr.Append($" where  是否可用='是' and (");
                                isQuery = true;
                            }
                            var name = item.Text;
                            sbr.Append($" 类别名称='{name}' or");
                            index++;
                        }
                        
                    }
                 
                }
              
            }
            if (isQuery)
            {
                var cmdText = sbr.ToString().Substring(0, sbr.Length - 2)+") order by 类别名称";
                DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
                var list = DataHandler.TableToListModel<物品>(dt);
                gridControl_cxlb.DataSource = list;
            }
            else
            {
                gridControl_cxlb.DataSource = null; 
            }
          
        }

        private void comboBoxEdit1_Click(object sender, System.EventArgs e)
        {

        }
        //开始查询
        private void simpleButton_query_Click(object sender, System.EventArgs e)
        {
            //StartQuery();
            var dt = new DataTable();
            //添加表头
            foreach (var item in new 集市查询结果().GetType().GetProperties())
            {
                dt.Columns.Add(item.Name);
            }
            gridControl1.DataSource = dt;
            thread = new Thread(new ThreadStart(StartQuery));
            thread.Start();
        }
       
        public  void StartQuery()
        {
            if (simpleButton_query.InvokeRequired)
            {
                Action SetSource = delegate { simpleButton_query.Enabled = false; };
                simpleButton_query.Invoke(SetSource);
               
            }
            simpleButton_query.Enabled = false;
            //需要查询的物品
            var list = gridControl_cxlb.DataSource as List<物品>;
            if (list != null){
                var dt = new DataTable();
                //添加表头
                foreach (var item in new 集市查询结果().GetType().GetProperties())
                {
                    dt.Columns.Add(item.Name);
                }
               
                Random rm = new Random();
                foreach (var item in list)
                {
                    var keyResult = GetKeyList(item.物品名称,item.通货类型=="混沌石"?"chaos": "divine");
                    resultList = new List<集市物品>();
                    var model = GetPrice(keyResult,item.物品名称,item.通货类型,(int)item.最低数量,0);
                    var _mo = new 集市查询结果() { 名称 = model.名称, 价格 = model.单价.ToString() ,通货类型= item .通货类型};
                    DevGridControlHandler.AddRecord(dt, _mo);//添加记录到DataTable

                    if (gridControl1.InvokeRequired)
                    {
                        var a = gridView1.GetRowHandle(gridView1.DataRowCount); 
                        Action SetSource = delegate {
                            // 创建新行
                            gridView1.AddNewRow();
                            // 获取当前新行所在行号
                            int newRowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                            foreach (var info in _mo.GetType().GetProperties())
                            {
                                // 设置新行数据
                                var val = ObjectHandler.GetPropertyValue(_mo, info.Name);
                                gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), val);
                            }
                            // 更新视图
                            gridView1.RefreshData();
                        };
                        gridControl1.Invoke(SetSource);
                        
                    }
                    //暂停，防止查询过快
                    var sleep = rm.Next(3000, 5000);
                    Thread.Sleep(sleep);
                }
                if (simpleButton_query.InvokeRequired)
                {
                    Action SetSource = delegate { simpleButton_query.Enabled = true; };
                    simpleButton_query.Invoke(SetSource);

                }
            }
          
        }
        //获取物品key
        public JObject GetKeyList(string name,string priceType)
        {
            string url = "https://poe.game.qq.com/api/trade/search/S21%E8%B5%9B%E5%AD%A3";
            Hashtable ht = new Hashtable();//将参数打包成json格式的数据
            ht.Add("name", name);
            string list = HttpUitls.DoPost(url, ht, priceType);  //HttpRequest是自定义的一个类
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
        /// <param name="tongHuo">交易通货</param>
        /// <param name="minimum">最少数量</param>
        /// <param name="i"></param>
        /// <returns></returns>
        public 集市物品 GetPrice(JObject jsonObject,string name, string tongHuo,int minimum, int i)
        {
            var url2 = "https://poe.game.qq.com/api/trade/fetch/";
            var id = jsonObject["id"];
            var result = jsonObject["result"];
            var len = result.Count();
          
            int j = i + 10;
            int k = i;
            if (j>len)
            {
                j = len;
            }
            for (k = i; k < j; k++)
            {
                url2 += result[k].ToString() + ",";
            }
            url2 = url2.Substring(0, url2.Length - 1);
            url2 += "?query=" + id.ToString();
            var list2 = HttpUitls.Get(url2);
            JObject jsonObject2 = JObject.Parse(list2);
            foreach (var item in jsonObject2["result"])
            {
                var TransactionType = item["item"]["note"].ToString().Split(' ')[0];
                var thType = item["item"]["note"].ToString().Split(' ')[2];//通货类型
                var thTypeName = "";
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
                if (TransactionType == "=a/b/o" && thTypeName== tongHuo)
                {
                    var model = new 集市物品();
                    model.名称 = item["item"]["typeLine"].ToString();
                    model.交易通货 = item["listing"]["price"]["currency"].ToString();
                    model.总价格 = Convert.ToInt32(item["listing"]["price"]["amount"]);
                    var t = item["item"]["properties"][0]["values"][0][0].ToString().Replace(" ", "");
                    var sl = t.Split('/');
                    堆叠数量 count = new 堆叠数量();
                    count.数量 = Convert.ToInt32(sl[0]);
                    count.堆叠上限 = Convert.ToInt32(sl[1]);
                    model.数量 = count;
                    model.单价 = (double)model.总价格 / (double)count.数量;

                    if(count.数量>=minimum)
                    {
                        resultList.Add(model);
                    }
                }
            }
            var c = jsonObject["result"].Count();
            if (resultList.Count < 10 && jsonObject["result"].Count()> k)
            {
               return GetPrice(jsonObject,name, tongHuo, minimum, k);
            }
            if (resultList .Count> 0)
            {
                //计算平均价格
                double total = 0;
                double price = 0;
                foreach (var item in resultList)
                {
                    total += item.单价;
                }
                price = Math.Round(total / (double)resultList.Count, 2);
                //返回结果
                集市物品 obj = new 集市物品() { 名称 = name, 单价 = price};
                return obj;
            }
            else
            {
                集市物品 obj = new 集市物品() { 名称 = name, 单价 = -1 };
                return obj;
            }
         

        }
        //获取DC比例
        public double GetScale()
        {
            var keyResult = GetKeyList("神圣石","chaos");
            var model = GetPrice(keyResult, "神圣石", "混沌石", 1, 0);
            label1.Text = $"1 神圣 = {model.单价} 混沌石";
            return model.单价;
        }
        private void Frm_集市价格查询_Load(object sender, EventArgs e)
        {
            scale = GetScale();
        }
    }
    public class 集市查询结果
    {
        public string 名称 { get; set; }

        public string 价格 { get; set; }

        public string 通货类型 { get; set; }
    }
}
