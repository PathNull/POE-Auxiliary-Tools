using CSRTMISYC.Model.ViewModels;
using DevExpress.XtraNavBar;

namespace CSRTMISYC.Core.DevControlHandler
{
    public static class NavBarControlHandler
    {
        /// <summary>
        /// 初始化左侧导航节点数据
        /// </summary>
        /// <param name="navBarControl"></param>
        /// <param name="nodeMenu"></param>
        /// <param name="myDel"></param>
        public static void InitMunu(NavBarControl navBarControl, NodeMenuViewModel nodeMenu, NavBarLinkEventHandler myDel)
        {
            navBarControl.Items.Clear();
            navBarControl.Groups.Clear();
            //navBarControl.SmallImages = this.imageTool;//dev  自带图标才会有用
            int i = 0;
            foreach (var nbi in nodeMenu.Nodes)
            {
                NavBarGroup navBarGroup1 = new NavBarGroup();
                navBarGroup1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                navBarGroup1.Appearance.Options.UseFont = true;
                navBarGroup1.TopVisibleLinkIndex = i;
                navBarGroup1.Caption = nbi.节点名称;
                navBarGroup1.Name = "导航菜单";
                //navBarGroup1.SmallImageIndex = nbg.SmallImageIndex;
                if (i == 0)
                    navBarGroup1.Expanded = true;
                foreach (var item in nbi.Nodes)
                {
                    if (item.节点名称 != nbi.节点名称 && item.节点名称 != "原油接车" && item.节点名称 != "铁路卸车通报")
                    {
                        NavBarItem Item = new NavBarItem();
                        Item.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        Item.Caption = item.节点名称;
                        Item.Name = item.节点名称;
                        Item.Tag = item.节点名称;
                        Item.Appearance.Options.UseFont = true;
                        Item.LinkClicked += myDel;
                        if (item.isEnable)
                        {
                            Item.Enabled = true;
                        }
                        else
                        {
                            Item.Enabled = false;
                        }
                        navBarGroup1.ItemLinks.Add(Item);
                        navBarControl.Items.Add(Item);
                    }
                }
                navBarControl.Groups.Add(navBarGroup1);
                i++;
            }
        }

    }

}
