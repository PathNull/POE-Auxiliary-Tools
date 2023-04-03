using System.Collections.Generic;

namespace CSRTMISYC.Core
{
    public static class ArrayHandler
    {
        /// <summary>
        /// 过滤重复数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">需过滤的对象数组</param>
        /// <param name="name">过滤的对象属性</param>
        /// <returns></returns>
        public static List<T> FilterDuplicates<T>(List<T> array, string name)
        {
            List<T> result = new List<T>();
            foreach (var item in array)
            {
                if (ObjectHandler.GetPropertyValue(item, name) != null)
                {
                    bool isAdd = true;
                    //判断是否存在
                    foreach (var em in result)
                    {
                        var _new = ObjectHandler.GetPropertyValue(item, name);
                        var _old = ObjectHandler.GetPropertyValue(em, name);
                        if (_old.Equals(_new) || _new.ToString() == "")
                        {
                            isAdd = false;
                        }
                    }
                    if (isAdd)
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public static void Copy<T>(List<T> source,List<T> target) where T :  new()
        {
            target.Clear();
            foreach (var item in source)
            {
                var model = new T();
                foreach (var em in item.GetType().GetProperties())
                {
                    var val =  ObjectHandler.GetPropertyValue(item, em.Name);
                    ObjectHandler.SetPropertyValue(model, em.Name, val);
                }
                target.Add(model);
            }
        }
    }
}
