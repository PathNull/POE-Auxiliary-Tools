using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace CSRTMISYC.Core.ControlHelper
{
    public static class ControlsHandler
    {
        /// <summary>
        /// 遍历获取指定控件
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="control">要遍历的控件</param>
        /// <param name="controlsName">控件名</param>
        /// <returns></returns>
        public static T GetControl<T>(Control control, string controlsName) where T : Control
        {
            if (control == null) return null;
            Control _control;
            for (int i = 0; i < control.Controls.Count; i++)
            {
                _control = control.Controls[i];
                if (_control == null) return null;
                if (_control.Name == controlsName && _control is T)
                    return (T)_control;
                if (_control.HasChildren)
                {
                    _control = GetControl<T>(_control, controlsName);
                    if (_control != null)
                        return (T)_control;
                }
            }
            return null;
        }
     
        /// 
        /// 移除控件某个事件
        /// 
        /// 控件
        /// 需要移除的控件名称eg:EventClick
        public static void RemoveControlEvent(this Control control, string eventName)
        {
            FieldInfo _fl = typeof(Control).GetField(eventName, BindingFlags.Static | BindingFlags.NonPublic);
            if (_fl != null)
            {
                object _obj = _fl.GetValue(control);
                PropertyInfo _pi = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                EventHandlerList _eventlist = (EventHandlerList)_pi.GetValue(control, null);
                if (_obj != null && _eventlist != null)
                    _eventlist.RemoveHandler(_obj, _eventlist[_obj]);
            }
        }
    }
}
