using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ZKSDKCommunication
{
    public class ProtocolLogger
    {
        private const string DEFAULT_LOG_FOLDER = "Logs";


        public static void AddLogToFile(string messageLog)
        {
            try
            {
                StreamWriter sw = new StreamWriter(logFileName(), true);
                sw.WriteLine(DateTime.Now.ToString("yyyy.MM.dd - HH:mm") + " " + messageLog);
                sw.Close();
            }
            catch
            {
                
            }
        }

        public static void InitializeLogDirectory()
        {
            if (!Directory.Exists(folderPath()))
            {
                Directory.CreateDirectory(folderPath());
            }
        }

        private static string logFileName()
        {
            return System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\" + DEFAULT_LOG_FOLDER + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_TermKonfigLog.txt";
        }

        private static string folderPath()
        {
            return System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\" + DEFAULT_LOG_FOLDER;
        }
    }
}