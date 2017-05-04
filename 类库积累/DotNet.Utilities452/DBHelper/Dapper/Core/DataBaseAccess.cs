using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DotNet.Utilities.DBHelper.Dapper.Core
{
    /// <summary>
    /// 数据库访问类
    /// </summary>
    public class DataBaseAccess
    {
        public static IDbConnection Conn
        {
            get
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                return conn;
            }
        }

        /// <summary>
        /// 执行增加的方法
        /// </summary>
        /// <param name="sql">带参数的sql语句</param>
        /// <param name="parms">实体</param>
        /// <returns>id</returns>
        public static int Insert(string sql, object parms = null)
        {
            int id = 0;
            using (Conn)
            {
                object obj = Conn.ExecuteScalar(sql, parms);
                id = obj == null ? 0 : Convert.ToInt32(obj);
            }
            return id;
        }

        /// <summary>
        /// 得到受影响行数
        /// </summary>
        /// <param name="sql">带参数的sql语句</param>
        /// <param name="parms">实体</param>
        /// <returns></returns>
        public static int Execute(string sql, object parms = null)
        {
            using (Conn)
            {
                int i = Conn.Execute(sql, parms);
                return i;
            }
        }

        /// <summary>
        /// 得到单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, object parms = null)
        {
            using (Conn)
            {
                return Conn.ExecuteScalar(sql, parms);
            }
        }

        /// <summary>
        /// 单个数据集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TEntity> Query<TEntity>(string sql, Func<TEntity, bool> pre, object parms = null)
        {
            using (Conn)
            {
                return Conn.Query<TEntity>(sql, parms).Where(pre).ToList();
            }
        }

        /// <summary>
        /// 单个数据集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TEntity> Query<TEntity>(string sql, object parms = null)
        {
            using (Conn)
            {
                return Conn.Query<TEntity>(sql, parms).ToList();
            }
        }

        /// <summary>
        /// 多个数据集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static SqlMapper.GridReader MultyQuery(string sql, object parms = null)
        {
            using (Conn)
            {
                return Conn.QueryMultiple(sql, parms);
            }
        }

        /// <summary>
        /// 单个数据集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static TEntity FirstOrDefault<TEntity>(string sql, Func<TEntity, bool> selector, object parms = null)
        {
            using (Conn)
            {
                return Conn.Query<TEntity>(sql, parms).Where(selector).FirstOrDefault();
            }
        }
    }
}