﻿using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using DotNet.Utilities.DBHelper.Dapper.DataAnnotations;

namespace DotNet.Utilities.DBHelper.Dapper.Core.SqlClient
{
    public partial class Entry<T> where T : class
    {
        /// <summary>
        /// 主键字段名称
        /// </summary>
        public static string PrimaryKey
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
        public static string TableName
        {
            get
            {
                Type t = typeof(T);
                TableInfoAttribute tableInfo = t.GetCustomAttribute(typeof(TableInfoAttribute), true) as TableInfoAttribute;
                return tableInfo != null ? tableInfo.TableName : typeof(T).Name;
            }
        }
        /// <summary>
        /// 增加一条数据,返回增加之后的实体
        /// </summary>
        /// <param name="model">需要增加的实体</param>
        /// <returns>增加之后的实体</returns>
        public static T SaveChanges(T model)
        {
            using (IDbTransaction transaction = DataBaseAccess.Conn.BeginTransaction())
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

                int id = DataBaseAccess.Insert(strSql.ToString(), model);
                model = new Func<T>(delegate ()
                {
                    StringBuilder str = new StringBuilder();
                    str.Append($"select  top 1 * from [{TableName}] ");
                    str.Append($" where {PrimaryKey}=@id");
                    return DataBaseAccess.Query<T>(str.ToString(), new { id = id }).SingleOrDefault();
                }).Invoke();

                transaction.Commit();
            }
            
            return model;



        }
        /// <summary>
        /// 修改一条数据，返回受影响的行数
        /// </summary>
        /// <param name="model">需要修改的实体</param>
        /// <returns>受影响的行数</returns>
        public static int Update(T model)
        {
            using (IDbTransaction transaction = DataBaseAccess.Conn.BeginTransaction())
            {
                Type type = typeof(T);
                var pros = type.GetProperties().ToList();
                pros.Remove(type.GetProperty(PrimaryKey));
                

                StringBuilder strSql = new StringBuilder();
                strSql.Append($"update [{TableName}] set ");
                foreach (var pro in pros)
                {
                    strSql.AppendFormat("{0}=@{0},",pro.Name);
                }
                strSql.Remove(strSql.Length - 1, 1);
                strSql.AppendFormat(" where {0}=@{0}",PrimaryKey);

                int i = DataBaseAccess.Execute(strSql.ToString(), model);
                transaction.Commit();
                return i;
            }
        }

        public static int Delete(int id)
        {
            using (IDbTransaction transaction = DataBaseAccess.Conn.BeginTransaction())
            {
                Type type = typeof(T);
                var pros = type.GetProperties().ToList();
                pros.Remove(type.GetProperty(PrimaryKey));


                StringBuilder strSql = new StringBuilder();
                strSql.Append($"delete from [{TableName}] ");
                strSql.AppendFormat(" where {0}=@id",PrimaryKey);

                int i = DataBaseAccess.Execute(strSql.ToString(), new { id = id });
                transaction.Commit();
                return i;
            }
        }
    }
}