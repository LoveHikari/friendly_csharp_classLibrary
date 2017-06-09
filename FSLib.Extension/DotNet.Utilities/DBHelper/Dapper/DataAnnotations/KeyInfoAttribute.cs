namespace System.DBHelper.Dapper.DataAnnotations
{
    /// <summary>
    /// 表示唯一标识实体的一个或多个属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class KeyInfoAttribute : Attribute
    {
        
    }
}