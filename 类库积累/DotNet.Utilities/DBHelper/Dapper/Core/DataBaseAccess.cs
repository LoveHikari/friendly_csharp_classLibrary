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
        private string _connStr;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnStr { get => _connStr; set => _connStr = value; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public IDbConnection Conn
        {
            get
            {
                SqlConnection conn = new SqlConnection(_connStr);
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
        public int Insert(string sql, object parms = null)
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
        public int Execute(string sql, object parms = null)
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
        public object ExecuteScalar(string sql, object parms = null)
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
        /// <param name="pre"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<TEntity> Query<TEntity>(string sql, Func<TEntity, bool> pre, object parms = null)
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
        public List<TEntity> Query<TEntity>(string sql, object parms = null)
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
        public SqlMapper.GridReader MultyQuery(string sql, object parms = null)
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
        /// <param name="selector"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault<TEntity>(string sql, Func<TEntity, bool> selector, object parms = null)
        {
            using (Conn)
            {
                return Conn.Query<TEntity>(sql, parms).Where(selector).FirstOrDefault();
            }
        }
        /// <summary>
        /// 单个数据库查询
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public DataTable List(string sql, object parms = null)
        {
            DataTable dt = null;
            using (Conn)
            {
                IDataReader reader = Conn.ExecuteReader(sql, parms);
                dt = reader.ToDataTable();
                reader.Dispose();
            }
            return dt;
        }
    }
}