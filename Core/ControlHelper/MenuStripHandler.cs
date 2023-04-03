using System;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace CSRTMISYC.Core.ControlHelper
{
    public static class MenuStripHandler
    {
        //private static Form form;
        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>

        public static ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);

                return tsmi;
            }

            return null;
        }

        public static void MenuClicked(object sender, EventArgs e)
        {
            //以下主要是动态生成事件并打开窗体

            //((sender as ToolStripMenuItem).Tag)强制转换
            var a = (sender as Form);
            ObjectHandle t = Activator.CreateInstance("WinForms", "WinForms.BasicData");
            Form f = (Form)t.Unwrap();
            f.ShowDialog();

        }
    }
}
