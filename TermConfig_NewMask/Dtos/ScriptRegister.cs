using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TermConfig_NewMask.Dtos
{
    public static class ScriptRegister
    {
        public static void RegisterSomeScript(Page page)
        {
            var title = GetLocalizedText("duplicateTerminalID");
            var message = GetLocalizedText("duplicateTerminalID");
            page.ClientScript.RegisterStartupScript(page.GetType(), "ImportantInfoDialogPrompt", "ImportantInfoDialogPrompt('" + title + "', '" + message + "');", true);
        }

        [System.Web.Services.WebMethod]
        public static string GetLocalizedText(string key)
        {
            var culture = HttpContext.Current.Session["PreferredCulture"].ToString();
            var cultureInfo = new CultureInfo(culture);

            var text = (string)HttpContext.GetGlobalResourceObject("LocalizedText", key, cultureInfo);
            return text;
        }
    }
}