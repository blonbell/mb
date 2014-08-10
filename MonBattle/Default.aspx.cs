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
    public partial class Vote : System.Web.UI.Page
    {
        DataController dataController = new DataController();

        string IMAGE_OPEN_LOCATION = System.Configuration.ConfigurationManager.AppSettings["imagesOpenLocation"];

        const string clockURL = "http://free.timeanddate.com/countdown/i453v2zd/n256/cf12/cm0/cu4/ct1/cs1/ca0/co0/cr0/ss0/cac333/cpc333/pct/tcfff/fs100/szw576/szh243/iso";

        UserObject user;

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

        CardBattleObject cardBattle;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadBattle();
            }
        }

        private void loadBattle()
        {
            cardBattle = dataController.getCardBattleToday();

            if (cardBattle != null)
            {
                cardBattleID = (int)cardBattle.cardBattleId;

                lbl_cardOne.Text = cardBattle.cardOne.name;
                imgbtn_cardOne.ImageUrl = IMAGE_OPEN_LOCATION + (int)cardBattle.cardOne.cardId + "/" + cardBattle.cardOne.imageURL;
                imgbtn_cardOne.ToolTip = cardBattle.cardOne.description;
                imgbtn_cardOne.Visible = true;

                imgbtn_cardTwo.ImageUrl = IMAGE_OPEN_LOCATION + (int)cardBattle.cardTwo.cardId + "/" + cardBattle.cardTwo.imageURL;
                lbl_cardTwo.Text = cardBattle.cardTwo.name;
                imgbtn_cardTwo.ToolTip = cardBattle.cardTwo.description;
                imgbtn_cardTwo.Visible = true;

                img_vs.Visible = true;

                if (Session["User"] != null)
                {
                    user = (UserObject)Session["User"];

                    if (dataController.checkIfVotedToday((int)user.userId, (int)cardBattle.cardBattleId))
                    {
                        imgbtn_cardOne.OnClientClick = "return false;";
                        imgbtn_cardTwo.OnClientClick = "return false;";
                        conbtnext_cardOne.Enabled = false;
                        conbtnext_cardTwo.Enabled = false;


                        lbl_voteMessage.Text = "You have voted today!";
                    }
                }
                else
                {
                    imgbtn_cardOne.OnClientClick = "return false;";
                    imgbtn_cardTwo.OnClientClick = "return false;";
                    conbtnext_cardOne.Enabled = false;
                    conbtnext_cardTwo.Enabled = false;

                    lbl_voteMessage.Visible = false;
                    tblcell_login.Visible = true;
                }

                DateTime battleDate = (DateTime)cardBattle.battleDate;
                DateTime battleDateEnd = battleDate.AddDays(1);

                countDownClock.Attributes["src"] = clockURL + battleDateEnd.ToString("yyyy-MM-dd") + "T00:00:00";
                countDownClock.Visible = true;
                countDownClock.Attributes.Add("onclick", "return false;");
                //2014-05-08T00:00:00"
            }
            else
            {
                lbl_message.Text = "There is no battle today.";
                lbl_message.Visible = true;

                lbl_voteMessage.Visible = false;

                lbl_cardOne.Visible = false;
                lbl_cardTwo.Visible = false;
            }

            CardBattleObject cardBattleTomorrow = dataController.getCardBattleTomorrow();

            if (cardBattleTomorrow != null)
            {
                int cardBattleIDTomorrow = (int)cardBattleTomorrow.cardBattleId;

                //lbl_cardOne.Text = cardBattleTomorrow.cardOne.name;
                img_cardOneTomorrow.ImageUrl = IMAGE_OPEN_LOCATION + (int)cardBattleTomorrow.cardOne.cardId + "/" + cardBattleTomorrow.cardOne.imageURL;
                img_cardOneTomorrow.ToolTip = cardBattleTomorrow.cardOne.description;
                img_cardOneTomorrow.Visible = true;

                //lbl_cardTwo.Text = cardBattleTomorrow.cardTwo.name;
                img_cardTwoTomorrow.ImageUrl = IMAGE_OPEN_LOCATION + (int)cardBattleTomorrow.cardTwo.cardId + "/" + cardBattleTomorrow.cardTwo.imageURL;
                img_cardTwoTomorrow.ToolTip = cardBattleTomorrow.cardTwo.description;
                img_cardTwoTomorrow.Visible = true;

                tbl_tomorrowBattle.Visible = true;
            }

        }

        protected void imgbtn_cardOne_Click(object sender, ImageClickEventArgs e) {
            cardBattle = dataController.getCardBattleToday();
            voteCard((int) cardBattle.cardOne.cardId);

        }

        protected void imgbtn_cardTwo_Click(object sender, ImageClickEventArgs e) {
            cardBattle = dataController.getCardBattleToday();
            voteCard((int) cardBattle.cardTwo.cardId);
        }

        private void voteCard(int cardId) {
            if (Session["User"] != null) {
                user = (UserObject)Session["User"];
                if (cardBattle != null && (int)cardBattle.cardBattleId == cardBattleID) {
                    bool succ = dataController.insertCardPick((int)user.userId, (int)cardBattle.cardBattleId, cardId);
                    if (succ) {
                        lbl_popupMessage.Text = "Thank you for voting!";
                        popupext_vote.Show();
                    } else {
                        lbl_popupMessage.Text = "There was an error with your vote.  Please contact an admin for support.";
                        popupext_vote.Show();
                    }
                } else {
                    lbl_popupMessage.Text = "This battle is no longer available for voting.";
                    popupext_vote.Show();
                }
            } else {
                Response.Redirect("Default.aspx");
            }
        }
    }
}