using System;

namespace Common.DBHelper
{
    [Serializable]
    public class DBParam
    {
        private DataType _dbtype;
        private Object _dbvalue;
        private string _fieldsname;

        public DBParam()
        {
        }

        public DBParam(string fields, Object dbvalue, DataType dbtype)
        {
            this._fieldsname = fields;
            this._dbvalue = dbvalue;
            this._dbtype = dbtype;
        }


        public string FieldName
        {
            get { return this._fieldsname; }
            set { this._fieldsname = value; }
        }

        public Object DbValue
        {
            get { return this._dbvalue; }
            set { this._dbvalue = value; }
        }

        public DataType DbType
        {
            get { return this._dbtype; }
            set { this._dbtype = value; }
        }

    }
}