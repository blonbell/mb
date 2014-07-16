using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;
using System.Web.Security;

namespace MonBattle
{
    public partial class Login : System.Web.UI.Page
    {
        public string Email
        {
            get
            {
                return ViewState["Email"] as String;
            }
            set
            {
                ViewState["Email"] = value;
            }
        }

        public int? UserID
        {
            get
            {
                return ViewState["UserID"] as int?;
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }

        DataController dataController = new DataController();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Validates and logs in the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_login_Click(object sender, EventArgs e)
        {
            if (dataController.validateLogin(txt_email.Text, txt_password.Text))
            {
                UserObject user = dataController.getUser(txt_email.Text);
                if (user != null)
                {
                    // Check is user has a username, if not, then we force them to create one
                    if (!String.IsNullOrEmpty(user.username))
                    {
                        Session.Add("User", user);

                        FormsAuthentication.RedirectFromLoginPage(txt_email.Text, true);
                    }
                    else
                    {
                        Email = txt_email.Text;
                        UserID = user.userId;

                        pnl_username.Visible = true;
                        pnl_login.Visible = false;
                    }
                }
            }
            else
            {
                lbl_incorrectLogin.Visible = true;
            }
        }

        /// <summary>
        /// Create a username for the user when they log in and they have not yet created a username
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_createUsername_Click(object sender, EventArgs e)
        {
            if (UserID != null)
            {
                if (!dataController.usernameExists(txt_username.Text))
                {
                    if (dataController.updateUsername((int)UserID, txt_username.Text))
                    {
                        UserObject user = dataController.getUser(Email);
                        if (user != null)
                        {
                            Session.Add("User", user);

                            FormsAuthentication.RedirectFromLoginPage(txt_email.Text, true);
                        }

                        popupext_username.Show();
                    }
                }
                else
                {
                    popupext_usernameUsed.Show();
                }
            }
        }
    }
}