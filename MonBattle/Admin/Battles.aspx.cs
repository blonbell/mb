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
    public partial class Battles : System.Web.UI.Page
    {
        DataController dataController = new DataController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridData();
            }
        }

        /// <summary>
        /// Gets card battles from database and populates grid
        /// </summary>
        private void bindGridData()
        {
            DataTable cardBattlesTable = dataController.getCardBattles();

            grid_battles.DataSource = cardBattlesTable;
            grid_battles.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_battles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyplink_cardBattle = (HyperLink)e.Row.FindControl("hyplink_cardBattle");
                int cardBattleId = Convert.ToInt32(hyplink_cardBattle.Text);

                if (dataController.checkIfBattleResultCanAnnounce(cardBattleId))
                {
                    Button btn_calculateWinner = (Button)e.Row.FindControl("btn_calculateWinner");
                    btn_calculateWinner.Visible = true;
                }

                else
                {
                    Label lbl_winner = (Label)e.Row.FindControl("lbl_winner");
                    if (!String.IsNullOrEmpty(lbl_winner.Text))
                    {
                        if (lbl_winner.Text == "0")
                        {
                            lbl_winner.Text = "TIE";
                        }
                        else
                        {
                            Label lbl_cardOne = (Label)e.Row.FindControl("lbl_cardOne");
                            Label lbl_cardTwo = (Label)e.Row.FindControl("lbl_cardTwo");

                            if (lbl_cardOne.Text == lbl_winner.Text)
                            {
                                lbl_winner.Text = "Card One";
                                HyperLink hyplink_cardOne = (HyperLink)e.Row.FindControl("hyplink_cardOne");
                                hyplink_cardOne.CssClass = "grid-battles-winner";
                            }

                            else
                            {
                                lbl_winner.Text = "Card Two";
                                HyperLink hyplink_cardTwo = (HyperLink)e.Row.FindControl("hyplink_cardTwo");
                                hyplink_cardTwo.CssClass = "grid-battles-winner";
                            }
                        }
                        lbl_winner.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the winner for the battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_calculateWinner_Command(object sender, CommandEventArgs e)
        {
            int cardBattleId = Convert.ToInt32(e.CommandArgument.ToString());

            if ((bool)dataController.updateCardBattleVotesAndWinner(cardBattleId))
            {
                CardBattleObject cardBattle = dataController.getCardBattle(cardBattleId);

                if ((bool)dataController.updateUserPointsFromBattle(cardBattleId, (int)cardBattle.winnerCardId))
                {
                    bindGridData();
                    popupext_calculateWinner.Show();
                }
            }
        }
    }
}