using System.Collections.Generic;
using System.Windows.Forms;

namespace CSRTMISYC.Core.ControlHelper
{
    public static class DataGridViewHandler
    {
        /// <summary>
        /// 根据视图模型初始化DataGridView
        /// </summary>
        /// <param name="obj">DataGridView对象</param>
        /// <param name="model">视图模型</param>
        /// <param name="cols">默认生成的行数</param>
        /// <param name="exclude">需要排除的项</param>
        public static void InitDgvByCustom<T>(DataGridView obj, T model, int rows, List<string> exclude)
        {
            foreach (var item in model.GetType().GetProperties())
            {
                if (!exclude.Contains(item.Name))
                {
                    //创建列
                    InitDgvTextBoxColumn(obj, DataGridViewContentAlignment.MiddleCenter, StringHandler.GetSpell(item.Name.ToString()), item.Name.ToString(), 20, true, true);
                }
            }
            //创建行
            for (int i = 1; i <= rows; i++)
            {
                DataGridViewRow drRow1 = new DataGridViewRow();
                drRow1.CreateCells(obj);
                //将新创建的行添加到DataGridView中
                obj.Rows.Add(drRow1);
            }
            //设置DataGridView的属性
            obj.AllowUserToAddRows = false;
            obj.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            obj.AllowUserToResizeRows = false;
            obj.MultiSelect = false;

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="data"></param>
        ///  /// <param name="exclude">要排除的项</param>
        public static void BindData<T>(DataGridView obj, List<T> data, List<string> exclude)
        {
            obj.Rows.Clear();
            foreach (var item in data)
            {
                int i = 0;
                DataGridViewRow drRow1 = new DataGridViewRow();
                drRow1.CreateCells(obj);
                foreach (DataGridViewColumn em in obj.Columns)
                {
                    if (!exclude.Contains(em.HeaderText))
                    {
                        //设置单元格的值
                        var value = ObjectHandler.GetPropertyValue(item, em.HeaderText);
                        drRow1.Cells[i].Value = value;
                        i++;
                    }
                }
                //将新创建的行添加到DataGridView中
                obj.Rows.Add(drRow1);
            }
            //设置DataGridView的属性
            obj.AllowUserToAddRows = false;//不自动产生最后的新行
        }
        /// <summary>
        /// 创建DataGridView的TextBox列
        /// </summary>
        /// <param name="dgv">要创建列的DataGridView</param>
        /// <param name="_alignmeng">设置列的对齐方式</param>
        /// <param name="_columnName">列名</param>
        /// <param name="_headerText">显示的标题名</param>
        /// <param name="_maxInputLength">可输入的最大长度</param>
        /// <param name="_readOnly">设置列是否只读 true只读 false 读写</param>
        /// <param name="_visible">设置列是否可见 true 可见 false 不可见</param>
        private static void InitDgvTextBoxColumn(DataGridView dgv, DataGridViewContentAlignment _alignmeng,
            string _columnName, string _headerText, int _maxInputLength, bool _readOnly, bool _visible)
        {
            //实例化一个DataGridViewTextBoxColumn列
            DataGridViewTextBoxColumn tbc = new DataGridViewTextBoxColumn();
            tbc.HeaderCell.Style.Alignment = _alignmeng;
            tbc.Name = _columnName;
            tbc.HeaderText = _headerText;
            tbc.MaxInputLength = _maxInputLength;
            tbc.ReadOnly = _readOnly;
            tbc.Visible = _visible;
            dgv.Columns.Add(tbc);
        }

        /// <summary>
        /// 创建DataGridView的CheckBox列
        /// </summary>
        /// <param name="dgv">要创建列的DataGridView</param>
        /// <param name="_alignmeng">设置列的对齐方式</param>
        /// <param name="_columnName">列名</param>
        /// <param name="_headerText">显示的标题名</param>
        /// <param name="_readOnly">设置列是否只读 true只读 false 读写</param>
        /// <param name="_visible">设置列是否可见 true 可见 false 不可见</param>
        private static void InitDgvCheckBoxColumn(DataGridView dgv, DataGridViewContentAlignment _alignmeng,
            string _columnName, string _headerText, bool _readOnly, bool _visible)
        {
            //实例化一个DataGridViewTextBoxColumn列
            DataGridViewCheckBoxColumn cbc = new DataGridViewCheckBoxColumn();
            cbc.HeaderCell.Style.Alignment = _alignmeng;
            cbc.Name = _columnName;
            cbc.HeaderText = _headerText;

            //设置是否默认选中
            //cbc.Selected = _selected.Equals("男") ? true : false;
            cbc.ReadOnly = _readOnly;
            cbc.Visible = _visible;
            dgv.Columns.Add(cbc);
        }
    }
}
