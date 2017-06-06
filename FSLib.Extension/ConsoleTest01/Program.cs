using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using DotNet.Utilities;

namespace ConsoleTest01
{
    class Program
    {
        static void Main(string[] args)
        {
            string _historyDataPath = System.IO.Path.Combine(Environment.CurrentDirectory, "App_Data\\historyData.xml");
            if (!System.IO.File.Exists(_historyDataPath))
            {
                var dicList = new List<SerializableDictionary<string, string>>();  //0:上一个最新行情,1:上一个分时线,2:上一个大盘指数
                for (int i = 0; i < 3; i++)
                {
                    dicList.Add(new SerializableDictionary<string, string>());
                }
                XmlHelper.XmlSerilizeToFile(dicList, _historyDataPath);
            }

            System.Console.WriteLine("OK");
            System.Console.ReadKey();

        }

    }
}
