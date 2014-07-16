using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MonBattle.Data;
using MonBattle.Controllers;

namespace MonBattle
{
    public class Global : System.Web.HttpApplication
    {
        DataController dataController = new DataController();

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            String email = HttpContext.Current.User.Identity.Name;

            if (!String.IsNullOrEmpty(email))
            {
                UserObject user = dataController.getUser(email);

                if (user != null)
                {
                    Session.Add("User", user);
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}