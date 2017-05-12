using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
/******************************************************************************************************************
 * 
 * 
 * 标  题： DataTable 扩展类(版本：Version1.0.0)
 * 作  者： YuXiaoWei
 * 日  期： 2017/05/12
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
    /// DataTable 扩展类
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// DataTable转list
        /// </summary>
        /// <typeparam name="T">转化类型</typeparam>
        /// <param name="dt">DataTable数据</param>
        /// <returns>模型列表</returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class ,new()
        {
            // 定义集合 
            List<T> ts = new List<T>();

            //遍历DataTable中所有的数据行 
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    string tempName = pi.Name;//将属性名称赋值给临时变量 
                    //检查DataTable是否包含此列（列名==对象的属性名）  
                    if (dt.Columns.Contains(tempName))
                    {
                        //取值 
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性 
                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType.FullName.IndexOf(typeof(Int32).FullName,StringComparison.CurrentCultureIgnoreCase) > -1)  //如果类型是int
                            {
                                pi.SetValue(t, value.ToInt32(), null);
                            }
                            else
                            {
                                pi.SetValue(t, value, null);
                            }
                            
                        }
                    }
                }
                //对象添加到泛型集合中 
                ts.Add(t);
            }
            return ts;

        }
    }
}