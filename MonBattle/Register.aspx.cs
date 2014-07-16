using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;

namespace MonBattle
{
    public partial class Register : System.Web.UI.Page
    {
        DataController dataController = new DataController();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Registers the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_register_Click(object sender, EventArgs e)
        {
            UserObject user = new UserObject();
            user.email = txt_email.Text;
            user.username = txt_username.Text;
            user.active = true;

            if (dataController.emailExists(user.email))
            {
                popupext_emailUsed.Show();
            }
            else if (dataController.usernameExists(user.username))
            {
                popupext_usernameUsed.Show();
            }
            else
            {
                int? successfulRegistration = dataController.insertUser(user, txt_password.Text, "");

                if (successfulRegistration != null)
                {
                    popupext_register.Show();
                }
            }
        }
    }
}