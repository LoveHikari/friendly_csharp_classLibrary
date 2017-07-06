using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Text;
using System.Collection;
using System.Data.SqlClient;
using System.DBHelper.CrDB;
using System.DBHelper.DataAccess;

namespace ConsoleTest01
{
    class Program
    {
        static void Main(string[] args)
        {
            "".ToDateTime(null);

            System.Console.WriteLine("OK");
            System.Console.ReadKey();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nowPage">当前页</param>
        /// <param name="pageSize">每页容纳的记录数</param>
        /// <param name="tab">表名</param>
        /// <param name="strFld">字段字符串</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="sort">排序字段及规则,不用加order by</param>
        /// <returns></returns>
        public static dynamic GetRecordByPage(int nowPage, int pageSize, string tab, string strFld, string strWhere, string sort)
        {
            CrDB db = new DBHelper("ConnectionString");
            DataSet ds = new DataSet();
            //SqlParameter[] parameters = {
            //    new SqlParameter("@tab", SqlDbType.NVarChar,int.MaxValue),
            //    new SqlParameter("@strFld", SqlDbType.NVarChar,int.MaxValue),
            //    new SqlParameter("@strWhere", SqlDbType.VarChar,int.MaxValue),
            //    new SqlParameter("@Sort", SqlDbType.VarChar,225),
            //    new SqlParameter("@NowPage", SqlDbType.Int,4),
            //    new SqlParameter("@PageSize", SqlDbType.Int,4),
            //    new SqlParameter("@pageCount", SqlDbType.Int,4),
            //    new SqlParameter("@prevPage", SqlDbType.Int,4),
            //    new SqlParameter("@nextPage", SqlDbType.Int,4),
            //    new SqlParameter("@totalCount", SqlDbType.Int,4)
            //};
            //parameters[0].Value = tab;
            //parameters[1].Value = strFld;
            //parameters[2].Value = strWhere;
            //parameters[3].Value = sort;
            //parameters[4].Value = nowPage;
            //parameters[5].Value = pageSize;
            //parameters[6].Value = 0;
            //parameters[7].Value = 0;
            //parameters[8].Value = 0;
            //parameters[9].Value = 0;

            //parameters[6].Direction = ParameterDirection.Output;
            //parameters[7].Direction = ParameterDirection.Output;
            //parameters[8].Direction = ParameterDirection.Output;
            //parameters[9].Direction = ParameterDirection.Output;

            List<DBParam> dbParams = new List<DBParam>(new DBParam[]
            {
                new DBParam("@tab",tab, DbType.String,int.MaxValue),
                new DBParam("@strFld",strFld, DbType.String,int.MaxValue),
                new DBParam("@strWhere",strWhere, DbType.String,int.MaxValue),
                new DBParam("@Sort",sort, DbType.String,225),
                new DBParam("@NowPage",nowPage, DbType.Int32,4),
                new DBParam("@PageSize",pageSize, DbType.Int32,4),
                new DBParam("@pageCount",0, DbType.Int32,4,ParameterDirection.Output),
                new DBParam("@prevPage",0, DbType.Int32,4,ParameterDirection.Output),
                new DBParam("@nextPage",0, DbType.Int32,4,ParameterDirection.Output),
                new DBParam("@totalCount",0, DbType.Int32,4,ParameterDirection.Output)
            });


            ds = db.ExecuteDataSet("Common_PageList", CommandType.StoredProcedure, dbParams);
            var v = db.DbCommand.Parameters["@totalCount"].Value;

            //var pageCount = db.command.Parameters["@pageCount"].Value;
            //var prevPage = SqlHelper.Instance.command.Parameters["@prevPage"].Value;
            //var nextPage = SqlHelper.Instance.command.Parameters["@nextPage"].Value;
            //var totalCount = SqlHelper.Instance.command.Parameters["@totalCount"].Value;
            //var v = new
            //{
            //    NowPage = nowPage,  //当前页
            //    PageSize = pageSize,  //每页容纳的记录数
            //    PageCount = pageCount,  //最大页
            //    TotalCount = totalCount,//数据总数
            //    PrevPage = prevPage,  //上一页
            //    NextPage = nextPage,  //下一页
            //    Data = ds.Tables[0]
            //};
            return null;
        }
    }
}
