using System;
using System.Data;

namespace DotNet.Utilities.DBHelper.CrDB
{
    /// <summary>
    /// DbCommand 的参数
    /// </summary>
    [Serializable]
    public class DBParam
    {
        private DbType _dbtype;
        private Object _dbvalue;
        private string _fieldsname;
        private int _size;
        /// <summary>
        /// DbCommand 的参数
        /// </summary>
        public DBParam()
        {
        }
        /// <summary>
        /// DbCommand 的参数
        /// </summary>
        /// <param name="fields">参数名称</param>
        /// <param name="dbvalue">参数值</param>
        /// <param name="dbtype">参数类型</param>
        public DBParam(string fields, Object dbvalue, DbType dbtype)
        {
            this._fieldsname = fields;
            this._dbvalue = dbvalue;
            this._dbtype = dbtype;
        }
        /// <summary>
        /// DbCommand 的参数
        /// </summary>
        /// <param name="fields">参数名称</param>
        /// <param name="dbvalue">参数值</param>
        /// <param name="dbtype">参数类型</param>
        /// <param name="size">参数最大长度</param>
        public DBParam(string fields, Object dbvalue, DbType dbtype, int size)
        {
            this._fieldsname = fields;
            this._dbvalue = dbvalue;
            this._dbtype = dbtype;
            this._size = size;
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string FieldName
        {
            get { return this._fieldsname; }
            set { this._fieldsname = value; }
        }
        /// <summary>
        /// 参数值
        /// </summary>
        public Object DbValue
        {
            get { return this._dbvalue; }
            set { this._dbvalue = value; }
        }
        /// <summary>
        /// 参数类型
        /// </summary>
        public DbType DbType
        {
            get { return this._dbtype; }
            set { this._dbtype = value; }
        }
        /// <summary>
        /// 参数最大长度
        /// </summary>
        public int Size
        {
            get { return this._size; }
            set { this._size = value; }
        }
    }
}