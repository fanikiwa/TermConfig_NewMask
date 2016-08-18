using log4net;
using System;
using TermConfig_NewMask.App_Code;

namespace TermConfig_NewMask
{
    public partial class Error : BasePage
    {
        #region "properties"
        private static readonly ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion "properties"
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                log.Error(string.Format("Server Error : [{0}] ", ex.Message.ToString()));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                log.Error(string.Format("Server Error : [{0}] ", ex.Message.ToString()));
            }
        }


    }
}