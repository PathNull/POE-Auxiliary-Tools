using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CSRTMISYC.Core
{
    public static class ObjectHandler
    {


        /// <summary>
        /// 判断对象是否包含某个属性
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param>
        /// <returns>是否包含</returns>
        public static bool ContainProperty(this object instance, string propertyName)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo _findedPropertyInfo = instance.GetType().GetProperty(propertyName);
                return (_findedPropertyInfo != null);
            }
            return false;
        }
        /// <summary>
        /// 获取某个对象中的属性值
        /// </summary>
        /// <param name="info"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }
        /// <summary>
        /// 设置某个对象中的属性值
        /// </summary>
        /// <returns></returns>
        public static void SetPropertyValue<T>(object info, string field, T content) where T : new()
        {
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            var s = property.First().PropertyType;
            var b = Nullable.GetUnderlyingType(s);
            //var a = Convert.ChangeType(content, b);
            property.First().SetValue(info, content);
        }

        /// <summary>
        /// 为指定对象分配参数
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="dic">字段/值</param>
        /// <returns></returns>
        public static T Assign<T>(Dictionary<string, string> dic) where T : class, new()
        {
            try
            {
                Type myType = typeof(T);
                T entity = new T();
                var fields = myType.GetProperties();
                string val = string.Empty;
                object obj = null;

                foreach (var field in fields)
                {
                    if (!dic.Keys.Contains(field.Name))
                        continue;
                    val = dic[field.Name];

                    object defaultVal;
                    if (!field.PropertyType.IsGenericType)
                    {
                        if (field.PropertyType.Name.Equals("String"))
                            defaultVal = "";
                        else if (field.PropertyType.Name.Equals("Boolean"))
                        {
                            defaultVal = false;
                            val = (val.Equals("1") || val.Equals("on") || val.Equals("True")).ToString();
                        }
                        else if (field.PropertyType.Name.Equals("Decimal"))
                        {
                            defaultVal = 0M;
                        }
                        else if (field.PropertyType.Name.Equals("Int16"))
                        {
                            defaultVal = Convert.ToInt16(0);
                        }
                        else if (field.PropertyType.Name.Equals("Int32"))
                        {
                            defaultVal = Convert.ToInt32(0);
                        }
                        else if (field.PropertyType.Name.Equals("Int64"))
                        {
                            defaultVal = Convert.ToInt64(0);
                        }
                        else if (field.PropertyType.Name.Equals("Nullable`1"))
                        {
                            defaultVal = null;
                        }
                        else
                        {
                            var type = Nullable.GetUnderlyingType(field.PropertyType);
                            defaultVal = Convert.ChangeType(0, Nullable.GetUnderlyingType(type));
                        }
                    }
                    else
                    {
                        var type = Nullable.GetUnderlyingType(field.PropertyType);
                        if (type.Equals("String"))
                            defaultVal = "";
                        else if (type.Name.Equals("Boolean"))
                        {
                            defaultVal = false;
                            val = (val.Equals("1") || val.Equals("on") || val.Equals("True")).ToString();
                        }
                        else if (type.Name.Equals("Decimal"))
                        {
                            defaultVal = 0M;
                        }
                        else if (type.Name.Equals("Int16"))
                        {
                            defaultVal = Convert.ToInt16(0);
                        }
                        else if (type.Name.Equals("Int32"))
                        {
                            defaultVal = Convert.ToInt32(0);
                        }
                        else if (type.Name.Equals("DateTime"))
                        {
                            defaultVal = null;
                        }
                        else
                        {
                            defaultVal = null;
                        }
                    }



                    if (!field.PropertyType.IsGenericType)
                    {
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
                    }
                    else
                    {
                        Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            var t = Nullable.GetUnderlyingType(field.PropertyType);

                            if (t.Name.Contains("Int"))
                            {
                                bool isNumeric = Regex.IsMatch(val, @"^\d+$");
                                if (!isNumeric)
                                {
                                    switch (val)
                                    {
                                        case "True":
                                        case "true":
                                            val = "1";
                                            break;
                                        case "False":
                                        case "false":
                                            val = "0";
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, t);
                        }

                    }

                    field.SetValue(entity, obj, null);
                }

                return entity;
            }
            catch (Exception ex)
            {
                LogHandler.Error("Assign", ex);
                return new T();
            }
        }
        /// <summary>
        /// 将一个对象的字段值赋给另一个对象的同名字段
        /// </summary>
        /// <param name="soure">源对象</param>
        /// <param name="target">目标对象</param>
        public static void SetModelByModel<T>(object soure, T target) where T : class, new()
        {
            Type soureType = soure.GetType();
            Type myType = typeof(T);
            T entity = new T();
            var fields = myType.GetProperties();
            string val = string.Empty;
            object obj = null;
            object defaultVal=null;
            foreach (var field in fields)
            {
                var s = soure.GetType().GetProperties().SingleOrDefault(x => x.Name == field.Name);
                if (s != null)
                {
                    var m = soureType.GetProperty(s.Name)?.GetValue(soure);
                    if (m != null)
                    {
                        val = m.ToString();
                        if (!field.PropertyType.IsGenericType)
                        {
                            if (field.PropertyType.Name.Equals("String"))
                                defaultVal = "";
                            else if (field.PropertyType.Name.Equals("Boolean"))
                            {
                                defaultVal = false;
                                val = (val.Equals("1") || val.Equals("on") || val.Equals("True")).ToString();
                            }
                            else if (field.PropertyType.Name.Equals("Decimal"))
                            {
                                defaultVal = 0M;
                            }
                            else if (field.PropertyType.Name.Equals("Int16"))
                            {
                                defaultVal = Convert.ToInt16(0);
                            }
                            else if (field.PropertyType.Name.Equals("Int32"))
                            {
                                defaultVal = Convert.ToInt32(0);
                            }
                            else if (field.PropertyType.Name.Equals("Int64"))
                            {
                                defaultVal = Convert.ToInt64(0);
                            }
                            else if (field.PropertyType.Name.Equals("Nullable`1"))
                            {
                                defaultVal = null;
                            }
                            else
                            {
                                var type = Nullable.GetUnderlyingType(field.PropertyType);
                                defaultVal = Convert.ChangeType(0, Nullable.GetUnderlyingType(type));
                            }
                        }
                        else
                        {
                            var type = Nullable.GetUnderlyingType(field.PropertyType);
                            if (type.Equals("String"))
                                defaultVal = "";
                            else if (type.Name.Equals("Boolean"))
                            {
                                defaultVal = false;
                                val = (val.Equals("1") || val.Equals("on") || val.Equals("True")).ToString();
                            }
                            else if (type.Name.Equals("Decimal"))
                            {
                                defaultVal = 0M;
                            }
                            else if (type.Name.Equals("Int16"))
                            {
                                defaultVal = Convert.ToInt16(0);
                            }
                            else if (type.Name.Equals("Int32"))
                            {
                                defaultVal = Convert.ToInt32(0);
                            }
                            else if (type.Name.Equals("DateTime"))
                            {
                                defaultVal = null;
                            }
                            else
                            {
                                defaultVal = null;
                            }
                        }
                        if (!field.PropertyType.IsGenericType)
                        {
                            obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
                        }
                        else
                        {
                            Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                var t = Nullable.GetUnderlyingType(field.PropertyType);

                                if (t.Name.Contains("Int"))
                                {
                                    bool isNumeric = Regex.IsMatch(val, @"^\d+$");
                                    if (!isNumeric)
                                    {
                                        switch (val)
                                        {
                                            case "True":
                                            case "true":
                                                val = "1";
                                                break;
                                            case "False":
                                            case "false":
                                                val = "0";
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, t);
                            }
                        }
                        field.SetValue(target, obj, null);
                    }
                }
            }
        }

        /// <summary>
        /// 将object对象转换为实体对象
        /// </summary>
        /// <typeparam name="T">实体对象类名</typeparam>
        /// <param name="asObject">object对象</param>
        /// <returns></returns>
        public static T ConvertObject<T>(object asObject) where T : new()
        {
            //创建实体对象实例
            var t = Activator.CreateInstance<T>();
            if (asObject != null)
            {
                Type type = asObject.GetType();
                //遍历实体对象属性
                foreach (var info in typeof(T).GetProperties())
                {
                    object obj = null;
                    //取得object对象中此属性的值
                    var val = type.GetProperty(info.Name)?.GetValue(asObject);
                    if (val != null)
                    {
                        //非泛型
                        if (!info.PropertyType.IsGenericType)
                            obj = Convert.ChangeType(val, info.PropertyType);
                        else//泛型Nullable<>
                        {
                            Type genericTypeDefinition = info.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                obj = Convert.ChangeType(val, Nullable.GetUnderlyingType(info.PropertyType));
                            }
                            else
                            {
                                obj = Convert.ChangeType(val, info.PropertyType);
                            }
                        }
                        info.SetValue(t, obj, null);
                    }
                }
            }
            return t;
        }
        /// <summary>
        /// 将object对象转换为对象数组
        /// </summary>
        /// <typeparam name="T">实体对象类名</typeparam>
        /// <param name="asObject">object对象</param>
        /// <returns></returns>
        public static List<T> ConvertObjectToList<T>(object asObject) where T : new()
        {
            List<T> result = new List<T>();
            foreach (var item in asObject as IEnumerable<T>)
            {
                //创建实体对象实例
                var t = Activator.CreateInstance<T>();
            
                    Type type = t.GetType();
                    //遍历实体对象属性
                    foreach (var info in typeof(T).GetProperties())
                    {
                        object obj = null;
                        //取得object对象中此属性的值
                        var val = type.GetProperty(info.Name)?.GetValue(item);
                        if (val != null)
                        {
                            //非泛型
                            if (!info.PropertyType.IsGenericType)
                                obj = Convert.ChangeType(val, info.PropertyType);
                            else//泛型Nullable<>
                            {
                                Type genericTypeDefinition = info.PropertyType.GetGenericTypeDefinition();
                                if (genericTypeDefinition == typeof(Nullable<>))
                                {
                                    obj = Convert.ChangeType(val, Nullable.GetUnderlyingType(info.PropertyType));
                                }
                                else
                                {
                                    obj = Convert.ChangeType(val, info.PropertyType);
                                }
                            }
                            info.SetValue(t, obj, null);
                        }
                    }
                
                result.Add(t);
            }
            return result;
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }
}
