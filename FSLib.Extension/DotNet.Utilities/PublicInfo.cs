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
namespace DotNet.Utilities
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

    }
}