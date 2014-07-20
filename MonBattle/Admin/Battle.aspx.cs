using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;
using System.Data;

namespace MonBattle.Admin
{
    public partial class Battle : System.Web.UI.Page
    {
        DataController dataController = new DataController();

        string IMAGE_OPEN_LOCATION = System.Configuration.ConfigurationManager.AppSettings["imagesOpenLocation"];

        private int cardBattleID
        {
            get
            {
                if (ViewState["CardBattleID"] != null)
                {
                    return (Int32)ViewState["CardBattleID"];
                }
                return -1;
            }
            set
            {
                ViewState["CardBattleID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateDropDownData();

                if (Request.QueryString["ID"] != null)
                {
                    loadBattle();
                }
                else
                {
                    loadNewBattle();
                }
            }
        }

        private void loadBattle()
        {
            int validCardBattleId;
            if (Int32.TryParse(Request.QueryString["ID"].ToString(), out validCardBattleId))
            {
                CardBattleObject cardBattle = dataController.getCardBattle(validCardBattleId);

                if (cardBattle != null)
                {
                    cardBattleID = (int)cardBattle.cardBattleId;

                    if (cardBattle.battleDate != null)
                    {
                        DateTime battleDate = (DateTime)cardBattle.battleDate;

                        txt_battleDate.Text = battleDate.ToString("MMM dd, yyyy");
                    }

                    drpdwn_cardOne.SelectedValue = cardBattle.cardOne.cardId.ToString();
                    drpdwn_cardTwo.SelectedValue = cardBattle.cardTwo.cardId.ToString();

                    img_cardOne.ImageUrl = IMAGE_OPEN_LOCATION + (int)cardBattle.cardOne.cardId + "/" + cardBattle.cardOne.imageURL;
                    img_cardOne.Visible = true;

                    img_cardTwo.ImageUrl = IMAGE_OPEN_LOCATION + (int)cardBattle.cardTwo.cardId + "/" + cardBattle.cardTwo.imageURL;
                    img_cardTwo.Visible = true;


                    // If current date is >= battle date, do not allow update
                    if (cardBattle.battleDate != null && DateTime.Today.Date >= cardBattle.battleDate)
                    {
                        txt_battleDate.Enabled = false;
                        calext_battleDate.Enabled = false;
                        drpdwn_cardOne.Enabled = false;
                        drpdwn_cardTwo.Enabled = false;

                        if (cardBattle.cardOneVotes != null)
                        {
                            lbl_cardOneVotes.Text = cardBattle.cardOneVotes.ToString() + " votes";
                            lbl_cardOneVotes.Visible = true;
                        }

                        if (cardBattle.cardTwoVotes != null)
                        {
                            lbl_cardTwoVotes.Text = cardBattle.cardTwoVotes.ToString() + " votes";
                            lbl_cardTwoVotes.Visible = true;
                        }

                        Dictionary<string, List<CardVoterObject>> voterMap = dataController.
                            getCardBattleVoters(cardBattleID);

                        string listOne = prepareVoterList(cardBattle.cardOne.name, voterMap);
                        ltrCardOneVoters.Text = "<ul>" + listOne + "</ul>";

                        string listTwo = prepareVoterList(cardBattle.cardTwo.name, voterMap);
                        ltrCardTwoVoters.Text = "<ul>" + listTwo + "</ul>";
                    }
                    else
                    {
                        btn_addBattle.Text = "Update Battle";
                        btn_addBattle.Visible = true;
                    }

                }
                else
                {
                    Response.Redirect("Battle.aspx");
                }
            }
        }

        private string prepareVoterList(string name, Dictionary<string, List<CardVoterObject>> voterMap) {
            List<CardVoterObject> voters = new List<CardVoterObject>();
            try{
                voters = voterMap[name];
            } catch(KeyNotFoundException ex) {
                return "<li>Error!, Cannot produce list</li>";
            }
            string list = "";
            foreach (CardVoterObject voter in voters) {
                list += "<li title='" + voter.email + "," + voter.username + "'>" + voter.username + "</li>";
            }
            return list;
        }

        private void loadNewBattle()
        {

        }

        private void populateDropDownData()
        {
            DataTable cardsList = dataController.getCards();
            drpdwn_cardOne.DataSource = cardsList;
            drpdwn_cardOne.DataTextField = "Name";
            drpdwn_cardOne.DataValueField = "CardID";
            drpdwn_cardOne.DataBind();

            drpdwn_cardTwo.DataSource = cardsList;
            drpdwn_cardTwo.DataTextField = "Name";
            drpdwn_cardTwo.DataValueField = "CardID";
            drpdwn_cardTwo.DataBind();
        }

        /// <summary>
        /// Loads the picture of card one selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpdwn_cardOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpdwn_cardOne.SelectedValue != "-1")
            {
                int cardId = Convert.ToInt32(drpdwn_cardOne.SelectedValue);
                CardObject card = dataController.getCard(cardId);
                if (!String.IsNullOrEmpty(card.imageURL))
                {
                    img_cardOne.ImageUrl = IMAGE_OPEN_LOCATION + (int)card.cardId + "/" + card.imageURL;
                    img_cardOne.Visible = true;

                    if (drpdwn_cardTwo.SelectedValue != "-1")
                    {
                        btn_addBattle.Visible = true;
                    }
                }
            }
            else
            {
                img_cardOne.ImageUrl = "";
                img_cardOne.Visible = false;
                btn_addBattle.Visible = false;
            }
        }

        /// <summary>
        /// Loads the picture of card two selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpdwn_cardTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpdwn_cardTwo.SelectedValue != "-1")
            {
                int cardId = Convert.ToInt32(drpdwn_cardTwo.SelectedValue);
                CardObject card = dataController.getCard(cardId);
                if (!String.IsNullOrEmpty(card.imageURL))
                {
                    img_cardTwo.ImageUrl = IMAGE_OPEN_LOCATION + (int)card.cardId + "/" + card.imageURL;
                    img_cardTwo.Visible = true;

                    if (drpdwn_cardOne.SelectedValue != "-1")
                    {
                        btn_addBattle.Visible = true;
                    }
                }
            }
            else
            {
                img_cardTwo.ImageUrl = "";
                img_cardTwo.Visible = false;
                btn_addBattle.Visible = false;
            }
        }

        /// <summary>
        /// Adds or updates a battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_addBattle_Click(object sender, EventArgs e)
        {
            CardBattleObject cardBattle = new CardBattleObject();

            cardBattle.cardBattleId = cardBattleID;
            cardBattle.cardOne = dataController.getCard(Convert.ToInt32(drpdwn_cardOne.SelectedValue));
            cardBattle.cardTwo = dataController.getCard(Convert.ToInt32(drpdwn_cardTwo.SelectedValue));
            cardBattle.battleDate = DateTime.ParseExact(txt_battleDate.Text, "MMM dd, yyyy", null);

            if (dataController.checkIfBattleDateAvailable((DateTime)cardBattle.battleDate))
            {
                if (cardBattleID == -1)
                {
                    int? newCardBattleId = dataController.insertCardBattle(cardBattle);

                    if (newCardBattleId != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SuccessfulInsert", "alert('Battle successfully created.'); window.location='Battle.aspx?ID=" + (int)newCardBattleId + "';", true);
                    }
                }

                else
                {
                    bool? successfulUpdate = dataController.updateCardBattle(cardBattle);

                    if (successfulUpdate != null && (bool)successfulUpdate)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SuccessfulInsert", "alert('Battle successfully updated.'); window.location='Battle.aspx?ID=" + (int)cardBattle.cardBattleId + "';", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrorInsert", "alert('Another battle is already scheduled on the selected date.');", true);
            }
        }
    }
}