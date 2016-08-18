using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Controllers
{
    public class TMKMain
    {
        public void RedirectToLoginPage()
        {
            new TermKonfigMain().CheckAuthCookie();
            HttpContext.Current.Response.Redirect("/Content/Login.aspx");
        }
        public void SetPromptRedirectPage(string redirectPage)
        {
            HttpContext.Current.Session.Add("Prompt_RedirectPage", redirectPage);
        }

        public void RedirectToPromptPage()
        {
            var redirectPage = HttpContext.Current.Session["Prompt_RedirectPage"];
            if (redirectPage == null) return;
            HttpContext.Current.Session.Remove("Prompt_RedirectPage");
            HttpContext.Current.Response.Redirect(redirectPage.ToString());
        }
    }
}