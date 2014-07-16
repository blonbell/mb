using MonBattle.Controllers;
using MonBattle.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonBattle
{
    public partial class Battle : System.Web.UI.Page
    {

        public UserObject user;
        CharacterObject self = null;
        const int row_size = 6;
        CharacterObject[] opp = new CharacterObject[row_size];
        DataController controller = new DataController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Session["ErrorMessage"] = "You need to be logged in first";
                Response.Redirect("~/Login.aspx");
            }
            user = (UserObject)Session["User"];

            //update the battle counter from db
            user.battleCounter = controller.refreshUserBattleCount(user.userId.Value);

            lblBCounter.Text = "Battles Left: " + user.battleCounter;
            if (user.character == null)
            {
                Session["ErrorMessage"] = "You can create a Card Character here first before battling.";
                Response.Redirect("~/CreateAChar.aspx");
            }
            self = user.character;

            //update user's battlec counter in case they have not done a fresh login
            //refresh user object
            //controller.updateBattleCounter(user);

            /**
             *  Get a list of opponents.
             */
            opp = controller.getOpponentsList(row_size);
            for (int x = 0; x < row_size; x = x + 2)
            {
                Panel row = new Panel();
                row.CssClass = "row";
                for (int y = 0; y < 2 && (x + y < row_size); y++)
                {
                    int index = x + y;
                    if (opp[index] != null)
                    {
                        Panel oppItem = new Panel();
                        oppItem.ID = "OPP" + index;
                        oppItem.CssClass = "opp-item";

                        LiteralControl name = new LiteralControl("<p class='big-font'>" + opp[index].Name + "</p>");
                        ImageButton imb = new ImageButton();
                        imb.ID = "OPPImg" + index;
                        imb.Attributes["idx"] = Convert.ToString(index);
                        imb.CssClass = "xsprint";
                        imb.ImageUrl = opp[index].ImageUrl;
                        imb.AlternateText = "Character Image";
                        imb.Click += this.btnAtk_Click;

                        String atk = "?"; //opp[index].Attack.ToString();
                        Panel atkLine = createStatisticLine(atk, "~/images/Attack-Icon.png");

                        String hp = "?/?"; //opp[index].Health + "/" + opp[index].MaxHealth;
                        Panel hpLine = createStatisticLine(hp, "~/images/HP-Icon.png");

                        String spd = opp[index].Speed.ToString();
                        Panel spdLine = createStatisticLine(spd, "~/images/Speed-Icon.png");

                        Label lblReward = new Label();
                        lblReward.Text = "Reward: " + opp[index].Reward + "MP";
                        lblReward.CssClass = "pad-left-5";

                        Panel statBar = new Panel();
                        statBar.CssClass = "stat-bar";
                        statBar.Controls.Add(atkLine);
                        statBar.Controls.Add(hpLine);
                        statBar.Controls.Add(spdLine);

                        oppItem.Controls.Add(name);
                        oppItem.Controls.Add(imb);
                        oppItem.Controls.Add(statBar);
                        oppItem.Controls.Add(lblReward);
                        row.Controls.Add(oppItem);
                    }
                }
                oppContainer.Controls.Add(row);
            }
        }

        void btnAtk_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            int index = Convert.ToInt32(btn.Attributes["idx"]);
            Session["Opponent"] = opp[index];
            Response.Redirect("~/Duel.aspx");
        }

        private Panel createStatisticLine(String text, String imageUrl)
        {
            Image icon = new Image();
            icon.ImageUrl = imageUrl;
            icon.CssClass = "small-icon";
            Label lbl = new Label();
            lbl.Text = text;
            lbl.CssClass = "pad-left-5";
            Panel divLine = new Panel();
            divLine.Controls.Add(icon);
            divLine.Controls.Add(lbl);
            return divLine;
        }
    }
}