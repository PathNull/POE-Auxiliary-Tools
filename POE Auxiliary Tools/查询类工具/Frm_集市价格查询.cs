using Core;
using Core.Common;
using Core.DevControlHandler;
using Core.Web;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Newtonsoft.Json.Linq;
using POE_Auxiliary_Tools.Enum;
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
using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;
using static Core.Popup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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
        CancellationTokenSource cts = new CancellationTokenSource();
        bool defCheckedAll = true;

        public Frm_集市价格查询()
        {
            InitializeComponent();
           
        }
        public void TriggerLoadEvent()
        {
            this.OnLoad(EventArgs.Empty);
        }
        public void Init()
        {
            var list = GetProductType();


            foreach (var item in list)
            {
                var t = new CheckEdit() { Name = "editorName", Text = item.类别名称 };
                t.CheckedChanged += SelectProductType;
                flowLayoutPanel1.Controls.Add(t);
                t.Show();
            }
            // 重新设置所有 CheckEdit 控件的位置和大小，以确保它们对齐
            int maxLeft = 0;
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is CheckEdit checkEdit)
                {
                    maxLeft = Math.Max(maxLeft, checkEdit.Left); // 计算目前最靠左的控件的 Left 值
                }
            }
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is CheckEdit checkEdit)
                {
                    checkEdit.Left = maxLeft; // 将控件的 Left 值设置为最靠左的控件的 Left 值
                    checkEdit.Width = flowLayoutPanel1.ClientSize.Width - checkEdit.Left - flowLayoutPanel1.Padding.Right; // 将控件的宽度设置为 FlowLayoutPanel 的客户区域宽度减去 Left 值和右侧填充的宽度
                }
            }


        }
        /// <summary>
        /// 全选
        /// </summary>
        private void CheckedAll()
        {
            foreach (object item in flowLayoutPanel1.Controls)
            {
                if (item is CheckEdit ce)
                {
                    ce.Checked = true;
                }
            }
        }
        /// <summary>
        /// 反选选
        /// </summary>
        private void UnCheckedAll()
        {
            foreach (object item in flowLayoutPanel1.Controls)
            {
                if (item is CheckEdit ce)
                {
                    ce.Checked = false;
                }
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

        private void SelectProductType(object sender, System.EventArgs e)
        {
            var isQuery = false;
            sbr.Clear();
            sbr.Append($"SELECT * FROM 物品  LEFT JOIN 物品类别 ON 物品.物品类别id=物品类别.id ");
            var index = 0;
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if (item != null)
                {
                    if (item is CheckEdit)
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
                var cmdText = sbr.ToString().Substring(0, sbr.Length - 2) + ") order by 类别名称,物品名称";
                DataTable dt = MainFrom.database.ExecuteDataTable(cmdText);
                var list = DataHandler.TableToListModel<物品>(dt);
                gridControl_cxlb.DataSource = list;
            }
            else
            {
                gridControl_cxlb.DataSource = null;
            }

        }


        //开始查询
        private void simpleButton_query_Click(object sender, System.EventArgs e)
        {
            //隐藏和禁用
            progressBarControl1.Visible = true;
            simpleButton_query.Enabled = false;
            simpleButton_dcb.Enabled = false;
            simpleButton_zz.Enabled = true;

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

            if (GetScale() == 返回状态.失败)
            {
                progressBarControl1.Visible = false;
                simpleButton_query.Enabled = true;
                simpleButton_dcb.Enabled = true;
                simpleButton_zz.Enabled = false;
                return ;
            }



            var dt = new DataTable();
            //添加表头
            foreach (var item in new 集市查询结果().GetType().GetProperties())
            {
                dt.Columns.Add(item.Name);
            }
            gridControl1.DataSource = dt;
            thread = new Thread(() => DoWork(cts.Token));
            thread.Start();
        }

        public void StartQuery(CancellationToken cancellationToken)
        {
            //需要查询的物品
            var list = gridControl_cxlb.DataSource as List<物品>;
            var sleepList = new List<暂停>();
            //给每个物品预先分配sleep时间，方便计算完成时间
            Random rm = new Random();
            foreach (var item in list)
            {

                sleepList.Add(new 暂停() { SleepTime= rm.Next(4000, 5000),FloatTime=2000 });
            }
          
            if (list != null)
            {
                var index = 1;

                //倒计时
                CountDown(sleepList, index);

                  var dt = new DataTable();
                //添加表头
                foreach (var item in new 集市查询结果().GetType().GetProperties())
                {
                    dt.Columns.Add(item.Name);
                }



           
                foreach (var item in list)
                {
                    var keyResult = MarketQueryHandle.GetKeyList(item.物品名称,item.搜索id, item.通货类型 == "混沌石" ? "chaos" : "divine");
                    if (keyResult["错误的请求"] != null)
                    {
                        //请求错误
                        if (gridControl1.InvokeRequired)
                        {
                            var a = gridView1.GetRowHandle(gridView1.DataRowCount);
                            Action SetSource = delegate
                            {
                                var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                                CreateNewRow(err);
                            };
                            gridControl1.Invoke(SetSource);

                        }
                        else
                        {
                            var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                            CreateNewRow(err);
                        }
                        //进度条
                        if (progressBarControl1.InvokeRequired)
                        {
                            Action SetSource = delegate
                            {
                                SetProgress(index, list);
                            };
                            progressBarControl1.Invoke(SetSource);
                        }
                        else
                        {
                            SetProgress(index, list);
                        }
                        continue;
                    }


                    resultList = new List<集市物品>();
                    MarketQueryHandle.DeleteRecord(item.物品名称);
                    var model = MarketQueryHandle.GetPrice(resultList, keyResult, item.物品名称, item.类别名称, item.允许堆叠, item.通货类型, (int)item.最低数量, 0);
                    //请求错误
                    if (model.err != null)
                    {
                        if (gridControl1.InvokeRequired)
                        {
                            Action SetSource = delegate
                            {
                                var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                                CreateNewRow(err);
                            };
                            gridControl1.Invoke(SetSource);

                        }
                        else
                        {
                            var err = new 集市查询结果() { 名称 = item.物品名称, 价格 = "物品名称错误或请求超时", 通货类型 = item.通货类型 };
                            CreateNewRow(err);
                        }
                        //进度条
                        if (progressBarControl1.InvokeRequired)
                        {
                            Action SetSource = delegate
                            {
                                SetProgress(index, list);
                            };
                            progressBarControl1.Invoke(SetSource);
                        }
                        else
                        {
                            SetProgress(index, list);
                        }
                        continue;
                    }

                    var _mo = new 集市查询结果() { 名称 = model.名称, 价格 = model.单价.ToString(), 通货类型 = item.通货类型 };
                    DevGridControlHandler.AddRecord(dt, _mo);//添加记录到DataTable
                                                             //暂停，防止查询过快
                    var sleep = sleepList[index - 1];
                    Thread.Sleep(sleep.SleepTime);
                    //gridView添加查询结果
                    if (gridControl1.InvokeRequired)
                    {
                        var a = gridView1.GetRowHandle(gridView1.DataRowCount);
                        Action SetSource = delegate
                        {
                            CreateNewRow(_mo);
                        };
                        gridControl1.Invoke(SetSource);

                    }
                    else
                    {
                        CreateNewRow(_mo);
                    }
                    //进度条
                    if (progressBarControl1.InvokeRequired)
                    {
                        Action SetSource = delegate
                        {
                            SetProgress(index, list);
                        };
                        progressBarControl1.Invoke(SetSource);
                    }
                    else
                    {
                        SetProgress(index, list);

                    }
                   
                 
                    CountDown(sleepList, index + 1);
                    index++;
                    //检查线程是否终止
                    while (cancellationToken.IsCancellationRequested)
                    {
                        // 执行清理工作
                       
                        if (simpleButton_query.InvokeRequired)
                        {
                            Action SetSource = delegate { simpleButton_query.Enabled = true; };
                            simpleButton_query.Invoke(SetSource);
                        }
                        else
                        {
                            simpleButton_zz.Enabled = false;
                        }
                      
                        if (simpleButton_dcb.InvokeRequired)
                        {
                            Action SetSource = delegate { simpleButton_dcb.Enabled = true; };
                            simpleButton_dcb.Invoke(SetSource);
                        }
                        else
                        {
                            simpleButton_zz.Enabled = false;
                        }
                        if (simpleButton_zz.InvokeRequired)
                        {
                            Action SetSource = delegate { simpleButton_zz.Enabled = false; };
                            simpleButton_zz.Invoke(SetSource);
                        }
                        else
                        {
                            simpleButton_zz.Enabled = false;
                        }
                    }
                }
                //进度条隐藏
                if (progressBarControl1.InvokeRequired)
                {
                    Action SetSource = delegate
                    {
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
                //更新一下查询历史数据
                foreach (var item in MainFrom.formList)
                {
                    if (item.Name == "Frm_查询历史")
                    {
                        var from = item as Frm_查询历史;
                        if(from.InvokeRequired){
                            Action SetSource = delegate { from.ShowData(); };
                            simpleButton_dcb.Invoke(SetSource);
                        }
                        else
                        {
                            from.ShowData();
                        }
                        
                    }
                }
             

            }

        }
        private void CountDown(List<暂停> sleepList,int index)
        {
            //倒计时
            var totalTime = sleepList.Skip(index - 1).Sum(x => x.SleepTime+x.FloatTime);//消耗时间
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(totalTime);
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);

            if (timer.InvokeRequired)
            {
                Action action = delegate { timer.Text = formattedTime; };
                timer.Invoke(action);
            }
            else
            {
                timer.Text = formattedTime;
            }
        }
        private void SetProgress(int index ,List<物品> list)
        {
            var percent = (double)index / (double)list.Count * 100;
            progressBarControl1.Position = (int)percent;
            progressBarControl1.BackColor = Color.Blue;
            progressBarControl1.PerformStep();
        }
        private void CreateNewRow(集市查询结果 model)
        {
            // 创建新行
            gridView1.AddNewRow();
            // 获取当前新行所在行号
            int newRowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
            foreach (var info in model.GetType().GetProperties())
            {
                // 设置新行数据
                var val = ObjectHandler.GetPropertyValue(model, info.Name).ToString();
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
        /// <summary>
        /// 保存查询记录
        /// </summary>
        /// <param name="name">物品名称</param>
        /// <param name="price">价格</param>
        /// <param name="type">通货类型</param>
        public void SaveQueryRecord(string name, string price, string type, string productType)
        {
            sbr.Clear();
            sbr.Append($@"INSERT INTO 查询记录 (查询时间,物品名称,价格,通货类型,物品类型) VALUES ");
            sbr.Append($"('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{name}','{price}','{type}','{productType}');");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
        }
        //获取DC比例
        public 返回状态 GetScale()
        {
            MarketQueryHandle.DeleteRecord("神圣石");
            List<集市物品> rList = new List<集市物品>();
            var keyResult = MarketQueryHandle.GetKeyList("神圣石", "","chaos");
            if (keyResult ==null)
            {
                XtraMessageBox.Show("POESESSID失效请更新！", "提示信息");
                return 返回状态.失败;
            }
            var model = MarketQueryHandle.GetPrice(rList, keyResult, "神圣石", "通货", "是", "混沌石", 1, 0);
            if (model.err == null)
            {
                if (label1.InvokeRequired)
                {
                    label1.Invoke(new Action(() =>
                    {
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
                    label1.Invoke(new Action(() =>
                    {
                        label1.Text = $"DC比更新失败";
                    }));
                }
                else
                {
                    label1.Text = $"DC比更新失败";
                }
            }
            return 返回状态.成功;
        }
        private void Frm_集市价格查询_Load(object sender, EventArgs e)
        {
            Init();

            //隐藏进度条
            progressBarControl1.Visible = false;
            if (defCheckedAll)
            {
                CheckedAll(); //全选
            }
         
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
            Thread thead = new Thread(() =>
            {
                if (GetScale()== 返回状态.成功)
                {
                    if (simpleButton_dcb.InvokeRequired)
                    {
                        simpleButton_dcb.Invoke(new Action(() =>
                        {
                            simpleButton_dcb.Enabled = true;
                        }));
                    }
                    else
                    {
                        simpleButton_dcb.Enabled = true;
                    }
                    if (simpleButton_query.InvokeRequired)
                    {
                        simpleButton_query.Invoke(new Action(() =>
                        {
                            simpleButton_query.Enabled = true;
                        }));
                    }
                    else
                    {
                        simpleButton_query.Enabled = true;
                    }
                }
                else
                {
                    simpleButton_query.Invoke(new Action(() =>
                    {
                        simpleButton_dcb.Enabled = true;
                    }));
                    simpleButton_query.Invoke(new Action(() =>
                    {
                        simpleButton_query.Enabled = true;
                    }));
                    return;
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
                if (price.Contains("错误") || price.Contains("不足"))
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }
        //点击终止查询
        private void simpleButton_zz_Click(object sender, EventArgs e)
        {
            // 取消线程
            cts.Cancel();
        }
        private void DoWork(CancellationToken cancellationToken)
        {
            StartQuery(cancellationToken);
            simpleButton_zz.Invoke(new Action(() =>
            {
                simpleButton_zz.Enabled = false;
            }));
        }

        private void checkEdit_qfx_CheckedChanged(object sender, EventArgs e)
        {
            if (defCheckedAll)
            {
                defCheckedAll = false;
                UnCheckedAll();
            }
            else
            {
                defCheckedAll = true;
                CheckedAll();
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
    public class 暂停
    {
        public int SleepTime { get; set; }

        public int FloatTime { get; set;}
    }
}
