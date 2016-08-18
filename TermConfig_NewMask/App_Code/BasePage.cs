using log4net;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace TermConfig_NewMask.App_Code
{
    public class BasePage : System.Web.UI.Page
    { 
        protected override void InitializeCulture()
        {
            try
            {
                string previousCulture = Convert.ToString(CultureInfo.CurrentCulture);                  //Get Previous Culture
                string previousUICulture = Convert.ToString(CultureInfo.CurrentUICulture);          //Get Previous UI Culture

                string culture = (Session["PreferredCulture"] == null) ? "" : Session["PreferredCulture"].ToString();               //If There's no Preferred Culture in Session, String Empty, Otherwise get the culture from the session

                if (!string.Equals(culture, "previousCulture") && !string.Equals(culture, ""))                          //If culture In Session is not as the previousCulture. Change
                {
                    Culture = culture;
                    UICulture = culture;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                }
                else                                                //Maintain Previous Culture
                {
                    Culture = previousCulture;
                    UICulture = previousUICulture;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(previousCulture);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(previousUICulture);
                }

                base.InitializeCulture();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }              //Every form that derives this Class, Set the culture as above
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxWebControl.RegisterBaseScript(this);
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
    }
}