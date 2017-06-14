using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using CodeHelper;

namespace Common.DBHelper
{
    /// <summary>
    /// 数据库连接父类
    /// </summary>
    public abstract class CrDB : IDBHelper
    {
        private string _logpath;
        private string _dbName;
        private string _connstr;

        /// <summary>
        /// 数据库连接
        /// </summary>
        public virtual string ConnStr
        {
            get { return GetConnStr(_dbName); }
            set { this._connstr = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string DbName
        {
            get
            {
                return this._dbName;
            }
            set
            {
                _dbName = value;
            }
        }
        public virtual string LogPath
        {
            get
            {
                _logpath = ConfigurationManager.AppSettings["logpath"];
                if (string.IsNullOrEmpty(_logpath))
                {
                    _logpath = System.IO.Path.Combine(Environment.CurrentDirectory, "log");
                    if (!System.IO.Directory.Exists(_logpath))
                    {
                        System.IO.Directory.CreateDirectory(_logpath);
                    }

                    return _logpath;
                }
                else
                {
                    return _logpath;
                }
            }
            set
            {
                _logpath = value;
            }
        }

        public abstract DataSet ExecSqlDataSet(string sql);
        public abstract DataSet ExecSqlDataSet(string sql, List<DBParam> dbparamlist);
        public abstract DataSet ExecSqlDataSet(string sql, List<DBParam> dbparamlist, string tablename);
        public abstract DataTable ExecSqlDataTable(string sql);
        public abstract DataTable ExecSqlDataTable(string sql, List<DBParam> dbparamlist);
        public abstract int ExecSqlNonQuery(string sql);
        public abstract int ExecSqlNonQuery(string sql, List<DBParam> dbparamlist);
        public abstract SqlDataReader ExecSqlReader(string sql, List<DBParam> dbparamlist);
        public abstract object ExecSqlScalar(string sql);
        public abstract object ExecSqlScalar(string sql, List<DBParam> dbparamlist);
        public abstract System.Data.OleDb.OleDbDataReader ExecOleDbDataReader(string sql, List<DBParam> dbparamlist);
        public abstract MySql.Data.MySqlClient.MySqlDataReader ExecMySqlDataReader(string sql, List<DBParam> dbparamlist);
        #region 私有方法
        /// <summary>
        /// 从XML文件获取数据库连接
        /// </summary>
        public virtual string GetConnStr(string dbName)
        {
            return ConfigHelper.Instance._Connstr;
        }
        /// <summary>
        /// 插入错误日志
        /// </summary>
        /// <param name="ex">异常</param>
        public virtual void Error(Exception ex)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                string directPath = ConfigurationManager.AppSettings["LogFilePath"]?.Trim();    //获得文件夹路径
                if (string.IsNullOrEmpty(directPath))
                {
                    directPath = System.IO.Path.Combine(Environment.CurrentDirectory, "log");  //如果日志文件为空，则默认在Debug目录下新建 YYYY-mm-dd_Log.log文件
                }
                if (!System.IO.Directory.Exists(directPath))   //判断文件夹是否存在，如果不存在则创建
                {
                    System.IO.Directory.CreateDirectory(directPath);
                }
                directPath = System.IO.Path.Combine(directPath, $"{DateTime.Now:yyyy-MM-dd}_Log.log");

                sw = !System.IO.File.Exists(directPath) ? System.IO.File.CreateText(directPath) : System.IO.File.AppendText(directPath);    //判断文件是否存在如果不存在则创建，如果存在则添加。

                //把异常信息输出到文件
                sw.WriteLine("当前时间：" + DateTime.Now);
                sw.WriteLine("异常信息：" + ex.Message);
                sw.WriteLine("异常对象：" + ex.Source);
                sw.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
                sw.WriteLine("触发方法：" + ex.TargetSite);
                sw.WriteLine("***********************************************************************");
                sw.WriteLine();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Dispose();
                }
            }
        }

        public virtual void CreateDell(string path)
        {
            string m_dir = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(m_dir))
            {
                System.IO.Directory.CreateDirectory(m_dir);
            }
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }
            string s = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                        "<SQlConn>" +
                            "<local ConnStr=\"server=121.41.101.4,5533;uid=kidsnet;pwd=1D#g2!hj3kYt4rwg5r#o6hfd7sr@;database=nynet\" />" +
                        "</SQlConn>";
            StreamWriter sw = System.IO.File.CreateText(path);
            sw.Write(s);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        #endregion
    }
}