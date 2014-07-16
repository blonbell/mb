using MonBattle.Controllers;
using MonBattle.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonBattle
{
    public partial class CreateAChar : System.Web.UI.Page
    {

        string IMAGE_SAVE_LOCATION = System.Configuration.ConfigurationManager.AppSettings["charImagesSaveLocation"];
        DataController controller = new DataController();
        UserObject user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Session["ErrorMessage"] = "You need to be logged in first";
                Response.Redirect("~/Login.aspx");
            }

            user = (UserObject)Session["User"];
            if (user.character != null)
            {
                /*
                 * Show a sorry image with the text.
                 */
                Session["ErrorMessage"] = "You may only have one card Character at this moment.";
                formPanel.Visible = false;
                previewPanel.Visible = false;
            }
        }

        /**
         *  Functionality of the Preview Button.
         *  - Allows only .jpg, .png, .gif file types
         *  - Allows only 500kB (roughly 550000)
         *  - Saves the FileUpload object to session because postback clears Fileupload control.
         */
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (!imgUpload.HasFile)
            {
                lblHelp.Text = "Please select an image.";
                return;
            }
            string fileType = System.IO.Path.GetExtension(imgUpload.FileName).ToString().ToLower();
            if (!fileType.Equals(".jpg") && !fileType.Equals(".jpeg") && !fileType.Equals(".png") && !fileType.Equals(".gif"))
            {
                lblHelp.Text = "Please select a jpg, png or gif image.";
                return;
            }
            if (imgUpload.FileBytes.Length > 750000)
            {
                lblHelp.Text = "The image must be smaller than 750kb";
                return;
            }
            // ASP will clear FileUpload on postback for security reasons.
            Session["fileUpload"] = imgUpload;

            byte[] imgBytes = convertImageBytes();
            string base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
            imgCard.ImageUrl = "data:image/png;base64," + base64String;

            lblPrevName.Text = txtName.Text;
            formPanel.Visible = false;
            previewPanel.Visible = true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Session.Remove("fileUpload");
            formPanel.Visible = true;
            previewPanel.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FileUpload savedContents = (FileUpload)Session["fileUpload"];
            string fileName = System.Guid.NewGuid().ToString();
            string fileType = System.IO.Path.GetExtension(savedContents.FileName).ToString().ToLower();
            string filePath = Server.MapPath(IMAGE_SAVE_LOCATION + fileName + fileType);
            savedContents.PostedFile.SaveAs(filePath);

            int charId = controller.addCharacter(user.userId.Value, lblPrevName.Text, IMAGE_SAVE_LOCATION + fileName + fileType);
            controller.attachUserCharacterObject(user);
            Session["ErrorMessage"] = "Your monster " + lblPrevName.Text + " has been created!";
            Response.Redirect("~/ViewCharacter.aspx?charID=" + charId, true);
        }

        private byte[] convertImageBytes()
        {
            FileUpload img = (FileUpload)imgUpload;
            byte[] imgBytes = null;
            if (img.HasFile && img.PostedFile != null)
            {
                HttpPostedFile imageFile = imgUpload.PostedFile;

                Stream fs = imgUpload.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                imgBytes = br.ReadBytes((Int32)fs.Length);
            }
            return imgBytes;
        }
    }
}