using CSRTMISYC.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_集市价格查询 : BaseForm
    {
        public Frm_集市价格查询()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            var list =  XMLHandler.Read("集市价格查询");
            foreach (var item in list)
            {
                CheckEdit editorName = new CheckEdit() { Name = "editorName" ,Text=item,Width=10};
                // 创建一个新的LayoutControlItem
                LayoutControlItem itemName = new LayoutControlItem();
                itemName.Name = "liName";
                itemName.Text = item;
                itemName.Control = editorName;
                itemName.Width = 20;

                // 设置LayoutControlItem的布局方式，将其放置在第一行的下方，且宽度为内容宽度
                itemName.Move(layoutControlGroup1, DevExpress.XtraLayout.Utils.InsertType.Right);
                // 设置CheckEdit控件在LayoutControlItem中的布局方式，使其向右排列
                itemName.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;

                layoutControlGroup1.AddItem(itemName);
            }
           



        }
    }
}
