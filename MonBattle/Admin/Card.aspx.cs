using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;
using System.IO;

namespace MonBattle.Admin
{
    public partial class Card : System.Web.UI.Page
    {
        DataController dataController = new DataController();

        string IMAGE_SAVE_LOCATION = System.Configuration.ConfigurationManager.AppSettings["imagesSaveLocation"];

        string IMAGE_OPEN_LOCATION = System.Configuration.ConfigurationManager.AppSettings["imagesOpenLocation"];

        private int cardID
        {
            get
            {
                if (ViewState["CardID"] != null)
                {
                    return (Int32)ViewState["CardID"];
                }
                return -1;
            }
            set
            {
                ViewState["CardID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    loadCard();
                }
                else
                {
                    loadNewCard();
                }
            }
        }

        /// <summary>
        /// Loads info of card
        /// </summary>
        private void loadCard()
        {
            int validCardId;
            if (Int32.TryParse(Request.QueryString["ID"].ToString(), out validCardId))
            {
                CardObject card = dataController.getCard(validCardId);

                if (card != null)
                {
                    cardID = (int)card.cardId;

                    txt_name.Text = card.name;
                    txt_description.Text = card.description;

                    if (!String.IsNullOrEmpty(card.imageURL))
                    {
                        img_card.ImageUrl = IMAGE_OPEN_LOCATION + (int)card.cardId + "/" + card.imageURL;
                        img_card.Visible = true;
                    }

                    btn_updateCard.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Card.aspx");
            }
        }

        /// <summary>
        /// Loads controls for new card
        /// </summary>
        private void loadNewCard()
        {
            btn_createCard.Visible = true;
        }

        /// <summary>
        /// Create a new card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_createCard_Click(object sender, EventArgs e)
        {
            CardObject card = new CardObject();
            card.name = txt_name.Text;
            card.description = txt_description.Text;
            if (fileUpload_picture.HasFile)
            {
                card.imageURL = fileUpload_picture.FileName;
            }

            try
            {
                int? cardId = dataController.insertCard(card);

                if (cardId != null)
                {
                    if (fileUpload_picture.HasFile)
                    {
                        string filePath = Server.MapPath(IMAGE_SAVE_LOCATION + (int)cardId + "/");

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        fileUpload_picture.SaveAs(filePath + fileUpload_picture.FileName);
                    }   
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SuccessfulInsert", "alert('Card successfully created.'); window.location='Card.aspx?ID=" + (int)cardId + "';", true);
            }

            catch (Exception error)
            {

            }
        }

        /// <summary>
        /// Updates the card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_updateCard_Click(object sender, EventArgs e)
        {
            CardObject card = new CardObject();
            card.cardId = cardID;
            card.name = txt_name.Text;
            card.description = txt_description.Text;
            if (fileUpload_picture.HasFile)
            {
                card.imageURL = fileUpload_picture.FileName;
            }
            else
            {
                CardObject currentCard = dataController.getCard(cardID);

                card.imageURL = currentCard.imageURL;
            }

            try
            {
                bool? updateSuccessful = dataController.updateCard(card);

                if (updateSuccessful != null && (bool)updateSuccessful)
                {
                    if (fileUpload_picture.HasFile)
                    {
                        string filePath = Server.MapPath(IMAGE_SAVE_LOCATION + (int)card.cardId + "/");

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        fileUpload_picture.SaveAs(filePath + fileUpload_picture.FileName);
                    }
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SuccesfulUpdate", "alert('Card successfully updated.'); window.location='Card.aspx?ID=" + (int)card.cardId + "';", true);
            }

            catch (Exception error)
            {

            }
        }
    }
}