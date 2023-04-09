using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DevControlHandler
{
    public class DevComboBoxEditHandler
    {
        public static void BindData<T>(ComboBoxEdit comboBox, List<T> list, EventHandler myDel, bool IsAddTotalItem, string keyName, string valueName = "") where T : new()
        {
            comboBox.Properties.Items.Clear();
            comboBox.SelectedIndexChanged -= myDel;
            //ControlsHandler.RemoveControlEvent(comboBox, "");
            var _list = ArrayHandler.FilterDuplicates<T>(list, keyName);
            if (IsAddTotalItem)
            {
                comboBox.Properties.Items.Add(new ListItem("<全部>", "<全部>"));
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
                comboBox.Properties.Items.Add(new ListItem(key, value));
            }
            comboBox.SelectedIndexChanged += myDel;
        }
        /// <summary>
        /// 根据内容设置选中combox选项
        /// </summary>
        /// <param name="combox"></param>
        /// <param name="text"></param>
        public static void SetItemByText(ComboBoxEdit combox,string text)
        {
            foreach (object item in combox.Properties.Items)
            {
                if (item.ToString() == text)
                {
                    combox.SelectedItem = item;
                    break;
                }
            }
        }

    }
    public class ListItem : Object
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public ListItem(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
