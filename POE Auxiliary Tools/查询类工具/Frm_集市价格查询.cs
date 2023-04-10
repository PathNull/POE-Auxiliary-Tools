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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Core.Popup;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_集市价格查询 : BaseForm
    {
        public double scale; //dc比例
        public DateTime 查询时间;
        List<集市物品> resultList = new List<集市物品>();
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        Thread thread;

      
        public Frm_集市价格查询()
        {
            InitializeComponent();
            Init();
            //隐藏进度条
            progressBarControl1.Visible = false;
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
            sbr.Append("SELECT * FROM 物品类别 order by 类别名称");
            var cmdText = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<物品类别>(dt);
            return list;
        }

        private  void SelectProductType(object sender, System.EventArgs e)
        {
            var isQuery = false;
            sbr.Clear();
            sbr.Append($"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id ");
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
                var cmdText = sbr.ToString().Substring(0, sbr.Length - 2)+ ") order by 类别名称,物品名称";
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
            //隐藏和禁用
            progressBarControl1.Visible = true;
            simpleButton_query.Enabled = false;
            simpleButton_dcb.Enabled = false;
       

            //检查是否输入了POESESSID
           
            if (MainFrom.tokenList.Count == 0)
            {
                //弹出窗体提示输入Token
                POESESSID输入 to = new POESESSID输入();
                // 计算窗体在屏幕上的中央位置
                to.StartPosition = FormStartPosition.CenterScreen;
                to.ShowDialog();
                simpleButton_query.Enabled = true;
                simpleButton_dcb.Enabled = true;
                return;
            }
            //检查是否有查询物品
            if (gridView_cxlb.RowCount == 0)
            {
                dialogResult = Popup.Tips(this, "请先选择需要查询的物品!", "提示信息", PopUpType.Info);
                simpleButton_query.Enabled = true;
                simpleButton_dcb.Enabled = true;
                return;
            }
           
            GetScale();
           


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


                var index = 1;
                foreach (var item in list)
                {
                    var keyResult = MarketQueryHandle.GetKeyList(item.物品名称,item.通货类型=="混沌石"?"chaos": "divine");
                    if (keyResult["错误的请求"] != null)
                    {
                        //请求错误
                        if (gridControl1.InvokeRequired)
                        {
                            var a = gridView1.GetRowHandle(gridView1.DataRowCount);
                            Action SetSource = delegate {
                                // 创建新行
                                gridView1.AddNewRow();
                                // 获取当前新行所在行号
                                int newRowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                                foreach (var info in  err.GetType().GetProperties())
                                {
                                    // 设置新行数据
                                    var val = ObjectHandler.GetPropertyValue(err, info.Name);
                                    gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), val);
                                }
                                // 更新视图
                                gridView1.RefreshData();
                            };
                            gridControl1.Invoke(SetSource);

                        }
                        else
                        {
                            // 创建新行
                            gridView1.AddNewRow();
                            // 获取当前新行所在行号
                            int newRowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                            var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                            foreach (var info in err.GetType().GetProperties())
                            {
                                // 设置新行数据
                                var val = ObjectHandler.GetPropertyValue(err, info.Name);
                                gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), val);
                            }
                            // 更新视图
                            gridView1.RefreshData();
                        }
                        //进度条
                        double _percent = 0;
                        if (progressBarControl1.InvokeRequired)
                        {
                            Action SetSource = delegate {
                                _percent = (double)index / (double)list.Count * 100;
                                progressBarControl1.Position = (int)_percent;
                                progressBarControl1.BackColor = Color.Blue;
                                progressBarControl1.PerformStep();
                            };
                            progressBarControl1.Invoke(SetSource);
                        }
                        else
                        {
                            _percent = (double)index / (double)list.Count * 100;
                            progressBarControl1.Position = (int)_percent;
                            progressBarControl1.BackColor = Color.Blue;
                            progressBarControl1.PerformStep();
                        }
                        continue;
                    }


                    resultList = new List<集市物品>();
                    var model = MarketQueryHandle.GetPrice(resultList,keyResult, item.物品名称,item.类别名称, item.允许堆叠,item.通货类型,(int)item.最低数量,0);
                    //请求错误
                    if (model == null)
                    {
                        if (gridControl1.InvokeRequired)
                        {
                            var a = gridView1.GetRowHandle(gridView1.DataRowCount);
                            Action SetSource = delegate {
                                // 创建新行
                                gridView1.AddNewRow();
                                // 获取当前新行所在行号
                                int newRowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                                foreach (var info in err.GetType().GetProperties())
                                {
                                    // 设置新行数据
                                    var val = ObjectHandler.GetPropertyValue(err, info.Name);
                                    gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), val);
                                }
                                // 更新视图
                                gridView1.RefreshData();
                            };
                            gridControl1.Invoke(SetSource);

                        }
                        else
                        {
                            // 创建新行
                            gridView1.AddNewRow();
                            // 获取当前新行所在行号
                            int newRowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                            var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                            foreach (var info in err.GetType().GetProperties())
                            {
                                // 设置新行数据
                                var val = ObjectHandler.GetPropertyValue(err, info.Name);
                                gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), val);
                            }
                            // 更新视图
                            gridView1.RefreshData();
                        }
                        //进度条
                        double _percent = 0;
                        if (progressBarControl1.InvokeRequired)
                        {
                            Action SetSource = delegate {
                                _percent = (double)index / (double)list.Count * 100;
                                progressBarControl1.Position = (int)_percent;
                                progressBarControl1.BackColor = Color.Blue;
                                progressBarControl1.PerformStep();
                            };
                            progressBarControl1.Invoke(SetSource);
                        }
                        else
                        {
                            _percent = (double)index / (double)list.Count * 100;
                            progressBarControl1.Position = (int)_percent;
                            progressBarControl1.BackColor = Color.Blue;
                            progressBarControl1.PerformStep();
                        }
                        continue;
                    }
                    
                    var _mo = new 集市查询结果() { 名称 = model.名称, 价格 = model.单价.ToString() ,通货类型= item .通货类型};
                    DevGridControlHandler.AddRecord(dt, _mo);//添加记录到DataTable
                    //gridView添加查询结果
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
                                var val = ObjectHandler.GetPropertyValue(_mo, info.Name).ToString();
                                if (info.Name== "价格" && val=="-1")
                                {
                                    gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), "满足计算条件的物品不足");
                                }
                                else
                                {
                                    gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), val);
                                }
                            }
                            // 更新视图
                            gridView1.RefreshData();
                        };
                        gridControl1.Invoke(SetSource);

                    }
                    else
                    {
                        // 创建新行
                        gridView1.AddNewRow();
                        // 获取当前新行所在行号
                        int newRowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        foreach (var info in _mo.GetType().GetProperties())
                        {
                            // 设置新行数据
                            var val = ObjectHandler.GetPropertyValue(_mo, info.Name).ToString();
                            if (info.Name == "价格" && val == "-1")
                            {
                                gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), "满足计算条件的物品不足");
                            }
                            else
                            {
                                gridView1.SetRowCellValue(newRowHandle, info.Name.ToString(), val);
                            }
                        }
                        // 更新视图
                        gridView1.RefreshData();
                    }
                    //进度条
                    double percent=0;
                    if (progressBarControl1.InvokeRequired)
                    {
                        Action SetSource = delegate {
                            percent = (double)index / (double)list.Count * 100;
                            progressBarControl1.Position = (int)percent;
                            progressBarControl1.BackColor = Color.Blue;
                            progressBarControl1.PerformStep();
                        };
                        progressBarControl1.Invoke(SetSource);
                    }
                    else
                    {
                        percent = (double)index / (double)list.Count * 100;
                        progressBarControl1.Position = (int)percent;
                        progressBarControl1.BackColor = Color.Blue;
                        progressBarControl1.PerformStep();
                    }
                    index++;
                    //暂停，防止查询过快
                    var sleep = rm.Next(3000, 5000);
                    Thread.Sleep(sleep);
                }
                //进度条隐藏
                if (progressBarControl1.InvokeRequired)
                {
                    Action SetSource = delegate {
                        progressBarControl1.Visible = false;
                    };
                    progressBarControl1.Invoke(SetSource);
                }
                else
                {
                    progressBarControl1.Visible = false;
                }

                //查询按钮可用
                if (simpleButton_query.InvokeRequired)
                {
                    Action SetSource = delegate { simpleButton_query.Enabled = true; };
                    simpleButton_query.Invoke(SetSource);

                }
                else
                {
                    simpleButton_query.Enabled = true;
                }
                if (simpleButton_dcb.InvokeRequired)
                {
                    Action SetSource = delegate { simpleButton_dcb.Enabled = true; };
                    simpleButton_dcb.Invoke(SetSource);
                }
                else
                {
                    simpleButton_dcb.Enabled = true;
                }
            }
          
        }
        //获取物品key
        public JObject GetKeyList(string name,string priceType)
        {
            string url = "https://poe.game.qq.com/api/trade/search/S21%E8%B5%9B%E5%AD%A3";
            Hashtable ht = new Hashtable();//将参数打包成json格式的数据
            ht.Add("name", name);
          
            //判断是不是罗盘
            sbr.Clear();
            sbr.Append("SELECT * FROM 充能罗盘属性");
            DataTable _dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            var _list  = DataHandler.TableToListModel<充能罗盘属性>(_dt);
            var obj = _list.SingleOrDefault(x => x.罗盘名称 == name);
            if (obj != null){
                //是罗盘
                ht.Add("tjId", obj.搜索条件);
            }
            string list = HttpUitls.DoPost(url, MainFrom.tokenList[0].POESESSID ,ht, priceType);  //HttpRequest是自定义的一个类
            //判断请求是否错误
            if (list.Contains("错误的请求") || list.Contains("Too Many Requests") || list.Contains("未找到") || list.Contains("超时"))
            {
                if(list.Contains("Too Many Requests"))
                {
                    Thread.Sleep(10000);
                }
                //请求错误
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
        public 集市物品 GetPrice(JObject jsonObject,string name, string productType,string isHeap, string tongHuo,int minimum, int i)
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
            var list2 = HttpUitls.Get(url2, MainFrom.tokenList[0].POESESSID);

            //处理错误
            if (list2.Contains("未找到")||list2.Contains("Too Many Requests") || list2.Contains("错误") || list2.Contains("超时"))
            {
                if (list2.Contains("Too Many Requests"))
                {
                    Thread.Sleep(10000);
                }
                return null;
            }


            JObject jsonObject2 = JObject.Parse(list2);
            foreach (var item in jsonObject2["result"])
            {
                var TransactionType = item["item"]["note"].ToString().Split(' ')[0];
                var thType = item["item"]["note"].ToString().Split(' ')[2];//通货类型
                var thTypeName = "";
                var 上架时间 =Convert.ToDateTime(item["listing"]["indexed"].ToString());
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
                if (TransactionType == "=a/b/o" && thTypeName== tongHuo && (data- 上架时间).TotalHours<24 && (data - 上架时间).TotalHours > 1)
                {
                    var model = new 集市物品();
                    model.名称 = item["item"]["typeLine"].ToString();
                    model.交易通货 = item["listing"]["price"]["currency"].ToString();
                    model.总价格 = Convert.ToInt32(item["listing"]["price"]["amount"]);
                    if (isHeap=="是")
                    {
                        //允许堆叠
                        var t = item["item"]["properties"][0]["values"][0][0].ToString().Replace(" ", "");
                        var sl = t.Split('/');
                        堆叠数量 count = new 堆叠数量();
                        count.数量 = Convert.ToInt32(sl[0]);
                        count.堆叠上限 = Convert.ToInt32(sl[1]);
                        model.数量 = count;
                        model.单价 = (double)model.总价格 / (double)count.数量;

                        if (count.数量 >= minimum)
                        {
                            resultList.Add(model);
                        }
                    }
                    else
                    {
                        model.单价 = (double)model.总价格 /1;
                        resultList.Add(model);
                    }
                   
                }
            }
            var c = jsonObject["result"].Count();
            if (resultList.Count < endNum && jsonObject["result"].Count()> k)
            {
               return GetPrice(jsonObject,name, productType, isHeap, tongHuo, minimum, k);
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

                //记录查询结果
                SaveQueryRecord(name, price.ToString(), tongHuo, productType);
                return obj;
            }
            else
            {
                集市物品 obj = new 集市物品() { 名称 = name, 单价 = -1 };
                return obj;
            }
         

        }
        /// <summary>
        /// 保存查询记录
        /// </summary>
        /// <param name="name">物品名称</param>
        /// <param name="price">价格</param>
        /// <param name="type">通货类型</param>
        public void SaveQueryRecord(string name,string price,string type,string productType)
        {
            sbr.Clear();
            sbr.Append($@"INSERT INTO 查询记录 (查询时间,物品名称,价格,通货类型,物品类型) VALUES ");
            sbr.Append($"('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{name}','{price}','{type}','{productType}');");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
        }
        //获取DC比例
        public void GetScale()
        {
            var keyResult = GetKeyList("神圣石","chaos");
            var model = GetPrice(keyResult, "神圣石","通货", "是","混沌石", 1, 0);
            if (model != null)
            {
                if (label1.InvokeRequired)
                {
                    label1.Invoke(new Action(() => {
                        label1.Text = $"1 神圣 = {model.单价} 混沌石";
                    }));
                }
                else
                {
                    label1.Text = $"1 神圣 = {model.单价} 混沌石";
                }
            }
            else
            {
                if (label1.InvokeRequired)
                {
                    label1.Invoke(new Action(() => {
                        label1.Text = $"DC比更新失败";
                    }));
                }
                else
                {
                    label1.Text = $"DC比更新失败";
                }
            }
        }
        private void Frm_集市价格查询_Load(object sender, EventArgs e)
        {
            //隐藏进度条
            progressBarControl1.Visible = false;

            sbr.Clear();
            sbr.Append(@"SELECT   *  FROM  查询记录 
                        WHERE 物品名称='神圣石' order by 查询时间 desc  limit 0,1");
            var dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            var list = DataHandler.TableToListModel<查询记录>(dt);
            var bl = Convert.ToDouble(list[0].价格);  //dc比例
            label1.Text = $"1 神圣 = {bl} 混沌石";
        }
        //点击更新DC比
        private void simpleButton_dcb_Click(object sender, EventArgs e)
        {
            //检查是否输入了POESESSID
            sbr.Clear();
            sbr.Append("SELECT POESESSID FROM 用户属性 ");
            DataTable _dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            MainFrom.tokenList = DataHandler.TableToListModel<用户Token>(_dt);
            if (MainFrom.tokenList.Count == 0)
            {
                //弹出窗体提示输入Token
                POESESSID输入 to = new POESESSID输入();
                // 计算窗体在屏幕上的中央位置
                to.StartPosition = FormStartPosition.CenterScreen;
                to.ShowDialog();
                return;
            }

            simpleButton_dcb.Enabled = false;
            simpleButton_query.Enabled = false;
            Thread thead = new Thread(() => {
                GetScale();
                if (simpleButton_dcb.InvokeRequired)
                {
                    simpleButton_dcb.Invoke(new Action(() => {
                        simpleButton_dcb.Enabled = true;
                    }));
                }else
                {
                    simpleButton_dcb.Enabled = true;
                }
                if (simpleButton_query.InvokeRequired)
                {
                    simpleButton_query.Invoke(new Action(() => {
                        simpleButton_query.Enabled = true;
                    }));
                }else
                {
                    simpleButton_query.Enabled = true;
                }

            });
            thead.Start();
        }
        //设置行颜色
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                string price = view.GetRowCellDisplayText(e.RowHandle, view.Columns["价格"]);
                if (price.Contains("错误")|| price.Contains("不足"))
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }
    }
    public class 集市查询结果
    {
        public string 名称 { get; set; }

        public string 价格 { get; set; }

        public string 通货类型 { get; set; }
    }

    public class 充能罗盘属性
    {
        public string 罗盘名称 { get; set; }

        public string 搜索条件 { get; set; }
    }
}
