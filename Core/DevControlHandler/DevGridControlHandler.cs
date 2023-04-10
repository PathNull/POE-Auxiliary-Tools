using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using DevExpress.XtraGrid;
using System.Web.UI.WebControls;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace Core.DevControlHandler
{
    public class DevGridControlHandler
    {
       
        /// <summary>
        /// 点击GridConrtol绑定内容到控件,控件 Name  需和 ListViewSubItem Name 匹配
        /// </summary>
        public static void ControlBindContent(LayoutControlGroup control, GridView gridView, List<ListViewHeader> headers)
        {
            LayoutGroupItemCollection sonControls = control.Items;
            List<ComboBox> tempComboBoxList = new List<ComboBox>();
            List<ComboBox> tempComboBoxList2 = new List<ComboBox>();
            foreach (Control item in sonControls)
            {
                foreach (GridColumn em in (GridColumnCollection)gridView.GetFocusedRow())
                {
                    
                    if ((em.Name == null ? "" : em.Name.ToString()) == item.Name)
                    {
                        var _header = headers.Find(x => x.ControlName == item.Name);
                        var type = item.Name.Split('_')[0];
                        if (_header.IsAssignment)
                        {
                            switch (type)
                            {
                                case "textBox":
                                    item.Text = em.GetTextCaption();
                                    break;
                                case "comboBox":
                                    var combo = (item as ComboBox);
                                    if (em.GetTextCaption().ToString() != "")
                                    {
                                        //var index = combo.FindString(em.Text.ToString());// .FindString(em.Text.ToString()); //FindString();
                                        if (_header.LastAssignment)
                                        {
                                            tempComboBoxList.Add(combo);
                                        }
                                        if (_header.ComboBoxOrder != 0)
                                        {
                                            tempComboBoxList2.Add(combo);
                                        }
                                        else
                                        {
                                            combo.SelectedValue = em.GetTextCaption().ToString();
                                            //去掉绑定的事件
                                            //PropertyInfo pi = (typeof(Control)).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
                                            //EventHandlerList ehl = (EventHandlerList)pi.GetValue(combo, null);
                                            //var fieldInfo = (typeof(ComboBox)).GetField("EVENT_SELECTEDINDEXCHANGED", BindingFlags.Static | BindingFlags.NonPublic);
                                            //Delegate d = ehl[fieldInfo.GetValue(null)];
                                            //ehl.RemoveHandler(fieldInfo.GetValue(combo), d);
                                            //combo.SelectedIndex = index;
                                            //ehl.AddHandler(fieldInfo.GetValue(combo), d);
                                        }
                                    }
                                    else
                                        combo.SelectedIndex = -1;
                                    break;
                                case "checkBox":
                                    var checkBox = (item as System.Windows.Forms.CheckBox);
                                    checkBox.Checked = Convert.ToBoolean(em.Tag);
                                    break;
                                case "dateTimePicker":
                                    var dateTimePicker = (item as DateTimePicker);
                                    if (em.GetTextCaption() == " ")
                                    {
                                        dateTimePicker.CustomFormat = " ";
                                    }
                                    else
                                    {
                                        dateTimePicker.Text = em.GetTextCaption();
                                    }
                                    break;
                            }
                        }
                    }
                }
                //可用性限制
                var header = headers.Find(x => x.ControlName == item.Name);
                if (header != null)
                {
                    if (!header.IsAllowUpdates)
                    {
                        item.Enabled = false;
                    }
                }
                //if (item.Controls != null)
                //{
                //    ControlBindContent(item,listView, headers);
                //}
            }
        }
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridView"></param>
        /// <param name="model"></param>
        /// <param name="headers"></param>
        /// <param name="key"></param>
        public static bool AddRecord<T>(GridControl gc,T model, List<ListViewHeader> headers,string key=null)
        {
            DataTable dt = (DataTable)gc.DataSource;
            if (dt == null)
            {
                dt = new DataTable();
                foreach (ListViewHeader item in headers)
                {
                    if (item.HeaderName != null)
                    {
                        dt.Columns.Add(item.HeaderName);
                    }
                }
               
               
            }
            else
            {
                if (key != null)
                {
                    var list = key.Split('&');
                    var str = "";
                    int index = 0;
                    foreach (var item in list)
                    {
                        if (index == 0)
                        {
                            var val = ObjectHandler.GetPropertyValue(model, item);
                            str = $"{item}='{val}'";
                        }
                        else
                        {
                            var val = ObjectHandler.GetPropertyValue(model, item);
                            str += $" and {item}='{val}'";
                        }
                        index++;
                    }
                    var rows = dt.Select(str);

                    if (rows.Length > 0)
                    {
                        return false;
                    }
                }
            }
            DataRow newRow = dt.NewRow();
            foreach (ListViewHeader item in headers)
            {
                if (item.FieldName != null)
                {
                    var val = ObjectHandler.GetPropertyValue(model, item.FieldName);
                    newRow[item.HeaderName] = val;

                }
            }
            dt.Rows.Add(newRow);
            gc.DataSource = dt;
            return true;
        }
        public static bool AddRecord<T>(GridControl gc, T model)
        {
            


            //DataTable dt = (DataTable)gc.DataSource;
            //if (dt==null)
            //{
            //    dt = new DataTable();
            //    foreach (var item in model.GetType().GetProperties())
            //    {
            //       dt.Columns.Add(item.Name);
            //    }
            //}
            //DataRow newRow = dt.NewRow();
            //foreach (var item in model.GetType().GetProperties())
            //{
            //    var val = ObjectHandler.GetPropertyValue(model, item.Name).ToString();
            //    newRow[item.Name] = val;
            //}
            //dt.Rows.Add(newRow);
            //if (gc.InvokeRequired)
            //{
            //    Action SetSource = delegate { SetDataSource(gc, dt); };
            //    gc.Invoke(SetSource);    
            //}
            //else
            //{
            //    gc.DataSource = dt;;
            //};
            return true;
        }
        public static void AddRecord<T>(DataTable dt, T model)
        {
            DataRow newRow = dt.NewRow();
            foreach (var item in model.GetType().GetProperties())
            {
                var val = ObjectHandler.GetPropertyValue(model, item.Name).ToString();
                newRow[item.Name] = val;
            }
            dt.Rows.Add(newRow);
        }
     
        /// <summary>
        /// 列表数据转list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gc"></param>
        /// <returns></returns>
        public static List<T> RecordsToList<T>(GridControl gc) where T : new()
        {
            List<T> result = new List<T>();
            DataTable dt = (DataTable)gc.DataSource;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    T model = new T();
                    foreach (var em in model.GetType().GetProperties())
                    {
                        if (dr.Table.Columns.Contains(em.Name))
                        {
                            ObjectHandler.SetPropertyValue(model, em.Name, dr[em.Name]);
                        }
                    }
                    result.Add(model);
                }
            }
           
            return result;
        }

        public static T GetSelectModel<T>(GridView gridView) where T : new()
        {
            var row = gridView.GetSelectedRows();
            if (row.Length > 0)
            {
                var obj = gridView.GetRow(row[0]);
                T model = ObjectHandler.ConvertObject<T>(obj);
                return model;
            }
            else
            {
                return default(T);
            }
        }
    }
}
