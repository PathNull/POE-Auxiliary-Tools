using AutoHotkey.Interop;
using Path_of_Exile_Tool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public static class Core
    {
        /// <summary>
        /// 获取剪贴板数据
        /// </summary>
        /// <returns></returns>
        public static string ClipboardData()
        {
            IDataObject data = Clipboard.GetDataObject();
            string test = (string)data.GetData(DataFormats.Text);
            return test;
        }
        /// <summary>
        /// 获取剪切板数据并转换
        /// </summary>
        public static List<string[]> ShearPlate()
        {
            List<string[]> result = new List<string[]>();
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Text))
            {
                string test = (string)data.GetData(DataFormats.Text);
                var list = test.Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in list)
                {
                    result.Add(item.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取剪切板数据并转换
        /// </summary>
        public static List<string[]> Shear_Plate()
        {
            List<string[]> result = new List<string[]>();

            var list = Clipboard.GetText().Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Delete(list);
            foreach (var item in list)
            {
                var a = item.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (item!="")
                {
                    result.Add(a);
                }
            
            }

            return result;
        }
        /// <summary>
        /// 判断是否存在词缀
        /// </summary>
        /// <returns></returns>
        public static bool IsExit(List<string[]> result, string affix,int index)
        {
            bool isExist = false;
            //比对是否存在该词缀
          
                foreach (var item in result[index])
                {
                    isExist = Regex.IsMatch(item, affix);
                    if (isExist)
                    {
                        break;
                    }
                }
            return isExist;
        }
        /// <summary>
        /// 复制操作
        /// </summary>
        public static string Copy(AutoHotkeyEngine ahk,string lastClipboardData,int max)
        {
            max--;
            bool end = false;
            var lastList = lastClipboardData.Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ahk.ExecFunction("Copy"); //执行复制操作
            var clipboardData = Clipboard.GetText();
            var currList = clipboardData.Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (lastList.Count == currList.Count)
            {
                for (int i = 0; i < lastList.Count; i++)
                {
                    if (lastList[i] != currList[i])
                    {
                        end = true;
                    }
                }
            }
            else
            {
                end = true;
            }
            if (!end && max > 0)
            {
                //System.Threading.Thread.Sleep(1000);   
                return Copy(ahk, lastClipboardData, max);
            }
            else
            {
                return clipboardData;
            }
            return clipboardData;
        }
        /// <summary>
        /// 获取词缀的下标（洗一次装备，排除发生变化的非词缀，发生变化的那个就是词缀）
        /// </summary>
        /// <returns></returns>
        public static int GetAffixIndex(AutoHotkeyEngine ahk, string lastClipboardData,int max)
        {
            max--;
            Copy(ahk, lastClipboardData, 5);
            List<string> baseArray = lastClipboardData.Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Delete(baseArray);
            List<string> currArray = Clipboard.GetText().Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Delete(currArray);
            for (int i = 0; i < baseArray.Count; i++)
            {
                bool skip = false;
                if (baseArray[i] != currArray[i])
                {
                    Exclude exc = new Exclude();
                    foreach (var item in exc.List)
                    {
                        if (baseArray[i].IndexOf(item) > -1)
                        {
                            skip = true;
                        }
                    }
                    if (!skip)
                    {
                        return i;
                    }
                }
            }
            if (max <= 0)
            {
                return 0;
            }
            else
            {
                return GetAffixIndex(ahk, lastClipboardData, max);
            }
            return 0;
        }
        /// <summary>
        /// 移除装备信息中的会变化但不是词缀的某些项（如：星团的需求等级）
        /// </summary>
        public static void Delete(List<string> list)
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IndexOf("\r\n需求") > -1)
                {
                    indexList.Add(i);
                }
            }
            foreach (var item in indexList)
            {
                list.Remove(list[item]);
            }
        }
    }
}
