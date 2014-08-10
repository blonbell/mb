using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Controllers;
using System.Net.Mail;
using System.Net;

namespace MonBattle
{
    public partial class Activate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                string activationCode = Request.QueryString["ID"].ToString();

                // Some quick dirty checks to prevent SQL injection before we check if the query string is a valid activation code
                if (!activationCode.Contains("'") && !activationCode.Contains(";") && !activationCode.Contains("--") && !activationCode.Contains(" "))
                {
                    Guid guid = new Guid(activationCode);

                    if (Request.QueryString["Rec"] == null)
                    {
                        DataController dataController = new DataController();
                        int? userId = dataController.getUserIDFromUserVerification(guid, false);

                        if (userId != null)
                        {
                            bool successfulUpdate = dataController.updateUserStatus((int)userId, true);

                            if (successfulUpdate)
                            {
                                Session["UserID"] = userId;
                                Response.Redirect("Welcome.aspx");
                            }
                        }
                    }
                }
            }
        }
    }
}