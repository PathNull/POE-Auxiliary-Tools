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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_集市价格查询 : BaseForm
    {
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
                // 创建一个新的LayoutControlItem
            }
           


            //TextEdit editorName = new TextEdit() { Name = "editorName" };
            //MemoEdit editorAddress = new MemoEdit() { Name = "editorAddress" };
            //ButtonEdit editorEmail = new ButtonEdit() { Name = "editorEmail" };
            //PictureEdit editorPicture = new PictureEdit() { Name = "pePhoto" };
            //TextEdit editorPhone1 = new TextEdit() { Name = "editorPhone1" };
            //TextEdit editorPhone2 = new TextEdit() { Name = "editorPhone2" };
            //TextEdit editorFax = new TextEdit() { Name = "editorFax" };
            //SimpleButton btnOK = new SimpleButton() { Name = "btnOK", Text = "OK" };
            //SimpleButton btnCancel = new SimpleButton() { Name = "btnCancel", Text = "Cancel" };
            //MemoEdit editorNotes = new MemoEdit() { Name = "editorNotes" };


            ////Create a layout item in the Root group using the LayoutGroup.AddItem method
            //LayoutControlItem itemName = layoutControlGroup1.AddItem();
            //itemName.Name = "liName";
            //itemName.Control = editorName;
            //itemName.Text = "Name";

            //// Add the Photo group.
            //LayoutControlGroup groupPhoto = layoutControlGroup1.AddGroup();
            //groupPhoto.Name = "lgPhoto";
            //groupPhoto.Text = "Photo";
            //// Add a new layout item to the group to display an image.
            //LayoutControlItem liPhoto = layoutControlGroup1.AddItem();
            //liPhoto.Name = "liPhoto";
            //liPhoto.Control = editorPicture;
            //liPhoto.TextVisible = false;

            ////A tabbed group
            //TabbedControlGroup tabbedGroup = layoutControlGroup1.AddTabbedGroup(groupPhoto, InsertType.Right);
            //tabbedGroup.Name = "TabbedGroupPhoneFax";
            //// Add the Phone group as a tab.
            //LayoutControlGroup groupPhone = tabbedGroup.AddTabPage() as LayoutControlGroup;
            //groupPhone.Name = "lgPhone";
            //groupPhone.Text = "Phone";

            //LayoutControlItem liPhone1 = layoutControlGroup1.AddItem();
            //liPhone1.Name = "liPhone1";
            //liPhone1.Control = editorPhone1;
            //liPhone1.Text = "Phone 1";
            //LayoutControlItem liPhone2 = layoutControlGroup1.AddItem();
            //liPhone2.Name = "liPhone2";
            //liPhone2.Control = editorPhone2;
            //liPhone2.Text = "Phone 2";

            //// Add an empty resizable region below the last added layout item.
            //EmptySpaceItem emptySpace11 = new EmptySpaceItem();
            //emptySpace11.Parent = groupPhone;

            //// Add the Fax group as a tab.
            //LayoutControlGroup groupFax = tabbedGroup.AddTabPage() as LayoutControlGroup;
            //groupFax.Name = "lgFax";
            //groupFax.Text = "Fax";
            //LayoutControlItem liFax = layoutControlGroup1.AddItem();
            //liFax.Name = "liFax";
            //liFax.Control = editorFax;
            //liFax.Text = "Fax";

            //// Add an empty resizable region below the last added layout item.
            //EmptySpaceItem emptySpace12 = new EmptySpaceItem();
            //emptySpace12.Parent = groupFax;

            //tabbedGroup.SelectedTabPage = groupPhone;

            ////Create a borderless group to display the OK and CANCEL buttons at the bottom of the LayoutControl
            ////If items are combined in a group, their alignmenent is not dependent on the items outside this group.
            //LayoutControlGroup groupButtons = layoutControlGroup1.AddGroup();
            //groupButtons.Name = "GroupButtons";
            //groupButtons.GroupBordersVisible = false;

            //EmptySpaceItem emptySpace2 = new EmptySpaceItem();
            //emptySpace2.Parent = groupButtons;

            ////Create a layout item (using the LayoutGroup.AddItem method) next to the 'emptySpace2' item
            //LayoutControlItem itemOKButton = layoutControlGroup1.AddItem(emptySpace2, InsertType.Right);
            //itemOKButton.Name = "liButtonOK";
            //itemOKButton.Control = btnOK;
            //itemOKButton.Text = "OK Button";
            //itemOKButton.TextVisible = false;
            //itemOKButton.SizeConstraintsType = SizeConstraintsType.Custom;
            //itemOKButton.MaxSize = new Size(200, 25);
            //itemOKButton.MinSize = new Size(90, 25);

            ////Create a layout item (using the LayoutGroup.AddItem method) next to the 'itemOKButton' item
            //LayoutControlItem itemCancelButton = layoutControlGroup1.AddItem(itemOKButton, InsertType.Right);
            //itemCancelButton.Name = "liButton";
            //itemCancelButton.Control = btnCancel;
            //itemCancelButton.Text = "Cancel Button";
            //itemCancelButton.TextVisible = false;
            //itemCancelButton.SizeConstraintsType = SizeConstraintsType.Custom;
            //itemCancelButton.MaxSize = new Size(200, 25);
            //itemCancelButton.MinSize = new Size(90, 25);


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
            thread = new Thread(new ThreadStart(StartQuery));
            thread.Start();
        }

        public async void StartQuery()
        {
            if (simpleButton_query.InvokeRequired)
            {
                Action SetSource = delegate { simpleButton_query.Enabled = false; };
                simpleButton_query.Invoke(SetSource);
               
            }
            simpleButton_query.Enabled = false;
            var list = gridControl_cxlb.DataSource as List<物品>;
            if (list != null){
                var dt = new DataTable();
                foreach (var item in new 集市查询结果().GetType().GetProperties())
                {
                    dt.Columns.Add(item.Name);
                }
                Random rm = new Random();
                foreach (var item in list)
                {
                    var keyResult = GetKeyList(item.物品名称);
                    resultList = new List<集市物品>();
                    var model = GetPrice(keyResult,item.物品名称,0);
                    var _mo = new 集市查询结果() { 名称 = model.名称, 价格 = model.单价.ToString() };
                    DevGridControlHandler.AddRecord(dt, _mo);
                    var sleep = rm.Next(2, 3) * 1000;
                    Thread.Sleep(sleep);
                }
                if (gridControl1.InvokeRequired)
                {
                    Action SetSource = delegate { gridControl1.DataSource = dt;; };
                    gridControl1.Invoke(SetSource);
                }
                else
                {
                    gridControl1.DataSource = dt; ;
                };
                if (simpleButton_query.InvokeRequired)
                {
                    Action SetSource = delegate { simpleButton_query.Enabled = true; };
                    simpleButton_query.Invoke(SetSource);

                }
            }
          
        }
        public JObject GetKeyList(string name)
        {
            string url = "https://poe.game.qq.com/api/trade/search/S21%E8%B5%9B%E5%AD%A3";
            Hashtable ht = new Hashtable();//将参数打包成json格式的数据
            ht.Add("name", name);
            string list = HttpUitls.DoPost(url, ht);  //HttpRequest是自定义的一个类
            JObject jsonObject = JObject.Parse(list);
            var id = jsonObject["id"];
            var result = jsonObject["result"];
           
            return jsonObject;
        }
        public 集市物品 GetPrice(JObject jsonObject,string name,int i)
        {
            var url2 = "https://poe.game.qq.com/api/trade/fetch/";
            var id = jsonObject["id"];
            var result = jsonObject["result"];
            int j = i + 10;
            int k = i;
            for ( k=i; k < j; k++)
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
                if (TransactionType == "=a/b/o")
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
                    if(count.数量== count.堆叠上限)
                    {
                        resultList.Add(model);
                    }
                }
            }
            if (resultList.Count < 10)
            {
               return GetPrice(jsonObject,name, k);
            }

            //计算平均价格
            double total = 0;
            double price = 0;
            foreach (var item in resultList)
            {
                total += item.单价;
            }
            price = total / (double)resultList.Count;

            //返回结果
            集市物品 obj = new 集市物品() { 名称=name,单价=price};
            return obj;

        }

        private void Frm_集市价格查询_Load(object sender, EventArgs e)
        {

        }
    }
    public class 集市查询结果
    {
        public string 名称 { get; set; }

        public string 价格 { get; set; }
    }
}
