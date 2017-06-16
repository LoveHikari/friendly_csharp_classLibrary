using System.Collections;
using System.Collections.Generic;

namespace System.Collection
{
    /// <summary>
    /// <see cref="List{T}"/> 帮助类
    /// </summary>
    public class ListHelper
    {
        /// <summary>
        /// 转换<see cref="List{T}"/> 内部类型的类型
        /// </summary>
        /// <typeparam name="T">转换前的类型</typeparam>
        /// <typeparam name="TK">装换后的类型</typeparam>
        /// <param name="list">需要转换的集合</param>
        /// <returns>转换后的集合</returns>
        public static List<TK> ChangeType<T, TK>(List<T> list)
        {

            List<TK> os = new List<TK>();
            foreach (var i in list)
            {
                os.Add((TK)Convert.ChangeType(i, typeof(TK)));
            }
            return os;
        }
    }

    /// <summary>
    /// <see cref="IListExtensions"/> 扩展类
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), System.ComponentModel.Browsable(false)]
    public static class IListExtensions
    {
        /// <summary>
        /// 对<see cref="List{T}"/> 的每个元素执行指定操作，并返回<see cref="List{T}"/>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="lists">需要操作的集合</param>
        /// <param name="func">需要的操作</param>
        /// <returns>操作后的<see cref="List{T}"/></returns>
        public static List<T> ForEach<T>(this IList<T> lists, Func<T, T> func)
        {
            //List<T> lists = (List<T>) ls;
            List<T> tempList = new List<T>(lists);

            Action<List<T>, T, int> action = (list, s, j) =>
             {
                 list[j] = func(s);
             };
            int i = 0;
            foreach (var s in lists)
            {
                action(tempList, s, i);
                i++;
            }
            return tempList;
        }

        
    }
}