

/******************************************************************************************************************
 * 
 * 
 * 标  题： log4net 日志帮助类(版本：Version1.0.0)
 * 作  者： YuXiaoWei
 * 日  期： 2017/03/27
 * 修  改：
 * 参  考： http://www.cnblogs.com/kissazi2/p/3392094.html
 * 说  明： 需要在AssemblyInfo.cs中添加[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4net.config", ConfigFileExtension = "config", Watch = true)]
 *          添加文件Log4net.config，内容见底部注释内容
 * 备  注： 暂无...
 * 调用示列：
 *
 * 
 * ***************************************************************************************************************/
namespace System
{
    /// <summary>
    /// log4net 日志帮助类
    /// </summary>
    public class Log4NetHelper
    {
        private static log4net.ILog Loginfo => log4net.LogManager.GetLogger("loginfo");

        private static log4net.ILog Logerror => log4net.LogManager.GetLogger("logerror");
        /// <summary>
        /// 输出logo
        /// </summary>
        /// <param name="info">消息</param>
        public static void WriteLog(string info)
        {

            if (Loginfo.IsInfoEnabled)
            {
                Loginfo.Info(info);
            }
        }
        /// <summary>
        /// 输出logo
        /// </summary>
        /// <param name="info">消息</param>
        /// <param name="se">错误</param>
        public static void WriteLog(string info, Exception se)
        {
            if (Logerror.IsErrorEnabled)
            {
                Logerror.Error(info, se);
            }
        }
    }
}

//<?xml version="1.0" encoding="utf-8" ?>
//<configuration>
//  <configSections>
//    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net" />
//  </configSections>

//  <log4net>
//    <logger name="logerror">
//      <level value="ALL" />
//      <appender-ref ref="ErrorAppender" />
//    </logger>
//    <logger name="loginfo">
//      <level value="ALL" />
//      <appender-ref ref="InfoAppender" />
//    </logger>
//    <appender name="ErrorAppender" type="log4net.Appender.RollingFileAppender">
//      <param name="File" value="Log\\LogError\\" /> <!--file可以指定具体的路径 eg : d:\\test.log。不指定的话log被生成在项目的bin/Debug 或者 bin/Release目录下 （web的项目 默认生成在根目录下）-->
//      <param name="AppendToFile" value="true" />
//      <param name="MaxSizeRollBackups" value="100" /> <!--备份log文件的个数最多100个-->
//      <param name="MaxFileSize" value="10240" /> <!--每个log文件最大是2M，如果超过2M将重新创建一个新的log文件，并将原来的log文件备份。-->
//      <param name="StaticLogFileName" value="false" />
//      <param name="DatePattern" value="yyyyMMdd&quot;.htm&quot;" />
//      <param name="RollingStyle" value="Date" />
//      <layout type="log4net.Layout.PatternLayout"> <!--指定log的格式-->
//        <param name="ConversionPattern" value="&lt;HR COLOR=red&gt;%n异常时间：%d [%t] &lt;BR&gt;%n异常级别：%-5p &lt;BR&gt;%n异 常 类：%c [%x] &lt;BR&gt;%n%m &lt;BR&gt;%n &lt;HR Size=1&gt;"  />
//      </layout>
//    </appender>
//    <appender name="InfoAppender" type="log4net.Appender.RollingFileAppender">
//      <param name="File" value="Log\\LogInfo\\" />
//      <param name="AppendToFile" value="true" />
//      <param name="MaxFileSize" value="10240" />
//      <param name="MaxSizeRollBackups" value="100" />
//      <param name="StaticLogFileName" value="false" />
//      <param name="DatePattern" value="yyyyMMdd&quot;.htm&quot;" />
//      <param name="RollingStyle" value="Date" />
//      <layout type="log4net.Layout.PatternLayout">
//        <param name="ConversionPattern" value="&lt;HR COLOR=blue&gt;%n日志时间：%d [%t] &lt;BR&gt;%n日志级别：%-5p &lt;BR&gt;%n日 志 类：%c [%x] &lt;BR&gt;%n%m &lt;BR&gt;%n &lt;HR Size=1&gt;"  />
//      </layout>
//    </appender>
//  </log4net>
//</configuration>
