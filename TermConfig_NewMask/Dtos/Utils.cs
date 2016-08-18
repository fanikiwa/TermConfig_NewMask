using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;

namespace TermConfig_NewMask
{
    public static class Utils
    {
        private static readonly ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteToErrorLogFile(Exception ex)
        {
            string msg = ex.Message;
            if (ex.InnerException != null) msg += ("\n" + ex.InnerException.Message);
            if (!string.IsNullOrEmpty(ex.StackTrace)) msg += ("\nTrace = " + ex.StackTrace);
            log.Error(string.Format("Server Error : [{0}] ", msg));
        }

        #region Resources
        /// <summary>
        /// Takes the full name of a resource and loads it in to a stream.
        /// </summary>
        /// <param name="resourceName">Assuming an embedded resource is a file
        /// called info.png and is located in a folder called Resources, it
        /// will be compiled in to the assembly with this fully qualified
        /// name: Full.Assembly.Name.Resources.info.png. That is the string
        /// that you should pass to this method.</param>
        /// <returns></returns>
        public static Stream GetEmbeddedResourceStream(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        /// <summary>
        /// Get the list of all emdedded resources in the assembly.
        /// </summary>
        /// <returns>An array of fully qualified resource names</returns>
        public static string[] GetEmbeddedResourceNames()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceNames();
        }

        public static string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        #endregion

        #region  "Helpers"
        public static string ConvertFirstLetterToUpper(string s)
        {
            try
            {
                System.Globalization.CultureInfo c = new System.Globalization.CultureInfo("en-us", false);
                System.Globalization.TextInfo t = c.TextInfo;

                return t.ToTitleCase(s);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public static bool IsConnectedToInternet()
        {
            try
            {
                WebClient client = new WebClient();
                if (client.DownloadString("http://www.google.com/") != null)
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static string GetMACAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return sMacAddress;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        #endregion


    }
}