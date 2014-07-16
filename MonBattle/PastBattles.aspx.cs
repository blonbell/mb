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
    public partial class PastBattles : System.Web.UI.Page
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
            int? userId = null;

            if (Session["User"] != null)
            {
                UserObject user = (UserObject)Session["User"];
                userId = user.userId;
            }

            DataTable cardBattlesTable = dataController.getPastCardBattles(userId);

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
                //HyperLink hyplink_cardBattle = (HyperLink)e.Row.FindControl("hyplink_cardBattle");
                //int cardBattleId = Convert.ToInt32(hyplink_cardBattle.Text);

                    //Label lbl_winner = (Label)e.Row.FindControl("lbl_winner");
                    //if (!String.IsNullOrEmpty(lbl_winner.Text))
                    //{
                    //    if (lbl_winner.Text == "0")
                    //    {
                    //        lbl_winner.Text = "TIE";
                    //    }
                    //    else
                    //    {
                    //        Label lbl_cardOne = (Label)e.Row.FindControl("lbl_cardOne");
                    //        Label lbl_cardTwo = (Label)e.Row.FindControl("lbl_cardTwo");

                    //        if (lbl_cardOne.Text == lbl_winner.Text)
                    //        {
                    //            lbl_winner.Text = "Card One";
                    //            HyperLink hyplink_cardOne = (HyperLink)e.Row.FindControl("hyplink_cardOne");
                    //            hyplink_cardOne.CssClass = "grid-battles-winner";
                    //        }

                    //        else
                    //        {
                    //            lbl_winner.Text = "Card Two";
                    //            HyperLink hyplink_cardTwo = (HyperLink)e.Row.FindControl("hyplink_cardTwo");
                    //            hyplink_cardTwo.CssClass = "grid-battles-winner";
                    //        }
                    //    }
                    //    lbl_winner.Visible = true;
                //}
            }
        }
    }
}