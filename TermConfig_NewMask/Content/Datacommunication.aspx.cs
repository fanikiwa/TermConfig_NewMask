using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TermConfig_NewMask.App_Code;

namespace TermConfig_NewMask.Content
{
    public partial class Datacommunication : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Index.aspx");
        }
    }
}