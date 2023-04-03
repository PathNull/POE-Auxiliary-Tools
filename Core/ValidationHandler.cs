using System;
using System.Text.RegularExpressions;

namespace CSRTMISYC.Core
{
    public static class ValidationHandler
    {
       
        /// <summary>
        /// 验证字符串是否是电子邮件地址
        /// </summary>
        /// <param name="str"></param>

        public static bool EmailVaildation(string str)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
         RegexOptions.CultureInvariant | RegexOptions.Singleline);
            bool isValidEmail = regex.IsMatch(str);
            if (isValidEmail)
            {
                return true;//有效
            }
            else
            {
                return false;//无效
            }
        }
        /// <summary>
        /// 验证字符串是否是身份证
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ValidateIDCard(string id)
        {
            // 18位身份证正则表达式
            string pattern = @"^\d{17}(\d|X)$";

            if (!Regex.IsMatch(id, pattern))
            {
                return false;
            }

            // 省份代码
            string[] provinces = {
        "11", "12", "13", "14", "15", "21", "22", "23", "31", "32",
        "33", "34", "35", "36", "37", "41", "42", "43", "44", "45",
        "46", "50", "51", "52", "53", "54", "61", "62", "63", "64",
        "65", "71", "81", "82"
    };

            // 验证省份代码是否正确
            bool isProvinceCodeValid = false;
            for (int i = 0; i < provinces.Length; i++)
            {
                if (id.Substring(0, 2) == provinces[i])
                {
                    isProvinceCodeValid = true;
                    break;
                }
            }
            if (!isProvinceCodeValid)
            {
                return false;
            }

            // 验证生日是否合法
            int year, month, day;
            if (id.Length == 18)
            {
                year = int.Parse(id.Substring(6, 4));
                month = int.Parse(id.Substring(10, 2));
                day = int.Parse(id.Substring(12, 2));
            }
            else
            {
                year = int.Parse("19" + id.Substring(6, 2));
                month = int.Parse(id.Substring(8, 2));
                day = int.Parse(id.Substring(10, 2));
            }
            try
            {
                DateTime date = new DateTime(year, month, day);
                if (date > DateTime.Now || date < new DateTime(1800, 1, 1))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            // 验证校验码
            if (id.Length == 18)
            {
                int[] weights = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                char[] codes = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(id[i].ToString()) * weights[i];
                }
                if (id[17] != codes[sum % 11])
                {
                    return false;
                }
            }

            return true;
        }
    }


}
