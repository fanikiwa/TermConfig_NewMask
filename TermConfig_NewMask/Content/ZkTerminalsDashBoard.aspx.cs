using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TermConfig_NewMask.App_Code;

namespace TermConfig_NewMask.Content
{
    public partial class ZkTerminalsDashBoard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn680tc_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Content/TM680tc.aspx");
        }

        protected void btn680bc_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Content/TM680bc.aspx");
        }

        protected void btn900bc_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Content/TM900bc.aspx");
        }
    }
}