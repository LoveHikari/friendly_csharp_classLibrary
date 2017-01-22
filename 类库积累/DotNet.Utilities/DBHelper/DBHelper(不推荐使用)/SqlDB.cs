using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Common.DBHelper
{
    /// <summary>
    /// sql server
    /// </summary>
    public sealed class SqlDB : CrDB
    {
        public static readonly SqlDB Instance = new SqlDB();//公共实例
        public SqlDB()
        {

        }
        public SqlDB(string dbName)
        {
            this.DbName = dbName;
        }

        /// <summary>
        /// 更新SQL,返回受影响行数，用于执行insert、update、delete等非查询语句。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override int ExecSqlNonQuery(string sql)
        {
            int n = 0;
            using (SqlConnection conn = new SqlConnection(this.ConnStr))
            {
                try
                {
                    SqlCommand commd = new SqlCommand(sql, conn);
                    conn.Open();
                    n = commd.ExecuteNonQuery();
                    conn.Close();
                    return n;
                }
                catch (Exception ex)
                {
                    Error(ex);
                    throw ex;
                }
            }

        }

        /// <summary>
        /// 更新参数化的SQL，用于执行insert、update、delete等非查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist">参数列表</param>
        /// <returns></returns>
        public override int ExecSqlNonQuery(string sql, List<DBParam> dbparamlist)
        {
            int n = 0;
            using (SqlConnection conn = new SqlConnection(this.ConnStr))
            {
                try
                {
                    SqlCommand commd = new SqlCommand(sql, conn);
                    commd.Parameters.Clear();
                    commd = GetSqlParameter(dbparamlist, commd);
                    conn.Open();
                    n = commd.ExecuteNonQuery();
                    conn.Close();
                    return n;
                }
                catch (Exception ex)
                {
                    Error(ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 执行Sql语句，返回DataSet。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override System.Data.DataSet ExecSqlDataSet(string sql)
        {
            DataSet ds;
            using (SqlConnection conn = new SqlConnection(this.ConnStr))
            {
                try
                {
                    ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    sda.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    ds = null;
                    Error(ex);
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 查询SQL，带参数，返回DataSet。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        public override System.Data.DataSet ExecSqlDataSet(string sql, List<DBParam> dbparamlist)
        {
            DataSet ds;
            using (SqlConnection conn = new SqlConnection(this.ConnStr))
            {
                try
                {
                    SqlCommand commd = new SqlCommand(sql, conn);
                    commd.Parameters.Clear();
                    commd = GetSqlParameter(dbparamlist, commd);
                    SqlDataAdapter sda = new SqlDataAdapter(commd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    ds = null;
                    Error(ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 查询SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override System.Data.DataTable ExecSqlDataTable(string sql)
        {
            return this.ExecSqlDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 查询SQL，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        public override System.Data.DataTable ExecSqlDataTable(string sql, List<DBParam> dbparamlist)
        {
            return this.ExecSqlDataSet(sql, dbparamlist).Tables[0];
        }

        /// <summary>
        /// 查询SQL，并指定表名，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public override System.Data.DataSet ExecSqlDataSet(string sql, List<DBParam> dbparamlist, string tablename)
        {
            DataSet ds;
            using (SqlConnection conn = new SqlConnection(this.ConnStr))
            {
                try
                {
                    SqlCommand commd = new SqlCommand(sql, conn);
                    commd.Parameters.Clear();
                    commd = GetSqlParameter(dbparamlist, commd);
                    SqlDataAdapter sda = new SqlDataAdapter(commd);
                    ds = new DataSet();
                    sda.Fill(ds, tablename);
                    return ds;
                }
                catch (Exception ex)
                {
                    ds = null;
                    Error(ex);
                    throw ex;
                }
            }

        }

        /// <summary>
        /// 查询SQL，返回第一行第一列字段
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override object ExecSqlScalar(string sql)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnStr))
            {
                try
                {
                    SqlCommand commd = new SqlCommand(sql, conn);
                    conn.Open();
                    object o = commd.ExecuteScalar();
                    conn.Close();
                    return o;
                }
                catch (Exception ex)
                {
                    Error(ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 查询SQL，返回第一行第一列字段，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        public override object ExecSqlScalar(string sql, List<DBParam> dbparamlist)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnStr))
            {
                try
                {
                    SqlCommand commd = new SqlCommand(sql, conn);
                    commd.Parameters.Clear();
                    commd = GetSqlParameter(dbparamlist, commd);
                    conn.Open();
                    object obj = commd.ExecuteScalar();
                    conn.Close();
                    return obj;
                }
                catch (Exception ex)
                {
                    Error(ex);
                    throw ex;
                }
            }

        }

        /// <summary>
        /// 查询SQL，返回DataReader，带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbparamlist"></param>
        /// <returns></returns>
        public override SqlDataReader ExecSqlReader(string sql, List<DBParam> dbparamlist)
        {
            SqlConnection conn = new SqlConnection(this.ConnStr);
            try
            {
                SqlCommand commd = new SqlCommand(sql, conn);
                commd.Parameters.Clear();
                commd = GetSqlParameter(dbparamlist, commd);
                conn.Open();
                return commd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                Error(ex);
                conn.Close();
                conn.Dispose();
                throw ex;
            }

        }
        public override System.Data.OleDb.OleDbDataReader ExecOleDbDataReader(string sql, List<DBParam> dbparamlist)
        {
            return null;
        }

        public override MySqlDataReader ExecMySqlDataReader(string sql, List<DBParam> dbparamlist)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取SQL参数类型
        /// </summary>
        /// <param name="dtype"></param>
        /// <returns></returns>
        public SqlDbType TypeToSqlType(DataType dtype)
        {
            switch (dtype)
            {
                case DataType.DBBit:
                    return SqlDbType.Bit;
                case DataType.DBByte:
                    return SqlDbType.Binary;
                case DataType.DBChar:
                    return SqlDbType.Char;
                case DataType.DBDate:
                    return SqlDbType.Date;
                case DataType.DBDateTime:
                    return SqlDbType.DateTime;
                case DataType.DBDecimal:
                    return SqlDbType.Decimal;
                case DataType.DBFloat:
                    return SqlDbType.Float;
                case DataType.DBGuid:
                    return SqlDbType.UniqueIdentifier;
                case DataType.DBImage:
                    return SqlDbType.Image;
                case DataType.DBInt:
                    return SqlDbType.Int;
                case DataType.DBLong:
                    return SqlDbType.BigInt;
                case DataType.DBMoney:
                    return SqlDbType.Money;
                case DataType.DBSmallDateTime:
                    return SqlDbType.SmallDateTime;
                case DataType.DBStr:
                    return SqlDbType.NVarChar;
                case DataType.DBText:
                    return SqlDbType.Text;
                case DataType.DBNText:
                    return SqlDbType.NText;
                case DataType.DBTime:
                    return SqlDbType.Time;
                case DataType.DBVarBinary:
                    return SqlDbType.VarBinary;
                case DataType.DBVarChar:
                    return SqlDbType.VarChar;
                default:
                    return SqlDbType.NVarChar;
            }
        }

        public SqlCommand GetSqlParameter(List<DBParam> dbparamlist, SqlCommand commd)
        {

            foreach (DBParam p in dbparamlist)
            {

                SqlParameter sqlp = new SqlParameter();
                sqlp.ParameterName = p.FieldName;
                sqlp.SqlDbType = TypeToSqlType(p.DbType);

                if (p.DbValue == null)
                {
                    sqlp.Value = Convert.DBNull;
                }
                else
                {
                    if (p.DbValue.GetType().ToString() == "System.DateTime")
                    {
                        if (DateTime.MinValue == (DateTime)p.DbValue)
                        {
                            sqlp.Value = Convert.DBNull;
                        }
                        else
                        {
                            sqlp.Value = p.DbValue;
                        }
                    }
                    else
                    {
                        sqlp.Value = p.DbValue;
                    }
                }

                commd.Parameters.Add(sqlp);
            }

            return commd;
        }

    }
}