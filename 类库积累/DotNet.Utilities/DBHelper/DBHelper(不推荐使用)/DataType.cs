namespace Common.DBHelper
{
    public enum DataType
    {
        /// <summary>
        /// int \ SqlDbType.Int \ OleDbType.Integer
        /// </summary>
        DBInt,
        /// <summary>
        ///long \  SqlDbType.BigInt;
        /// </summary>
        DBLong,
        /// <summary>
        /// string \ SqlDbType.NVarChar \ OleDbType.VarWChar
        /// </summary>
        DBStr,
        /// <summary>
        /// bool \ SqlDbType.Bit \ OleDbType.Boolean
        /// </summary>
        DBBit,
        /// <summary>
        /// byte \ SqlDbType.Binary \ OleDbType.Binary;
        /// </summary>
        DBByte,
        /// <summary>
        /// DateTime \ SqlDbType.DateTime \ OleDbType.Date;
        /// </summary>
        DBDateTime,
        /// <summary>
        /// DateTime \ SqlDbType.SmallDateTime \ OleDbType.Date;
        /// </summary>
        DBSmallDateTime,
        /// <summary>
        /// DateTime \ SqlDbType.Date \ OleDbType.Date
        /// </summary>
        DBDate,
        /// <summary>
        /// DateTime \ SqlDbType.Time \ OleDbType.DBTime
        /// </summary>
        DBTime,
        /// <summary>
        /// Decomal \ SqlDbType.Decimal \ OleDbType.Decimal
        /// </summary>
        DBDecimal,
        /// <summary>
        /// float \ SqlDbType.Float \ OleDbType.Double
        /// </summary>
        DBFloat,
        /// <summary>
        /// btye \ SqlDbType.Image \ OleDbType.LongVarBinary
        /// </summary>
        DBImage,
        /// <summary>
        /// Decomal \ SqlDbType.Money \  OleDbType.Currency
        /// </summary>
        DBMoney,
        /// <summary>
        /// string \ SqlDbType.Text \ OleDbType.LongVarChar
        /// </summary>
        DBText,
        /// <summary>
        /// string \ SqlDbType.NText \ OleDbType.LongVarWChar
        /// </summary>
        DBNText,
        /// <summary>
        /// guid \ SqlDbType.UniqueIdentifier \  OleDbType.Guid
        /// </summary>
        DBGuid,
        /// <summary>
        /// byte SqlDbType.VarBinary \ OleDbType.VarBinary
        /// </summary>
        DBVarBinary,
        /// <summary>
        /// string  SqlDbType.VarChar \ OleDbType.VarChar
        /// </summary>
        DBVarChar,
        /// <summary>
        /// string SqlDbType.Char \ OleDbType.Char
        /// </summary>
        DBChar
    }
}