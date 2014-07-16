using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;
using System.Data;

namespace MonBattle
{
    
    public partial class MonBattle1 : System.Web.UI.MasterPage
    {
        public UserObject user;
        string BANNER_LOCATION = System.Configuration.ConfigurationManager.AppSettings["bannerLocation"];

        DataController dataController = new DataController();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable bannerTable = dataController.getBanner();

            if (bannerTable.Rows.Count > 0)
            {
                img_banner.ImageUrl = BANNER_LOCATION + bannerTable.Rows[0]["BannerID"].ToString() + "/" + bannerTable.Rows[0]["FileName"].ToString();
                img_banner.PostBackUrl = bannerTable.Rows[0]["URL"].ToString();
                div_banner.Visible = true;
                img_banner.Visible = true;
            }

            if (Session["User"] != null)
            {
                user = (UserObject)Session["User"];


                if ((bool)user.isAdmin)
                {
                    div_admin.Visible = true;
                }


                navbar_in.Visible = true;
                navbar_out.Visible = false;

                //UserObject user = (UserObject)Session["User"];

                //btn_shop.Text = "Shop (" + user.points + " pts)";

                //if ((bool)user.isAdmin)
                //{
                //    div_admin.Visible = true;
                //}

                //btn_logout.Visible = true;
            }
            else
            {
                navbar_in.Visible = false;
                navbar_out.Visible = true;

                //btn_login.Visible = true;
                //btn_register.Visible = true;
            }

            if (Session["ErrorMessage"] != null)
            {
                lblError.Text = (String)Session["ErrorMessage"];
                Session.Remove("ErrorMessage");
                errorPanel.Visible = true;
            }
            else
            {
                errorPanel.Visible = false;
            }
        }
    }
}