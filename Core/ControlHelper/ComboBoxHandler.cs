using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.CheckedListBox;

namespace Core.ControlHelper
{
    public static class ComboBoxHandler
    {
        public readonly static Dictionary<string, string> ProvinceCodeDic = new Dictionary<string, string> { { "四川省", "510000" }, { "北京市", "110000" }, { "天津市", "120000" }, { "河北省", "130000" }, { "山西省", "140000" }, { "内蒙古自治区", "150000" }, { "辽宁省", "210000" }, { "吉林省", "220000" }, { "黑龙江省", "230000" }, { "上海市", "310000" }, { "江苏省", "320000" }, { "浙江省", "330000" }, { "安徽省", "340000" }, { "福建省", "350000" }, { "江西省", "360000" }, { "山东省", "370000" }, { "河南省", "410000" }, { "湖北省", "420000" }, { "湖南省", "430000" }, { "广东省", "440000" }, { "广西自治区", "450000" }, { "海南省", "460000" }, { "重庆市", "500000" }, { "贵州省", "520000" }, { "云南省", "530000" }, { "西藏自治区", "540000" }, { "陕西省", "610000" }, { "甘肃省", "620000" }, { "青海省", "630000" }, { "宁夏自治区", "640000" }, { "新疆自治区", "650000" }, { "台湾省", "710000" }, { "香港特别行政区", "810000" }, { "澳门特别行政区", "820000" } };
        public delegate void myDelegate(object sender, EventArgs e);

        public static void BindProvince(ComboBox comboBox)
        {
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            foreach (var item in ProvinceCodeDic)
            {
                DropDownList.Add(new ComboBoxListItem(item.Key, item.Key));
            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.SelectedIndex = -1;
            comboBox.SelectedItem = null;
        }

        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">comboBox name</param>
        /// <param name="list">数据</param>
        /// <param name="myDel">绑定的时间</param>
        /// <param name="IsAddTotalItem">是否添加 ‘<全部>’</param>
        /// <param name="keyName">绑定到comboBox Text 的list列名</param>
        /// <param name="valueName">绑定到comboBox Value 的list列名</param>
        public static void BindData<T>(ComboBox comboBox, List<T> list, EventHandler myDel, bool IsAddTotalItem, string keyName, string valueName = "") where T : new()
        {
            comboBox.SelectedIndexChanged -= myDel;
            var _list = ArrayHandler.FilterDuplicates<T>(list, keyName);
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            if (IsAddTotalItem)
            {
                DropDownList.Add(new ComboBoxListItem("<全部>", "<全部>"));
            }
            foreach (var item in _list)
            {
                string value = "";
                var key = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                if (valueName == "")
                {
                    value = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                }
                else
                {
                    value = ObjectHandler.GetPropertyValue(item, valueName).ToString();
                }
                DropDownList.Add(new ComboBoxListItem(key, value));
            }
            if (comboBox.InvokeRequired)
            {
                comboBox.BeginInvoke(new Action(() =>
                {
                    comboBox.DataSource = DropDownList;
                    comboBox.DisplayMember = "Key";
                    comboBox.ValueMember = "Value";
                    comboBox.SelectedIndex = -1;
                    comboBox.SelectedItem = null;
                    comboBox.SelectedIndexChanged += myDel;
                }));
            }
            else
            {
                comboBox.DataSource = DropDownList;
                comboBox.DisplayMember = "Key";
                comboBox.ValueMember = "Value";
                comboBox.SelectedIndex = -1;
                comboBox.SelectedItem = null;
                comboBox.SelectedIndexChanged += myDel;
            }
        }
        /// <summary>
        /// 绑定下拉框数据并检索
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">comboBox name</param>
        /// <param name="list">数据</param>
        /// <param name="myDel">绑定的时间</param>
        /// <param name="IsAddTotalItem">是否添加 ‘<全部>’</param>
        /// <param name="keyName">绑定到comboBox Text 的list列名</param>
        /// <param name="valueName">绑定到comboBox Value 的list列名</param>
        public static void BindDataRetrieval<T>(ComboBox comboBox, List<T> list, EventHandler myDel, bool IsAddTotalItem, string keyName, string valueName = "") where T : new()
        {
            //comboBox.KeyDown -= comboBox_KeyDown;
            comboBox.SelectedIndexChanged -= myDel;

            var uniqueList = ArrayHandler.FilterDuplicates<T>(list, keyName);
            var dropDownList = uniqueList.Select(item =>
            {
                var key = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                var value = string.IsNullOrEmpty(valueName) ? key : ObjectHandler.GetPropertyValue(item, valueName).ToString();
                return new ComboBoxListItem(key, value);
            }).ToList();

            if (IsAddTotalItem)
            {
                dropDownList.Insert(0, new ComboBoxListItem("<全部>", "<全部>"));
            }

            Action initializeComboBox = () =>
            {
                comboBox.DataSource = dropDownList;
                comboBox.DisplayMember = "Key";
                comboBox.ValueMember = "Value";
                comboBox.SelectedIndex = -1;
                comboBox.SelectedItem = null;
                if (comboBox.DataSource != null)
                {
                    comboBox.SelectedIndexChanged += myDel;
                    comboBox.Tag = dropDownList;
                    //comboBox.KeyDown += comboBox_KeyDown;
                }
            };

            if (comboBox.InvokeRequired)
            {
                comboBox.BeginInvoke(initializeComboBox);
            }
            else
            {
                initializeComboBox();
            }

        }
        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">comboBox name</param>
        /// <param name="list">数据</param>
        /// <param name="myDel">绑定的时间</param>
        /// <param name="IsAddTotalItem">是否添加 ‘<全部>’</param>
        /// <param name="IsAddEmpty">是否添加空白</param>
        /// <param name="keyName">绑定到comboBox Text 的list列名</param>
        /// <param name="valueName">绑定到comboBox Value 的list列名</param>
        public static void BindData<T>(ComboBox comboBox, List<T> list, EventHandler myDel, bool IsAddTotalItem,bool IsAddEmpty, string keyName, string valueName = "") where T : new()
        {
            comboBox.SelectedIndexChanged -= myDel;
            var _list = ArrayHandler.FilterDuplicates<T>(list, keyName);
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            if (IsAddTotalItem)
            {
                DropDownList.Add(new ComboBoxListItem("<全部>", "<全部>"));
            }
            foreach (var item in _list)
            {
                string value = "";
                var key = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                if (valueName == "")
                {
                    value = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                }
                else
                {
                    value = ObjectHandler.GetPropertyValue(item, valueName).ToString();
                }
                DropDownList.Add(new ComboBoxListItem(key, value));
            }
            if (IsAddEmpty)
            {
                DropDownList.Add(new ComboBoxListItem(" ", " "));
            }
            if (comboBox.InvokeRequired)
            {
                comboBox.BeginInvoke(new Action(() =>
                {
                    comboBox.DataSource = DropDownList;
                    comboBox.DisplayMember = "Key";
                    comboBox.ValueMember = "Value";
                    comboBox.SelectedIndex = -1;
                    comboBox.SelectedItem = null;
                    comboBox.SelectedIndexChanged += myDel;
                }));
            }
            else
            {
                comboBox.DataSource = DropDownList;
                comboBox.DisplayMember = "Key";
                comboBox.ValueMember = "Value";
                comboBox.SelectedIndex = -1;
                comboBox.SelectedItem = null;
                comboBox.SelectedIndexChanged += myDel;
            }
        }
        public static void BindOrRemoveEvent(ComboBox comboBox, EventHandler myDel, bool isBind)
        {
            if (isBind)
            {
                comboBox.SelectedIndexChanged += myDel;
            }
            else
            {
                comboBox.SelectedIndexChanged -= myDel;
            }
        }
        public static void BindData<T>(ComboBox comboBox, List<T> list, EventHandler myDel, bool IsAddTotalItem, string keyName, int defaultIndex, string valueName = "") where T : new()
        {
            comboBox.SelectedIndexChanged -= myDel;
            //ControlsHandler.RemoveControlEvent(comboBox, "");
            var _list = ArrayHandler.FilterDuplicates<T>(list, keyName);
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            if (IsAddTotalItem)
            {
                DropDownList.Add(new ComboBoxListItem("<全部>", "<全部>"));
            }
            foreach (var item in _list)
            {
                string value = "";
                var key = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                if (valueName == "")
                {
                    value = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                }
                else
                {
                    value = ObjectHandler.GetPropertyValue(item, valueName).ToString();
                }
                DropDownList.Add(new ComboBoxListItem(key, value));
            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = defaultIndex;
            comboBox.SelectedIndexChanged += myDel;
        }

        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">下拉框</param>
        /// <param name="list">原始数据</param>
        /// <param name="IsAddTotalItem">是否添加全部选项</param
        /// <param name="keyName">按该字段抽取不重复记录</param>
        /// <param name="valueName">实际绑定到item的值字段Name（插入数据库时的值）</param>
        public static void BindData<T>(ComboBox comboBox, List<T> list, bool IsAddTotalItem, string keyName, string valueName = "") where T : new()
        {
            var _list = ArrayHandler.FilterDuplicates<T>(list, keyName);
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            if (IsAddTotalItem)
            {
                DropDownList.Add(new ComboBoxListItem("<全部>", "<全部>"));
            }
            foreach (var item in _list)
            {
                string value = "";
                if (ObjectHandler.GetPropertyValue(item, keyName) != null)
                {
                    var key = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                    if (valueName == "")
                    {
                        value = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                    }
                    else
                    {
                        value = ObjectHandler.GetPropertyValue(item, valueName).ToString();
                    }
                    DropDownList.Add(new ComboBoxListItem(key, value));
                }

            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = -1;
            comboBox.SelectedItem = null;
        }
        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">下拉框</param>
        /// <param name="list">原始数据</param>
        /// <param name="IsAddTotalItem">是否添加全部选项</param
        /// <param name="keyName">按该字段抽取不重复记录</param>
        /// <param name="valueName">实际绑定到item的值字段Name（插入数据库时的值）</param>
        public static void BindData<T>(ComboBox comboBox, List<T> list, bool IsAddTotalItem, bool IsAddEmpty, string keyName, string valueName = "") where T : new()
        {
            var _list = ArrayHandler.FilterDuplicates<T>(list, keyName);
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            if (IsAddTotalItem)
            {
                DropDownList.Add(new ComboBoxListItem("<全部>", "<全部>"));
            }
            foreach (var item in _list)
            {
                string value = "";
                if (ObjectHandler.GetPropertyValue(item, keyName) != null)
                {
                    var key = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                    if (valueName == "")
                    {
                        value = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                    }
                    else
                    {
                        value = ObjectHandler.GetPropertyValue(item, valueName).ToString();
                    }
                    DropDownList.Add(new ComboBoxListItem(key, value));
                }

            }
            if (IsAddEmpty)
            {
                DropDownList.Add(new ComboBoxListItem(" ", " "));
            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = -1;
            comboBox.SelectedItem = null;
        }
        public static void BindData<T>(ComboBox comboBox, List<T> list, EventHandler myDel, string keyName, string valueName = "") where T : new()
        {
            comboBox.SelectedIndexChanged -= myDel;
            var _list = ArrayHandler.FilterDuplicates<T>(list, keyName);
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            foreach (var item in _list)
            {
                string value = "";
                var key = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                if (valueName == "")
                {
                    value = ObjectHandler.GetPropertyValue(item, keyName).ToString();
                }
                else
                {
                    value = ObjectHandler.GetPropertyValue(item, valueName).ToString();
                }
                DropDownList.Add(new ComboBoxListItem(key, value));
            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = -1;
            comboBox.SelectedItem = null;
            comboBox.SelectedIndexChanged += myDel;
        }
        public static void BindData(ComboBox comboBox, string[] list)
        {
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            foreach (var item in list)
            {
                DropDownList.Add(new ComboBoxListItem(item, item));
            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = -1;
            comboBox.SelectedItem = null;
        }
        public static void BindData(ComboBox comboBox, string[] list, EventHandler myDel)
        {
            comboBox.SelectedIndexChanged -= myDel;
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            foreach (var item in list)
            {
                DropDownList.Add(new ComboBoxListItem(item, item));
            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = -1;
            comboBox.SelectedItem = null;
            comboBox.SelectedIndexChanged += myDel;
        }
        public static void BindData(ComboBox comboBox, string[] list, EventHandler myDel, int defaultIndex)
        {
            comboBox.SelectedIndexChanged -= myDel;
            List<ComboBoxListItem> DropDownList = new List<ComboBoxListItem>();
            foreach (var item in list)
            {
                DropDownList.Add(new ComboBoxListItem(item, item));
            }
            comboBox.DataSource = DropDownList;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = defaultIndex;
            comboBox.SelectedIndexChanged += myDel;
        }
        /// <summary>
        /// combox信息检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void comboBox_TextChanged(object sender, EventArgs e)
        {
            
            var combox = sender as ComboBox;
            var originalItems = new object[combox.Items.Count];
            var data = (List<ComboBoxListItem>)combox.Tag;
            //data.ToArray().CopyTo(originalItems, 0);
            var item = combox.Items;
            
            string searchText = combox.Text.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                var list = data.Where(x => x.Key.Contains(searchText));
                combox.DataSource = list.ToList();
                combox.DroppedDown = true; // 强制下拉框弹出，显示筛选结果
            }
            else
            {
                combox.DataSource = data;
                combox.DisplayMember = "Key";
                combox.ValueMember = "Value";
                combox.SelectedIndex = -1;
                //combox.Items.Clear();
                //combox.Items.AddRange(originalItems);
                combox.DroppedDown = false; // 隐藏下拉框
            }
        }
        private static void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var combox = sender as ComboBox;
                var originalItems = new object[combox.Items.Count];
                var data = (List<ComboBoxListItem>)combox.Tag;
                //data.ToArray().CopyTo(originalItems, 0);
                var item = combox.Items;

                string searchText = combox.Text.ToLower();
                if (!string.IsNullOrEmpty(searchText))
                {
                    var list = data.Where(x => x.Key.Contains(searchText));
                    combox.DataSource = list.ToList();
                    combox.DroppedDown = true; // 强制下拉框弹出，显示筛选结果
                }
                else
                {
                    combox.DataSource = data;
                    combox.DisplayMember = "Key";
                    combox.ValueMember = "Value";
                    combox.SelectedIndex = -1;
                    //combox.Items.Clear();
                    //combox.Items.AddRange(originalItems);
                    combox.DroppedDown = false; // 隐藏下拉框
                }
            }
           
        }
    }
 
}


public class ComboBoxListItem
{
    public string Key { get; set; }
        public string Value { get; set; }

        public ComboBoxListItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
   }

