using MonBattle.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonBattle.Admin {
    public partial class AddMoveCatalog : System.Web.UI.Page {

        string IMAGE_SAVE_LOCATION = System.Configuration.ConfigurationManager.AppSettings["moveImagesSaveLocation"];

        protected void Page_Load(object sender, EventArgs e) {
            GridView1.DataBind();

            string dirPath = Server.MapPath(IMAGE_SAVE_LOCATION);
            if(!Directory.Exists(dirPath)) {
                Directory.CreateDirectory(dirPath);
            }
        }

        protected void btnAddMove_Click(object sender, EventArgs e) {
            if (!imgUpload.HasFile) {
                return;
            }

            string fileType = System.IO.Path.GetExtension(imgUpload.FileName).ToString().ToLower();
            if (!fileType.Equals(".jpg") && !fileType.Equals(".jpeg") && !fileType.Equals(".png") && !fileType.Equals(".gif")) {
                return;
            }

            //string fileName = System.Guid.NewGuid().ToString();
            //string fileType = System.IO.Path.GetExtension(savedContents.FileName).ToString().ToLower();
            string filePath = Server.MapPath(IMAGE_SAVE_LOCATION + imgUpload.FileName);
            imgUpload.PostedFile.SaveAs(filePath);

            DataController dc = new DataController();

            dc.addMove(
                txtName.Text,
                txtDescription.Text,
                txtTurn.Text,
                lingerList.SelectedValue,
                txtMeterCost.Text,
                txtCommandStr.Text,
                txtRedeemCost.Text,
                filePath); 
        }

        protected void btnRemoveMove_Click(object sender, EventArgs e) {
            DataController dc = new DataController();
            dc.deleteMove(txtMoveId.Text);
        }
    }
}