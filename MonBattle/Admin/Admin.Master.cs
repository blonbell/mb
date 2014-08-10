using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;

namespace MonBattle.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                UserObject user = (UserObject)Session["User"];
                if (!(bool)user.isAdmin)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }

            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}