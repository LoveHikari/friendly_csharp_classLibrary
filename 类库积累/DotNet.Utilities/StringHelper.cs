using System;
using System.Globalization;
using System.Text.RegularExpressions;

/******************************************************************************************************************
 * 
 * 
 * 标  题： string 帮助类(版本：Version1.0.0)
 * 作  者： YuXiaoWei
 * 日  期： 2016/11/18
 * 修  改：
 * 参  考： 
 * 说  明： 暂无...
 * 备  注： 暂无...
 * 调用示列：
 *
 * 
 * ***************************************************************************************************************/
namespace DotNet.Utilities
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 查找两个字符串最长的公共子串(LongestCommonSequence)
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static string Lcs(string value1, string value2)
        {
            if (string.IsNullOrEmpty(value1)
                || string.IsNullOrEmpty(value2))
            {
                return "";
            }
            int[,] d = new int[value1.Length, value2.Length];
            int index = 0;
            int length = 0;

            for (int i = 0; i < value1.Length; i++)
            {
                for (int j = 0; j < value2.Length; j++)
                {
                    // 左上角值
                    int n = i - 1 >= 0 && j - 1 >= 0 ? d[i - 1, j - 1] : 0;
                    // 当前节点值 = "1 + 左上角值" : "0"
                    d[i, j] = value1[i] == value2[j] ? 1 + n : 0;
                    // 如果是最大值，则记录该值和行号
                    if (d[i, j] > length)
                    {
                        length = d[i, j];
                        index = i;
                    }
                }
            }
            return value1.Substring(index - length + 1, length);
        }

    }
    /// <summary>
    /// string 扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 验证字符串个数是否超出指定长度
        /// </summary>
        /// <param name="inputstr">被检验的字符串</param>
        /// <param name="count">指定的长度</param>
        /// <returns></returns>
        public static bool ValidateCharLength(this string inputstr, int count)
        {
            bool flag = false;
            int tempLen = 0;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();

            byte[] tempstr = ascii.GetBytes(inputstr);
            for (int i = 0; i < tempstr.Length; i++)
            {
                if (tempstr[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            if (tempLen <= count)
                flag = true;

            return flag;
        }
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="inputString">被处理的字符串</param>
        /// <param name="len">要求截取的长度</param>
        /// <param name="flag">截取后是否显示省略号,flag为true显示…，为false不显示，默认不显示</param>
        /// <returns></returns>
        public static string CutString(this string inputString, int len, bool flag = false) //对inputString截取len字节的字符
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string outputString = string.Empty;
            byte[] str = ascii.GetBytes(inputString);
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
                try
                {
                    outputString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (tempLen >= len)
                    break;
            }

            //如果截过则加上半个省略号
            if (inputString != outputString && flag)
                outputString += "…";
            return outputString;
        }

        /// <summary>
        /// 过滤Sql查询关键词中的敏感词汇
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SqlFilter(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            Regex regsql = new Regex(@"0x([0-9a-fA-F]{4})+|(%[0-9a-fA-F]{2})+|--|@@|count|asc|mid|char|chr|sysobjects|sys.|select|insert|delete|update|drop|truncate|xp_cmdshell|netlocalgroup|administrator|net user|exec|master|declare|localgroup|remove|create|extended_properties|objects|columns|types|extended|comments|table|cast", RegexOptions.IgnoreCase);

            value = value.Replace("'", "''").Trim();
            //捕获的字符转换为""
            return regsql.Replace(value, m => string.Empty);
        }
        /// <summary>
        /// 基于字符串将字符串拆分为多个子字符串。可以指定子字符串是否包含空数组元素。
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="separator">分隔此字符串中子字符串的字符串数组、不包含分隔符的空数组或 null。</param>
        /// <param name="options">要省略返回的数组中的空数组元素，则为 StringSplitOptions.RemoveEmptyEntries；要包含返回的数组中的空数组元素，则为 StringSplitOptions.None。</param>
        /// <returns>一个数组，其元素包含此字符串中的子字符串，这些子字符串由 separator 中的一个或多个字符串分隔。 有关详细信息，请参阅“备注”部分。</returns>
        public static string[] Split(this string value, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return value.Split(new string[] { separator }, options);
        }
        /// <summary>
        /// 获取左边指定位数的字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string LastLeft(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            if (value.Length <= length)
            {
                return value;
            }
            return value.Substring(0, length);
        }
        /// <summary>
        /// 获取右边指定位数的字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string LastRight(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            if (value.Length <= length)
            {
                return value;
            }

            return value.Substring(value.Length - length);
        }
        /// <summary>
        /// 获取拆分符左边的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split">拆分符</param>
        /// <returns></returns>
        public static string SplitLeft(this string value, string split)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            int index = value.IndexOf(split, StringComparison.Ordinal);
            if (index > 0)
            {
                return value.Substring(0, index);
            }
            else
            {
                return value;
            }
        }
        /// <summary>
        /// 获取拆分符右边的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split">拆分符</param>
        /// <returns></returns>
        public static string SplitRight(this string value, string split)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            int index = value.IndexOf(split, StringComparison.Ordinal);
            if (index > 0)
            {
                return value.Substring(index + split.Length);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            Regex reg = new Regex(@"[\f\n\r\t\v]*", RegexOptions.IgnoreCase);
            value = reg.Replace(value, "");
            reg = new Regex(@"[ ]+");//合并多个空格为一个
            return reg.Replace(value, " ");
        }

        /// <summary>
        /// 过滤文本中的空行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveEmptyRow(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            Regex reg = new Regex(@"\n[\t|\s| ]*\r", RegexOptions.IgnoreCase);
            return reg.Replace(value, "");
        }

        /// <summary>
        /// 获取字符串在数组中累计出现的次数
        /// </summary>
        /// <param name="stringArray"></param>
        /// <param name="findString"></param>
        /// <returns></returns>
        public static int GetStringCount(this string[] stringArray, string findString)
        {
            string totalString = string.Join("", stringArray);
            return GetStringCount(totalString, findString);
        }

        /// <summary>
        /// 获取字符串在字符串累计出现的次数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="findString"></param>
        /// <returns></returns>
        public static int GetStringCount(this string value, string findString)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            int count = 0;
            int findStringLength = findString.Length;
            string subString = value;

            while (subString.IndexOf(findString, StringComparison.Ordinal) >= 0)
            {
                subString = subString.Substring(subString.IndexOf(findString, StringComparison.Ordinal) + findStringLength);
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 截取从startString开始到结尾的字符
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startString"></param>
        /// <returns></returns>
        public static string GetSubString(this string value, string startString)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            int index = value.IndexOf(startString, StringComparison.Ordinal);
            if (index == -1) return "";
            index += startString.Length;
            if (index > 0)
            {
                return value.Substring(index);
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 截取从startString开始到endString的字符
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <returns></returns>
        public static string GetSubString(this string value, string startString, string endString)
        {
            value = GetSubString(value, startString);
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            int length = value.IndexOf(endString, StringComparison.Ordinal);
            if (length > 0)
            {
                return value.Substring(0, length);
            }
            else
            {
                return "";
            }
        }
        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSbc(this string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }


        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDbc(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 类型转换
        
        /// <summary>
        /// 提取出所有数字，并转换为int，失败返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToNumber(this string value)
        {
            if (value == null)
                return 0;
            string num = "";
            foreach (char item in value)
            {
                if (item >= 48 && item <= 58)
                {
                    num += item;
                }
            }
            int result;
            if (int.TryParse(num, out result))
                return result;
            return 0;
        }
        /// <summary>
        /// 转换成时间类型，失败则得到最小时间
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime result;
            if (DateTime.TryParse(s, out result))
            {
                return result;
            }
            return DateTime.MinValue;
        }
        /// <summary>
        /// 转换成时间类型，失败则得到最小时间
        /// </summary>
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, IFormatProvider provider)
        {
            DateTime result;
            if (DateTime.TryParse(s, provider, DateTimeStyles.None, out result))
            {
                return result;
            }
            return DateTime.MinValue;
        }
        
        #endregion

    }
}