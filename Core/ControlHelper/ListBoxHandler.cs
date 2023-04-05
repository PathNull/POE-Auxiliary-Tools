using System.Collections.Generic;
using System.Windows.Forms;

namespace Core.ControlHelper
{
    public static class ListBoxHandler
    {
        //绑定数据
        public static void BindData<T>(ListBox obj, T data)
        {
            List<Item> list = new List<Item>();
            int i = 0;
            foreach (var item in data.GetType().GetProperties())
            {
                if (!item.Name.Contains("分隔线"))
                {
                    var value = ObjectHandler.GetPropertyValue(data, item.Name);
                    var content = value == null ? "" : value.ToString();
                    var text = item.Name + ":" + content;
                    list.Add(new Item() { Id = i, Display = text });
                }
                else
                {
                    list.Add(new Item() { Id = i, Display = "--------------------------------" });
                }
                i++;
            }
            obj.DataSource = list;
            obj.DisplayMember = "Display";
            obj.ValueMember = "Id";
        }
    }



    class Item
    {
        public int Id { get; set; }

        public string Display { get; set; }
    }
}
