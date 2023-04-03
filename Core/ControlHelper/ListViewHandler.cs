using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CSRTMISYC.Core.ControlHelper
{
    public static class ListViewHandler
    {
      
        public static string GetItemValueByName(ListView listView, string columnName)
        {
            var index = listView.Columns[columnName].Index;
            string txt = "";
            if (listView.SelectedItems[0].SubItems[index].Tag != null)
            {
                txt = listView.SelectedItems[0].SubItems[index].Tag.ToString();
            }
            return txt;
        }
        /// <summary>
        /// 获取选中行指定列的text
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="columnName">headerName</param>
        /// <returns></returns>
        public static string GetItemTextByName(ListView listView, string columnName)
        {
            var index = listView.Columns[columnName].Index;
            string txt = "";
            if (listView.SelectedItems[0].SubItems[index].Text != null)
            {
                txt = listView.SelectedItems[0].SubItems[index].Text.ToString();
            }
            return txt;
        }
    }

    public class MyListViewSort : System.Collections.IComparer

    {
        private int col;
        private bool descK;
        public MyListViewSort()
        {
            col = 0;
        }

        public MyListViewSort(int column, object Desc)
        {
            descK = Convert.ToBoolean(Desc);
            col = column; //当前列,0,1,2...,参数由ListView控件的ColumnClick事件传递
        }
        public int Compare(object x, object y)
        {
            int tempInt = String.Compare(

                ((ListViewItem)x).SubItems[col].Text,

                ((ListViewItem)y).SubItems[col].Text);

            if (descK) return -tempInt;

            else return tempInt;
        }
    }


    public class MyListView
    {
        public string Name { get; set; }

        public List<bool> Cols { get; set; }
    }

    public class ListViewBuff : ListView
    {
        
        public ListViewBuff()
        {
            this.SetStyle(                                //设置控件的样式和行为
            ControlStyles.DoubleBuffer |                   //绘制在缓冲区中进行，完成后将结果输出到屏幕上。双重缓冲区可防止由控件重绘引起的闪烁
            ControlStyles.OptimizedDoubleBuffer |          //控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁
            ControlStyles.AllPaintingInWmPaint, true);     //控件将忽略WM_ERASEBKGND（当窗口背景必须被擦除时 例如窗口改变大小时）窗口消息以减少闪烁
            UpdateStyles();                               //更新控件的样式和行为
        }
    }
}
