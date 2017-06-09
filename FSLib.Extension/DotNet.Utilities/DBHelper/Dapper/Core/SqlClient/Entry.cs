using System.Collections.Generic;
using System.Data;
using System.DBHelper.Dapper.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.DBHelper.Dapper.Core.SqlClient
{
    /// <summary>
    /// Dapper数据库访问核心类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Entry<T> where T : class
    {
        private DataBaseAccess _access;
        /// <summary>
        /// 数据库访问实例
        /// </summary>
        public DataBaseAccess Access { get => _access; set => _access = value; }
        /// <summary>
        /// 主键字段名称
        /// </summary>
        private string PrimaryKey
        {
            get
            {
                Type t = typeof(T);
                var pros = t.GetProperties();
                foreach (var pro in pros)
                {
                    var keyInfo = pro.GetCustomAttribute(typeof(KeyInfoAttribute), true) as KeyInfoAttribute;
                    if (keyInfo != null)
                    {
                        return pro.Name;
                    }
                }
                return "Id";
            }
        }
        /// <summary>
        /// 表名
        /// </summary>
        private string TableName
        {
            get
            {
                Type t = typeof(T);
                TableInfoAttribute tableInfo = t.GetCustomAttribute(typeof(TableInfoAttribute), true) as TableInfoAttribute;
                return tableInfo != null ? tableInfo.TableName : typeof(T).Name;
            }
        }
        /// <summary>
        /// 构造函数，默认数据库连接配置节为connString
        /// </summary>
        public Entry()
        {
            _access = new DataBaseAccess();
            _access.ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接配置节</param>
        public Entry(string connectionString)
        {
            _access = new DataBaseAccess();
            _access.ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="mappingType">无意义</param>
        public Entry(string connStr, MappingType mappingType)
        {
            _access = new DataBaseAccess();
            _access.ConnStr = connStr;
        }


        /// <summary>
        /// 增加一条数据,返回增加之后的实体
        /// </summary>
        /// <param name="entity">需要增加的实体</param>
        /// <returns>增加之后的实体</returns>
        public T SaveChanges(T entity)
        {
            using (IDbTransaction transaction = _access.Conn.BeginTransaction())
            {
                Type type = typeof(T);

                var pros = type.GetProperties().ToList();
                pros.Remove(type.GetProperty(PrimaryKey));
                var pns = pros.Select(p => p.Name);
                string s1 = String.Join(",", pns);
                pns = pros.Select(p => "@" + p.Name);
                string s2 = String.Join(",", pns);

                StringBuilder strSql = new StringBuilder();
                strSql.Append($"insert into [{TableName}](");
                strSql.Append($"{s1})");
                strSql.Append(" values (");
                strSql.Append($"{s2})");
                strSql.Append(";select @@IDENTITY");

                int id = _access.Insert(strSql.ToString(), entity);
                entity = new Func<T>(delegate ()
                {
                    StringBuilder str = new StringBuilder();
                    str.Append($"select  top 1 * from [{TableName}] ");
                    str.Append($" where {PrimaryKey}=@id");
                    return _access.Query<T>(str.ToString(), new { id = id }).SingleOrDefault();
                }).Invoke();

                transaction.Commit();
            }

            return entity;



        }
        ///// <summary>
        ///// 查询记录数
        ///// </summary>
        ///// <param name="predicate">条件表达式</param>
        ///// <returns></returns>
        //public  int Count(Func<T, bool> predicate)
        //{
        //    using (IDbTransaction transaction = _access.Conn.BeginTransaction())
        //    {
        //        Type type = typeof(T);
        //        var pros = type.GetProperties().ToList();
        //        pros.Remove(type.GetProperty(PrimaryKey));


        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append($"SELECT COUNT(1) FROM [{TableName}]");


        //        int i = _access.Query<T>(strSql.ToString(), predicate).Count;
        //        transaction.Commit();
        //        return i;
        //    }
        //}
        ///// <summary>
        ///// 是否存在
        ///// </summary>
        ///// <param name="predicate">条件表达式</param>
        ///// <returns>存在为true</returns>
        //public  bool Any(Func<T, bool> predicate)
        //{
        //    using (IDbTransaction transaction = _access.Conn.BeginTransaction())
        //    {
        //        Type type = typeof(T);
        //        var pros = type.GetProperties().ToList();
        //        pros.Remove(type.GetProperty(PrimaryKey));


        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append($"SELECT COUNT(1) FROM [{TableName}]");


        //        int i = _access.Query<T>(strSql.ToString(), predicate).Count;
        //        transaction.Commit();
        //        if (i > 0)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}
        /// <summary>
        /// 修改一条数据，返回受影响的行数
        /// </summary>
        /// <param name="entity">需要修改的实体</param>
        /// <returns>受影响的行数</returns>
        public  int Update(T entity)
        {
            using (IDbTransaction transaction = _access.Conn.BeginTransaction())
            {
                Type type = typeof(T);
                var pros = type.GetProperties().ToList();
                pros.Remove(type.GetProperty(PrimaryKey));


                StringBuilder strSql = new StringBuilder();
                strSql.Append($"update [{TableName}] set ");
                foreach (var pro in pros)
                {
                    strSql.AppendFormat("{0}=@{0},", pro.Name);
                }
                strSql.Remove(strSql.Length - 1, 1);
                strSql.AppendFormat(" where {0}=@{0}", PrimaryKey);

                int i = _access.Execute(strSql.ToString(), entity);
                transaction.Commit();
                return i;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity">需要删除的实体</param>
        /// <returns>受影响的行数</returns>
        public  int Delete(T entity)
        {
            using (IDbTransaction transaction = _access.Conn.BeginTransaction())
            {
                Type type = typeof(T);
                var pros = type.GetProperties().ToList();
                pros.Remove(type.GetProperty(PrimaryKey));
                var pns = pros.Select(p => p.Name);
                string s1 = "";
                pns.ToList().ForEach(s =>
                {
                    s1 += $"{s}=@{s} and ";
                });
                s1 = s1.Remove(s1.Length - 4, 4);

                StringBuilder strSql = new StringBuilder();
                strSql.Append($"delete from [{TableName}] ");
                strSql.AppendFormat(" where {0}", s1);

                int i = _access.Execute(strSql.ToString(), entity);
                transaction.Commit();
                return i;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">需要删除的数据的id</param>
        /// <returns>受影响的行数</returns>
        public int Delete(int id)
        {
            using (IDbTransaction transaction = _access.Conn.BeginTransaction())
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append($"delete from [{TableName}] ");
                strSql.Append($" where {PrimaryKey}=@id");

                int i = _access.Execute(strSql.ToString(), new{id = id});
                transaction.Commit();
                return i;
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<T> Set()
        {
            using (IDbTransaction transaction = _access.Conn.BeginTransaction())
            {
                Type type = typeof(T);
                var pros = type.GetProperties().ToList();
                pros.Remove(type.GetProperty(PrimaryKey));


                StringBuilder strSql = new StringBuilder();
                strSql.Append($"SELECT * FROM [{TableName}]");


                var entity = _access.Query<T>(strSql.ToString());
                transaction.Commit();

                return entity;
            }
        }
    }
}