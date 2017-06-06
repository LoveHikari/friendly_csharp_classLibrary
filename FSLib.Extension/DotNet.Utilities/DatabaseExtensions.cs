using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Data.Entity;

/******************************************************************************************************************
 * 
 * 
 * 标  题： EF调用存储过程、函数 帮助类(版本：Version1.0.0)
 * 作  者： YuXiaoWei
 * 日  期： 2017/04/28
 * 修  改：
 * 参  考： http://blog.csdn.net/yhyhyhy/article/details/52497666
 * 说  明： 暂无...
 * 备  注： 暂无...
 * 调用示列：
 *
 * 
 * ***************************************************************************************************************/
namespace DotNet.Utilities
{
    /// <summary>
    /// EF Database 扩展类
    /// </summary>
    /// <remarks></remarks>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// 执行sql，返回DataTable，与其他sql函数用法相同
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable SqlQueryForDataTable(this Database db, string sql, params object[] parameters)
        {
            return ToDataTable(SqlQueryForDynamic(db, sql, parameters));
        }
        /// <summary>
        /// 执行sql，返回公开枚举数，与其他sql函数用法相同
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>公开枚举数</returns>
        public static IEnumerable SqlQueryForDynamic(this Database db, string sql, params object[] parameters)
        {
            IDbConnection defaultConn = new System.Data.SqlClient.SqlConnection();

            return SqlQueryForDynamicOtherDB(db, sql, defaultConn, parameters);
        }
        /// <summary>
        /// 执行sql，返回公开枚举数，与其他sql函数用法相同
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据源</param>
        /// <param name="parameters">参数</param>
        /// <returns>公开枚举数</returns>
        private static IEnumerable SqlQueryForDynamicOtherDB(this Database db, string sql, IDbConnection conn, params object[] parameters)
        {
            conn.ConnectionString = db.Connection.ConnectionString;

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            IDataReader dataReader = cmd.ExecuteReader();

            if (!dataReader.Read())
            {
                return null; //无结果返回Null
            }

            #region 构建动态字段

            TypeBuilder builder = DatabaseExtensions.CreateTypeBuilder(
                          "EF_DynamicModelAssembly",
                          "DynamicModule",
                          "DynamicType");

            int fieldCount = dataReader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                //dic.Add(i, dataReader.GetName(i));

                //Type type = dataReader.GetFieldType(i);

                DatabaseExtensions.CreateAutoImplementedProperty(
                  builder,
                  dataReader.GetName(i),
                  dataReader.GetFieldType(i));
            }

            #endregion

            dataReader.Close();
            dataReader.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            Type returnType = builder.CreateType();

            if (parameters != null)
            {
                return db.SqlQuery(returnType, sql, parameters);
            }
            else
            {
                return db.SqlQuery(returnType, sql);
            }
        }
        /// <summary>
        /// 创建类型构建器
        /// </summary>
        /// <param name="assemblyName">部件名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="typeName">类型名称</param>
        /// <returns>类型构建器</returns>
        private static TypeBuilder CreateTypeBuilder(string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
              new AssemblyName(assemblyName),
              AssemblyBuilderAccess.Run).DefineDynamicModule(moduleName).DefineType(typeName,
              TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            return typeBuilder;
        }
        /// <summary>
        /// 创建自动实现的属性
        /// </summary>
        /// <param name="builder">类型构建器</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyType">属性类型</param>
        private static void CreateAutoImplementedProperty(TypeBuilder builder, string propertyName, Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = builder.DefineField(
              string.Concat(
                PrivateFieldPrefix, propertyName),
              propertyType,
              FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = builder.DefineProperty(
              propertyName,
              System.Reflection.PropertyAttributes.HasDefault,
              propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes = MethodAttributes.Public
              | MethodAttributes.SpecialName
              | MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = builder.DefineMethod(
                string.Concat(
                  GetterPrefix, propertyName),
                propertyMethodAttributes,
                propertyType,
                Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = builder.DefineMethod(
              string.Concat(SetterPrefix, propertyName),
              propertyMethodAttributes,
              null,
              new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
        /// <summary>
        /// 公开枚举数转DataTable
        /// </summary>
        /// <param name="enumerable">枚举</param>
        /// <returns>DataTable</returns>
        private static DataTable ToDataTable(IEnumerable enumerable)
        {
            var ls = enumerable as object[] ?? enumerable.Cast<object>().ToArray();
            if (ls.Length < 1)
            {
                return null;
            }
            DataTable dataTable = new DataTable("Table");
            
            foreach (var l in ls)
            {
                Type type = l.GetType();
                var pros = type.GetProperties();
                foreach (var pro in pros)
                {
                    dataTable.Columns.Add(pro.Name);
                }
                break;
            }

            foreach (var l in ls)
            {
                DataRow row = dataTable.NewRow();
                Type type = l.GetType();
                var pros = type.GetProperties();
                foreach (var pro in pros)
                {
                    row[pro.Name] = pro.GetValue(l, null);
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}