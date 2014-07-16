using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;
using System.Data;
using System.IO;

namespace MonBattle.Admin
{
    public partial class Banner : System.Web.UI.Page
    {
        DataController dataController = new DataController();

        string BANNER_LOCATION = System.Configuration.ConfigurationManager.AppSettings["bannerLocation"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable bannerTable = dataController.getBanner();

                if (bannerTable.Rows.Count > 0)
                {
                    img_banner.ImageUrl = BANNER_LOCATION + bannerTable.Rows[0]["BannerID"].ToString() + "/" + bannerTable.Rows[0]["FileName"].ToString();
                    img_banner.PostBackUrl = bannerTable.Rows[0]["URL"].ToString();
                    img_banner.Visible = true;
                }
            }
        }

        protected void btn_uploadBanner_Click(object sender, EventArgs e)
        {
            string fileName;

            if (fileUpload_picture.HasFile && !String.IsNullOrEmpty(txt_link.Text))
            {
                fileName = fileUpload_picture.FileName;

                try
                {
                    int? bannerId = dataController.insertBanner(fileName, txt_link.Text);

                    if (bannerId != null)
                    {
                        string filePath = Server.MapPath(BANNER_LOCATION + (int)bannerId + "/");

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        fileUpload_picture.SaveAs(filePath + fileUpload_picture.FileName);
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SuccessfulInsert", "alert('Banner successfully uploaded.'); window.location = window.location;", true);
                }

                catch (Exception error)
                {
                    txt_link.Text = error.Message;
                }
            }
        }
    }
}