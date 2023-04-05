using Core;
using Core.Common;
using Core.Web;
using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Model;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_集市价格查询 : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
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

        private void simpleButton_query_Click(object sender, System.EventArgs e)
        {
            string ur = "https://poe.game.qq.com/api/trade/fetch/52ec3b6a980298b0d86f3cfacf4a211ef0050162845a80e3bf4e182e00982faf,107da33b3978590b2ac657f8a80db2265a5db6ff218c78ac1a70a10987da96be,28f2b9fa352765d6c1438ca6f772495177d9d76d7a976fe74ea036f282c5d8ae,64cd07124668a48002fd44ccf4b94f28a4880507001ee43300d7f5458a5d98ee,32e26080717b8ce5a5e93763db8f983e0026b674c52a3084c2071a4486983e82,d7ef6cf21605659bc8229df92623854a54dae47f1eb5469b2be03b629c31f9ba,f703adda3d58e4629a5f2fbd50cd16c711d8671816bd186d9c9d21a383935589,fc701e011b07542f75223b776e83e5021eaf09d402efdd31a556af40d62fec69,d9af648e2f66d5b41f208f920c62b1b43e77c8d084a896da18a6d31eb0803dbe,c55216dd6e8da667e6a0378084e4985d756c436e91313457c69e7c93c30de359?query=bVOGTL";
            string url = "https://poe.game.qq.com/api/trade/search/S21%E8%B5%9B%E5%AD%A3";
            string pass = "123456";
            string personId = "周杰伦";
            Hashtable ht = new Hashtable();//将参数打包成json格式的数据
            string v = "{'query':{'status':{'option':'any'},'type':以太化石,'stats':[{'type':'and','filters':[]}]},'filters':{'trade_filters':{'filters':{'price':{'min':1},'collapse':{'option':'true'},'sale_type':{'option':'priced'}},'disabled':False}},'sort':{'price':'asc'}}";
            string list = HttpUitls.DoPost(url, ht);  //HttpRequest是自定义的一个类
            JObject jsonObject = JObject.Parse(list);
            var id = jsonObject["id"];
            var result = jsonObject["result"];
            var url2 = "https://poe.game.qq.com/api/trade/fetch/";
            for (int i = 0; i < 10; i++)
            {
                url2 += result[i].ToString() + ",";
            }
            url2 = url2.Substring(0, url2.Length - 1);
            url2 += "?query=" + id.ToString();
            var list2 = HttpUitls.Get(url2);
            JObject jsonObject2 = JObject.Parse(list2);
            foreach (var item in jsonObject2["result"])
            {
                var a = item["item"];
            }
        }
    }
}
