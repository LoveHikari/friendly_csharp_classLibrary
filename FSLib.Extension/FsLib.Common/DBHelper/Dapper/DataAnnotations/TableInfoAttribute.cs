namespace System.DBHelper.Dapper.DataAnnotations
{
    /// <summary>
    /// 指定类将映射到的数据库表。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class TableInfoAttribute : Attribute
    {
        private string _tableName;
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string TableName => _tableName;
        public TableInfoAttribute(string tableName)
        {
            this._tableName = tableName;
        }
    }
}