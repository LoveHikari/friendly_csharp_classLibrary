using System;
using System.Data;
using System.Reflection;
using System.Text;
using DotNet.Utilities.DBHelper;
using DotNet.Utilities.DBHelper.CrDB;
using DotNet.Utilities.DBHelper.Dapper.Core.SqlClient;
using DotNet.Utilities.DBHelper.Dapper.DataAnnotations;

namespace ConsoleTest01
{
    class Program
    {
        static void Main(string[] args)
        {
           
            System.Console.WriteLine("OK");
            System.Console.ReadKey();

        }

        /// <summary>
        /// 反射调用方法
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="type">参数类型用“,”隔开，例：int,string,string</param>
        /// <param name="parameter">参数列表，用“,”隔开，例：p1,p2,p3</param>
        public static string ReflectionCall(string className, string methodName, string type, string parameter)
        {
            StringBuilder sb = new StringBuilder();

            string[] o = parameter.Split(',');
            object[] param = new object[o.Length];
            string[] s = type.Split(',');
            Type[] paramTypes = new Type[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Trim().ToLower() == "string")
                {
                    paramTypes[i] = typeof(string);
                    param[i] = o[i].Trim();
                }
                else
                {
                    paramTypes[i] = typeof(Int32);
                    param[i] = Convert.ToInt32(o[i].Trim());
                }

            }

            Type[] bllTypes = Assembly.Load("Com.Cos.Bll").GetTypes();  //Com.Cos.Bll下的所有类
            int index = Array.IndexOf(bllTypes, className);
            Type tx = bllTypes[index]; //获得类
            Object im = tx.InvokeMember("Instance", BindingFlags.GetProperty, null, null, null); //在type上调用静态属性，得到类型实例
            MethodInfo mf = tx.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance, null, paramTypes, null);
            //获得方法
            object saf = mf.Invoke(im, param); //调用方法

            Type[] entityTypes = Assembly.Load("Com.Cos.Entity").GetTypes();  //Com.Cos.Entity下的所有类

            if (saf.GetType() == typeof(DataTable))
            {
                DataTable dt = saf as DataTable;
                sb.Append("{");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.AppendFormat("\"{0}\": [", i);
                    sb.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.AppendFormat("\"{0}\": \"{1}\",", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                    }
                    sb.Remove(sb.ToString().Length - 1, 1);
                    sb.Append("}");
                    sb.Append("],");
                }
                sb.Remove(sb.ToString().Length - 1, 1);
                sb.Append("}");
            }
            if (Array.IndexOf(entityTypes, saf.GetType()) != -1)
            {
                sb.Append("{");
                foreach (PropertyInfo propertyInfo in saf.GetType().GetProperties())
                {
                    sb.AppendFormat("\"{0}\": \"{1}\",", propertyInfo.Name, propertyInfo.GetValue(saf));
                }
                sb.Remove(sb.ToString().Length - 1, 1);
                sb.Append("}");
            }
            if (saf is int)
            {
                sb.Append("{");
                sb.AppendFormat("\"vaule\": \"{0}\"", saf);
                sb.Append("}");
            }
            if (saf is bool)
            {
                sb.Append("{");
                sb.AppendFormat("\"status\": \"{0}\"", saf);
                sb.Append("}");
            }
            return sb.ToString();
        }
    }
}
