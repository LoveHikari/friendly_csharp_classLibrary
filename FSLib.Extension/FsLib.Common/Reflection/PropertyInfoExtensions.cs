using System.Globalization;

namespace System.Reflection
{
    /// <summary>
    /// <see cref="PropertyInfo"/> 扩展类
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), System.ComponentModel.Browsable(false)]
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// 设置指定对象的属性值
        /// </summary>
        /// <param name="pi">对象的属性</param>
        /// <param name="t">将设置其属性值的对象</param>
        /// <param name="value">新的属性值</param>
        /// <param name="culture">无用</param>
        /// <remarks>http://www.cnblogs.com/gossip/p/3913428.html</remarks>
        public static void SetValue(this Reflection.PropertyInfo pi, object t, object value, CultureInfo culture)
        {
            if (value != DBNull.Value)
            {
                #region 将列值赋给tect属性

                if (pi.PropertyType.FullName == "System.String")
                {
                    pi.SetValue(t, Convert.ToString(value), null);
                }
                else if (pi.PropertyType.FullName == "System.Int32")
                {
                    pi.SetValue(t, Convert.ToInt32(value), null);
                }
                else if (pi.PropertyType.FullName == "System.Int64")
                {
                    pi.SetValue(t, Convert.ToInt64(value), null);
                }
                else if (pi.PropertyType.FullName == "System.Single")
                {
                    pi.SetValue(t, Convert.ToSingle(value), null);
                }
                else if (pi.PropertyType.FullName == "System.Double")
                {
                    pi.SetValue(t, Convert.ToDouble(value), null);
                }
                else if (pi.PropertyType.FullName == "System.Decimal")
                {
                    pi.SetValue(t, Convert.ToDecimal(value), null);
                }
                else if (pi.PropertyType.FullName == "System.Char")
                {
                    pi.SetValue(t, Convert.ToChar(value), null);
                }
                else if (pi.PropertyType.FullName == "System.Boolean")
                {
                    pi.SetValue(t, Convert.ToBoolean(value), null);
                }
                else if (pi.PropertyType.FullName == "System.DateTime")
                {
                    pi.SetValue(t, Convert.ToDateTime(value), null);
                }
                //可空类型
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToDateTime(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToDateTime(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Int32, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToInt32(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToInt32(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Int64, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToInt64(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToInt64(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Decimal, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToDecimal(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToDecimal(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Boolean, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToBoolean(value), null);
                }
                else if (pi.PropertyType.FullName ==
                         "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"
                )
                {
                    pi.SetValue(t, Convert.ToBoolean(value), null);
                }
                else
                {
                    pi.SetValue(t, null, null);
                    //throw new Exception("属性包含不支持的数据类型!");
                }

                #endregion 将列值赋给tect属性
            }
            else
            {
                pi.SetValue(t, null, null);
            }
        }
    }
}