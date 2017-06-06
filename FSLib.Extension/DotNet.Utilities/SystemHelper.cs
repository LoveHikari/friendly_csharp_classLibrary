using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNet.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemHelper
    {
        /// <summary>
        /// 检测端口号
        /// </summary>
        /// <param name="tempPort">端口号</param>
        /// <remarks>http://blog.csdn.net/jayzai/article/details/8182418</remarks>
        /// <returns></returns>
        public static bool CheckPort(string tempPort)
        {
            Process p = new Process
            {
                StartInfo = new ProcessStartInfo("netstat", "-an")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };
            p.Start();
            string result = p.StandardOutput.ReadToEnd().ToLower();//最后都转换成小写字母
            System.Net.IPAddress[] addressList = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
            List<string> ipList = new List<string> {"127.0.0.1", "0.0.0.0"};
            foreach (System.Net.IPAddress t in addressList)
            {
                ipList.Add(t.ToString());
            }
            bool use = false;
            foreach (string t in ipList)
            {
                if (result.IndexOf("tcp    " + t + ":" + tempPort, StringComparison.CurrentCultureIgnoreCase) > -1 || result.IndexOf("udp    " + t + ":" + tempPort, StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    use = true;
                    break;
                }
            }
            p.Close();
            return use;
        }
    }
}