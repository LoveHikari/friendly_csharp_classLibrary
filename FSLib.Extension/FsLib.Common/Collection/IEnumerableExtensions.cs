using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

namespace System.Collection
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> 扩展类
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), System.ComponentModel.Browsable(false)]
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 确认序列中是否包含指定元素
        /// </summary>
        /// <param name="source">需要确认的序列</param>
        /// <param name="value">要寻找的值</param>
        /// <param name="sc">是否忽略大小写</param>
        /// <returns></returns>
        public static bool Contains(this IEnumerable<string> source, string value, StringComparison sc)
        {
            if (sc == StringComparison.CurrentCultureIgnoreCase || sc == StringComparison.InvariantCultureIgnoreCase || sc == StringComparison.OrdinalIgnoreCase)
            {
                CompareOnly c = new CompareOnly();
                return source.Contains(value, c);
            }
            else
            {
                return source.Contains(value);
            }

        }

    }





    /// <summary>
    /// 比较器
    /// </summary>
    class CompareOnly : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.ToUpper() == y.ToUpper();
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}