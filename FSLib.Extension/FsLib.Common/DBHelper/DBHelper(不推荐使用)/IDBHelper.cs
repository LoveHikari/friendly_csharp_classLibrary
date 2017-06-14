using System.Collections.Generic;
using System.Data;

namespace Common.DBHelper
{
    /// <summary>
    /// 数据库连接接口
    /// </summary>
    interface IDBHelper
    {

        string LogPath
        {
            get;
            set;
        }

        string DbName
        {
            get;
            set;
        }
        string ConnStr
        {
            get;
            set;
        }
        /// <summary>
        /// 更新SQL,返回受影响行数，用于执行insert、update、delete等非查询语句。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecSqlNonQuery(string sql);
        /// <summary>
        /// 更新参数化的SQL，用于执行insert、update、delete等非查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist">参数列表</param>
        /// <returns></returns>
        int ExecSqlNonQuery(string sql, List<DBParam> dbparamlist);
        /// <summary>
        /// 查询SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable ExecSqlDataTable(string sql);
        /// <summary>
        /// 查询SQL，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        DataTable ExecSqlDataTable(string sql, List<DBParam> dbparamlist);
        /// <summary>
        /// 执行Sql语句，返回DataSet。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet ExecSqlDataSet(string sql);
        /// <summary>
        /// 查询SQL，带参数，返回DataSet。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        DataSet ExecSqlDataSet(string sql, List<DBParam> dbparamlist);
        /// <summary>
        /// 查询SQL，并指定表名，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        DataSet ExecSqlDataSet(string sql, List<DBParam> dbparamlist, string tablename);
        /// <summary>
        /// 查询SQL，返回第一行第一列字段
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecSqlScalar(string sql);
        /// <summary>
        /// 查询SQL，返回第一行第一列字段，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        object ExecSqlScalar(string sql, List<DBParam> dbparamlist);
        /// <summary>
        /// 查询SQL，返回DataReader，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        System.Data.SqlClient.SqlDataReader ExecSqlReader(string sql, List<DBParam> dbparamlist);
        System.Data.OleDb.OleDbDataReader ExecOleDbDataReader(string sql, List<DBParam> dbparamlist);
        MySql.Data.MySqlClient.MySqlDataReader ExecMySqlDataReader(string sql, List<DBParam> dbparamlist);

    }
}