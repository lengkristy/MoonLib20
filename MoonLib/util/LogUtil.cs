using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace MoonLib.util
{
    public class LogUtil
{
    // Fields
    private static object lockObject = new object();
    public static string path = (AppDomain.CurrentDomain.BaseDirectory + @"\Log\");

    // Methods
    public static void Debug(string title, string content)
    {
        new Thread(() => new LogUtil().Log("debug", title, content)).Start();
    }

    public static void Error(string type, Exception ex)
    {
        new Thread(() => new LogUtil().Log(type, ex)).Start();
    }

    public static void Error(string title, string content)
    {
        new Thread(() => new LogUtil().Log("error", title, content)).Start();
    }

    public static void Info(string title, string content)
    {
        new Thread(() => new LogUtil().Log("info", title, content)).Start();
    }

    private void Log(string type, Exception ex)
    {
        lock (lockObject)
        {
            string str = DateTime.Now.ToString("yyyyMMdd") + ".moon.log";
            string str2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(path + str, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("utf-8")))
                {
                    writer.WriteLine("------------------------------------------------------------------");
                    writer.WriteLine("[1.位置:" + type + "]");
                    writer.WriteLine("[2.时间:" + str2 + "]");
                    writer.WriteLine("[3.实例:" + ex.InnerException + "]");
                    writer.WriteLine("[4.来源:" + ex.Source + "]");
                    writer.WriteLine("[5.方法:" + ex.TargetSite + "]");
                    writer.WriteLine("[6.堆栈:" + ex.StackTrace + "]");
                    writer.WriteLine("[7.提示:" + ex.Message + "]");
                    writer.WriteLine("------------------------------------------------------------------");
                }
            }
        }
    }

    private void Log(string type, string title, string content)
    {
        lock (lockObject)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".moon.log";
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string logContent = string.Format("[{0}][{1}][{2}][{3}]", datetime, type, title, content);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileStream stream = null;
            StreamWriter writer = null;
            try
            {
                stream = new FileStream(path + fileName, FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(stream, Encoding.GetEncoding("utf-8"));
                writer.WriteLine(logContent);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (writer != null)
                {
                    try
                    {
                        writer.Close();
                    }
                    catch
                    {
                    }
                }
                if (stream != null)
                {
                    try
                    {
                        stream.Close();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}


}
