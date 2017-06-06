using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Threading;

/******************************************************************************************************************
* 
* 
* 标  题：sqlite数据库操作类(版本：Version1.0.0)
* 作  者：YuXiaoWei
* 日  期：2016/10/24
* 修  改：
* 参  考： 
* 说  明： 暂无...
* 备  注： 配置节添加示例：<connectionStrings><add name="ConnString" connectionString="Data Source=E:\App_Data\data.db;Pooling=true;FailIfMissing=false;" /></connectionStrings>
* 
* 
* ***************************************************************************************************************/
namespace DotNet.Utilities.DBHelper.DataAccess
{
    /// <summary>
    /// sqlite数据库操作类
    /// </summary>
    public sealed class SqliteHelper
    {
        #region Field

        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        private static string ConnString = "";
        private bool isCommon;
        private SQLiteCommand command;
        /// <summary>
        /// 公共实例
        /// </summary>
        public static readonly SqliteHelper Instance = new SqliteHelper(true);//公共实例
        private static readonly object obj = new object();
        #endregion

        #region Constructor

        private SqliteHelper(bool isCommon)
        {
            ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;//数据库连接字符串
            if (!isCommon)
            {
                this.command = new SQLiteCommand();
            }
            this.isCommon = isCommon;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connString">config文件中的配置节名称</param>
        public SqliteHelper(string connString)
        {
            ConnString = connString;
            this.isCommon = true;
        }
        #endregion

        #region NonQuery

        /// <summary>
        /// 执行Sql语句，返回受影响的行数。用于执行insert、update、delete等非查询语句。
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        public int ExecSqlNonQuery(string sql)
        {
            return InnerExecNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 执行参数化的Sql语句，返回受影响的行数。用于执行insert、update、delete等非查询语句。
        /// </summary>
        /// <param name="sql">要执行的包含参数的sql语句</param>
        /// <param name="values">Sql语句中表示参数的SqlParameter对象</param>
        public int ExecSqlNonQuery(string sql, params SQLiteParameter[] values)
        {
            return InnerExecNonQuery(sql, CommandType.Text, values);
        }

        /// <summary>
        /// 执行存储过程，返回受影响的行数。用于执行insert、update、delete等非查询语句。
        /// </summary>
        /// <param name="procName">要执行的存储过程的名字</param>
        /// <param name="parameters">存储过程的参数</param>
        public int ExecProcNonQuery(string procName, params object[] parameters)
        {
            return InnerExecNonQuery(procName, CommandType.StoredProcedure, parameters);
        }

        #endregion

        #region Scalar

        /// <summary>
        /// 执行Sql语句，返回查询结果集中的第一行第一列的值。
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        public object ExecSqlScalar(string sql)
        {
            return InnerExecScalar(sql, CommandType.Text);
        }

        /// <summary>
        /// 执行参数化的Sql语句，返回查询结果集中的第一行第一列的值。
        /// </summary>
        /// <param name="sql">要执行的包含参数的sql语句</param>
        /// <param name="values">Sql语句中表示参数的SqlParameter对象</param>
        public object ExecSqlScalar(string sql, params SQLiteParameter[] values)
        {
            return InnerExecScalar(sql, CommandType.Text, values);
        }

        /// <summary>
        /// 执行存储过程，返回查询结果集中的第一行第一列的值。
        /// </summary>
        /// <param name="procName">要执行的存储过程的名字</param>
        /// <param name="parameters">存储过程的参数</param>
        public object ExecProcScalar(string procName, params object[] parameters)
        {
            return InnerExecScalar(procName, CommandType.StoredProcedure, parameters);
        }

        #endregion

        #region SqlDataReader

        /// <summary>
        /// 执行Sql语句，获取SqlDataReader。
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="block">要使用SqlDataReader的代码块（方法）/委托/Lambda语句块</param>
        public void ExecSqlReader(string sql, Action<SQLiteDataReader> block)
        {
            InnerExecReader(sql, CommandType.Text, block);
        }

        /// <summary>
        /// 执行参数化Sql语句，获取SqlDataReader。
        /// </summary>
        /// <param name="sql">要执行的包含参数的sql语句</param>
        /// <param name="block">要使用SqlDataReader的代码块（方法）/委托/Lambda语句块</param>
        /// <param name="values">Sql语句中表示参数的SqlParameter对象</param>
        public void ExecSqlReader(string sql, Action<SQLiteDataReader> block, params SQLiteParameter[] values)
        {
            InnerExecReader(sql, CommandType.Text, block, values);
        }

        /// <summary>
        /// 执行存储过程，获取SqlDataReader。 
        /// </summary>
        /// <param name="procName">要执行的存储过程的名字</param>
        /// <param name="block">要使用SqlDataReader的代码块（方法）/委托/Lambda语句块</param>
        /// <param name="parameters">存储过程的参数</param>
        public void ExecProcReader(string procName, Action<SQLiteDataReader> block, params object[] parameters)
        {
            InnerExecReader(procName, CommandType.StoredProcedure, block, parameters);
        }

        #endregion

        #region List

        /// <summary>
        /// 执行Sql语句，返回强类型T的List列表。
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="selector">用于生成列表中类型为T的元素的方法/委托/Lambda表达式等</param>
        public List<T> ExecSqlList<T>(string sql, Func<SQLiteDataReader, T> selector)
        {
            return InnerExecList<T>(sql, CommandType.Text, selector);
        }

        /// <summary>
        /// 执行参数化Sql语句，返回强类型T的List列表。 
        /// </summary>
        /// <param name="sql">要执行的包含参数的sql语句</param>
        /// <param name="selector">用于生成列表中类型为T的元素的方法/委托/Lambda表达式等</param>
        /// <param name="values">Sql语句中表示参数的SqlParameter对象</param>
        public List<T> ExecSqlList<T>(string sql, Func<SQLiteDataReader, T> selector, params SQLiteParameter[] values)
        {
            return InnerExecList<T>(sql, CommandType.Text, selector, values);
        }

        /// <summary>
        /// 执行存储过程，返回强类型T的List列表。 
        /// </summary>
        /// <param name="procName">要执行的存储过程的名字</param>
        /// <param name="selector">用于生成列表中类型为T的元素的方法/委托/Lambda表达式等</param>
        /// <param name="parameters">存储过程的参数</param>
        public List<T> ExecProcList<T>(string procName, Func<SQLiteDataReader, T> selector, params object[] parameters)
        {
            return InnerExecList<T>(procName, CommandType.StoredProcedure, selector, parameters);
        }

        #endregion

        #region DataSet

        /// <summary>
        /// 执行Sql语句，返回DataSet。
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        public DataSet ExecSqlDataSet(string sql)
        {
            return InnerExecDataSet(sql, CommandType.Text);
        }

        /// <summary>
        /// 执行参数化Sql语句，返回DataSet。 
        /// </summary>
        /// <param name="sql">要执行的包含参数的sql语句</param>
        /// <param name="values">Sql语句中表示参数的SqlParameter对象</param>
        public DataSet ExecSqlDataSet(string sql, params SQLiteParameter[] values)
        {
            return InnerExecDataSet(sql, CommandType.Text, values);
        }

        /// <summary>
        /// 执行存储过程，返回DataSet。 
        /// </summary>
        /// <param name="procName">要执行的存储过程的名字</param>
        /// <param name="parameters">存储过程的参数</param>
        public DataSet ExecProcDataSet(string procName, params object[] parameters)
        {
            return InnerExecDataSet(procName, CommandType.StoredProcedure, parameters);
        }

        #endregion

        #region DataTable

        /// <summary>
        /// 执行Sql语句，返回DataTable。
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        public DataTable ExecSqlDataTable(string sql)
        {
            return this.ExecSqlDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 执行参数化Sql语句，返回DataTable。
        /// </summary>
        /// <param name="sql">要执行的包含参数的sql语句</param>
        /// <param name="values">Sql语句中表示参数的SqlParameter对象</param>
        public DataTable ExecSqlDataTable(string sql, params SQLiteParameter[] values)
        {
            return this.ExecSqlDataSet(sql, values).Tables[0];
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SQLiteDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SQLiteConnection connection = new SQLiteConnection(ConnString);
            SQLiteDataReader returnReader;
            connection.Open();
            SQLiteCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;

        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SQLiteDataAdapter sqlDA = new SQLiteDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SQLiteDataAdapter sqlDA = new SQLiteDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        ///// <summary>
        ///// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        ///// </summary>
        ///// <param name="connection">数据库连接</param>
        ///// <param name="storedProcName">存储过程名</param>
        ///// <param name="parameters">存储过程参数</param>
        ///// <returns>SqlCommand</returns>
        private static SQLiteCommand BuildQueryCommand(SQLiteConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SQLiteCommand command = new SQLiteCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SQLiteParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }


        /// <summary>
        /// 执行存储过程，返回DataTable。
        /// </summary>
        /// <param name="procName">要执行的存储过程的名字</param>
        /// <param name="parameters">存储过程的参数</param>
        public DataTable ExecProcDataTable(string procName, params object[] parameters)
        {
            return this.ExecProcDataSet(procName, parameters).Tables[0];
        }

        #endregion

        #region Batch & Transaction

        /// <summary>
        /// 执行批量Sql语句。
        /// </summary>
        /// <param name="batch">要执行的批量Sql代码块（方法）/委托/Lambda语句块</param>
        public void ExecBatch(Action<SqliteHelper> batch)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnString))
            {
                SqliteHelper actuator = new SqliteHelper(false);
                actuator.command.Connection = con;
                try
                {
                    con.Open();
                    batch(actuator);
                }
                catch (SQLiteException)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 执行事务代码。 
        /// </summary>
        /// <param name="block">要执行的事务代码块（方法）/委托/Lambda语句块</param>
        public void ExecTransaction(Action<SqliteHelper, SQLiteTransaction> block)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnString))
            {
                SqliteHelper actuator = new SqliteHelper(false);
                actuator.command.Connection = con;
                try
                {
                    con.Open();
                    SQLiteTransaction tran = con.BeginTransaction();
                    actuator.command.Transaction = tran;
                    block(actuator, tran);
                }
                catch (SQLiteException)
                {
                    throw;
                }
            }
        }

        #endregion

        #region Private Method

        private int InnerExecNonQuery(string text, CommandType type, params object[] values)
        {
            return Convert.ToInt32(InnerExecCommand<object>(text, type, "NonQuery", null, null, values));
        }

        private object InnerExecScalar(string text, CommandType type, params object[] values)
        {
            return InnerExecCommand<object>(text, type, "Scalar", null, null, values);
        }

        private void InnerExecReader(string text, CommandType type, Action<SQLiteDataReader> block, params object[] values)
        {
            InnerExecCommand<object>(text, type, "Reader", block, null, values);
        }

        private List<T> InnerExecList<T>(string text, CommandType type, Func<SQLiteDataReader, T> selector, params object[] values)
        {
            return InnerExecCommand<T>(text, type, "List", null, selector, values) as List<T>;
        }

        private DataSet InnerExecDataSet(string text, CommandType type, params object[] values)
        {
            return InnerExecCommand<object>(text, type, "DataSet", null, null, values) as DataSet;
        }

        private object InnerExecCommand<T>(string text, CommandType type, string method, Action<SQLiteDataReader> block,
            Func<SQLiteDataReader, T> selector, params object[] values)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");
            Func<SQLiteCommand, object> exec;
            switch (method)
            {
                case "NonQuery":
                    exec = c =>
                    {
                        Monitor.Enter(obj);
                        int i = c.ExecuteNonQuery();
                        Monitor.Exit(obj);
                        return i;
                    };
                    break;
                case "Scalar":
                    exec = c => c.ExecuteScalar();
                    break;
                case "Reader":
                    exec = c =>
                    {
                        using (SQLiteDataReader reader = c.ExecuteReader())
                        {
                            block(reader);
                            return null;
                        }
                    };
                    break;
                case "DataSet":
                    exec = c =>
                    {
                        SQLiteDataAdapter adp = new SQLiteDataAdapter(c);
                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        return ds;
                    };
                    break;
                default:
                    //List
                    exec = c =>
                    {
                        using (SQLiteDataReader reader = c.ExecuteReader())
                        {
                            List<T> list = new List<T>();
                            while (reader.Read())
                            {
                                list.Add(selector(reader));
                            }
                            return list;
                        }
                    };
                    break;
            }
            if (this.isCommon)
            {
                using (SQLiteConnection con = new SQLiteConnection(ConnString))
                {
                    SQLiteCommand cmd = new SQLiteCommand(text, con)
                    {
                        CommandType = type
                    };
                    try
                    {
                        con.Open();
                        SetParams(cmd, values);
                        return exec(cmd);
                    }
                    catch (SQLiteException)
                    {
                        throw;
                    }
                }
            }
            else
            {
                this.command.Parameters.Clear();
                this.command.CommandText = text;
                this.command.CommandType = type;
                SetParams(this.command, values);
                return exec(this.command);
            }
        }

        private void SetParams(SQLiteCommand cmd, params object[] values)
        {
            if (cmd.CommandType == CommandType.Text)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    ((SQLiteParameter)(values[i])).Value = ((SQLiteParameter)(values[i])).Value ?? DBNull.Value;
                }
                cmd.Parameters.AddRange(values);
            }
            else if (cmd.CommandType == CommandType.StoredProcedure)
            {
                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    cmd.Parameters[i].Value = values[i] ?? DBNull.Value;
                }
            }
        }

        # endregion
    }

}
