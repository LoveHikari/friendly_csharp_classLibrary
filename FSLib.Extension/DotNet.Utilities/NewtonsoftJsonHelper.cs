using System.IO;
using Newtonsoft.Json;

namespace System
{
    public class NewtonsoftJsonHelper
    {
        /// <summary>
        /// json格式化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks>http://www.cnblogs.com/unintersky/p/3884712.html</remarks>
        public static string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }
}