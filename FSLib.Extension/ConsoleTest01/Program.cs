using System;
using System.Collection;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Text;

namespace ConsoleTest01
{
    class Program
    {
        static void Main(string[] args)
        {
            //var r = JsonHelper.UrlToJson("date=2017-03-31&fvaluep=45.058&tvaluep=3.266&perShare=0.04&netAssets=2.34&income=13.24%E4%BA%BF&incomeGrowth=34.50%25&netProfit=4855.22%E4%B8%87&netProfitGrowth=28.61%25&concept=&relevantConcept=&leadingStock=&themePoints=&totalShareCapital=11.47%E4%BA%BF&circulatingCapital=10.60%E4%BA%BF&shareholder=5.48%E4%B8%87&previous=&firstShareholder=&tenShareholder=&investor=&screenCode=000739&addTime=2017%2F5%2F19+9%3A22%3A25&status=1");
            NameValueCollection collection = new NameValueCollection(){{"a","1"},{"b","2"}};
            
            System.Console.WriteLine("OK");
            System.Console.ReadKey();

        }


    }
}
