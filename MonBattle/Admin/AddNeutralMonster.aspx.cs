using MonBattle.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonBattle.Admin {
    public partial class AddNeutralMonster : System.Web.UI.Page {

        string IMAGE_SAVE_LOCATION = ConfigurationManager.AppSettings["charImagesSaveLocation"] + "neutral/";
        DataController controller = new DataController();

        protected void Page_Load(object sender, EventArgs e) {
            lblHelp.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if(!imgUpload.HasFile) {
                lblHelp.Text = "Please select an image.";
                return;
            }
            string fileType = System.IO.Path.GetExtension(imgUpload.FileName).ToString().ToLower();
                if (!fileType.Equals(".jpg") && !fileType.Equals(".jpeg") && !fileType.Equals(".png") && !fileType.Equals(".gif")) {
                lblHelp.Text = "Please select a jpg, png or gif image.";
                return;
            }
            string fixedFileName = imgUpload.PostedFile.FileName.ToLower().Replace(" ","");
            string filePath = Server.MapPath(IMAGE_SAVE_LOCATION + fixedFileName);
            imgUpload.PostedFile.SaveAs(filePath);

            controller.addNeutralCharacter(txtName.Text, 
                                           txtAttack.Text, 
                                           txtHealth.Text, 
                                           txtSpeed.Text, 
                                           txtReward.Text,
                                           IMAGE_SAVE_LOCATION + fixedFileName);
            Response.Redirect("~/Admin/NeutralCharacterListing.aspx");
        }
    }
}