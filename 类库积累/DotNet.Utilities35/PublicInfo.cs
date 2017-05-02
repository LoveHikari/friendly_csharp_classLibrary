using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml;

/******************************************************************************************************************
 * 
 * 
 * 标  题：公共工具类(版本：Version1.0.0)
 * 作  者：YuXiaoWei
 * 日  期：2016/10/27
 * 修  改：
 * 参  考： http://www.cnblogs.com/huangfr/archive/2012/03/27/2420464.html
 * 说  明： 暂无...
 * 备  注： 暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace DotNet.Utilities35
{
    /// <summary>
    /// 公共工具类
    /// </summary>
    public class PublicInfo
    {
        /// <summary>
        /// 获取经纬度
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="lng">经度</param>
        /// <param name="lat">纬度</param>
        public static void GetLocation(string address, out string lng, out string lat)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("http://api.map.baidu.com/geocoder?address=" + address);
            DataTable dt = ConvertHelper.XmlToDataSet(doc.InnerXml).Tables["location"];
            if (dt != null && dt.Rows.Count > 0)
            {
                lng = dt.Rows[0]["lng"].ToString();
                lat = dt.Rows[0]["lat"].ToString();
            }
            else
            {
                lng = "";
                lat = "";
            }
        }

        #region 求最大公约数
        /// <summary>
        /// 辗转相除法求最大公约数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GetGreatestCommonDivisor3(int a, int b)
        {
            int result = 1;
            if (a > b)
                result = Gcd(a, b);
            else
                result = Gcd(b, a);
            return result;
        }
        private static int Gcd(int a, int b)
        {
            if (a % b == 0)
                return b;
            else
                return Gcd(b, a % b);
        }
        /// <summary>
        /// 更相减损术求最大公约数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GetGreatestCommonDivisor2(int a, int b)
        {
            if (a == b)
                return a;
            if (a < b)
                return GetGreatestCommonDivisor2(b - a, a);
            else
                return GetGreatestCommonDivisor2(a - b, b);
        }
        /// <summary>
        /// 更相减损术与移位结合求最大公约数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>参考：http://blog.jobbole.com/106315/?utm_source=blog.jobbole.com&amp;utm_medium=relatedPosts </remarks>
        public static int GetGreatestCommonDivisor(int a, int b)
        {
            if (a >> 1 == 0 || b >> 1 == 0) return 1;
            if (a == b)
                return a;
            if (a < b)
                return GetGreatestCommonDivisor(b, a); //保证参数a永远大于参数b，为减少代码量
            else
            {
                if ((a & 1) == 0 && (b & 1) == 0)
                    return GetGreatestCommonDivisor(a >> 1, b >> 1) << 1;
                else if ((a & 1) == 0 && (b & 1) != 0)
                    return GetGreatestCommonDivisor(a >> 1, b);
                else if ((a & 1) != 0 && (b & 1) == 0)
                    return GetGreatestCommonDivisor(a, b >> 1);
                else
                    return GetGreatestCommonDivisor(b, a - b);

            }
        }


        #endregion
        /// <summary>
        /// 禁止程序重复打开
        /// <remarks>http://blog.csdn.net/lc547913923/article/details/46557777</remarks>
        /// </summary>
        public static void OnStartup()
        {
            Process[] pro = Process.GetProcesses();
            int n = pro.Count(p => p.ProcessName.Equals(System.Reflection.Assembly.GetEntryAssembly().GetName().Name));
            if (n > 1)
            {
                Environment.Exit(Environment.ExitCode);
                //System.Windows.Application.Current.Shutdown();
                return;
            }
        }
    }
}