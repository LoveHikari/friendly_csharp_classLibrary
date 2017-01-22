/******************************************************************************************************************
 * 
 * 
 * 标  题： Object 帮助类(版本：Version1.0.0)
 * 作  者： YuXiaoWei
 * 日  期： 2016/11/23
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
    /// Object 帮助类
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 获取字符串 不返回null值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString2(this object value)
        {
            if (value == null || value == System.DBNull.Value)
                return string.Empty;
            return value.ToString();
        }
        /// <summary>
        /// 获取字符串 不返回null值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="s">指定为null时返回的值</param>
        /// <returns></returns>
        public static string ToString2(this object value, string s)
        {
            if (value == null || value == System.DBNull.Value)
                return s;
            return value.ToString();
        }
        /// <summary>
        /// 转换为等效的32位有符号整数，转换失败返回0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt32(this object input)
        {
            int result;
            if (input == null || input == System.DBNull.Value)
                return 0;
            if (int.TryParse(input.ToString(), out result))
                return result;
            return 0;

        }
        /// <summary>
        /// 转换为等效的32位有符号整数，转换失败返回指定的数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="i">指定转换失败时返回的值</param>
        /// <returns></returns>
        public static int ToInt32(this object input, int i)
        {
            int result;
            if (input == null || input == System.DBNull.Value)
                return 0;
            if (int.TryParse(input.ToString(), out result))
                return result;
            return i;
        }
    }
}
