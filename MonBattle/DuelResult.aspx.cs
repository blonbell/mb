using MonBattle.Controllers;
using MonBattle.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DuelResult : System.Web.UI.Page
{

    public CharacterObject self, opponent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Results"] == null || Session["Opponent"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (Session["User"] == null)
        {
            Session["ErrorMessage"] = "You need to be logged in first";
            Response.Redirect("~/Login.aspx");
        }
        UserObject user = (UserObject)Session["User"];

        if (user.character == null)
        {
            Session["ErrorMessage"] = "You need to have a card character before battling.";
            Response.Redirect("~/CreateAChar.aspx");
        }
        self = (CharacterObject)user.character;
        opponent = (CharacterObject)Session["Opponent"];
        lblDamage.Text = "Damage Dealt: " + Session["Damage"].ToString();
        Session.Remove("Damage");
        imgSelf.ImageUrl = self.ImageUrl;
        imgOther.ImageUrl = opponent.ImageUrl;

        bool victory = (bool)Session["Results"];
        if (victory)
        {
            imgSelf.CssClass = imgSelf.CssClass + " victor";
            imgOther.CssClass = imgOther.CssClass + " loser";
            giveRewards(user.userId.Value, opponent.Reward);
            lblMessage.Text = "<p>You have won this battle and earned " + opponent.Reward + "points.</p>";
            user.points += opponent.Reward;
        }
        else
        {
            imgSelf.CssClass = imgSelf.CssClass + " loser";
            imgOther.CssClass = imgOther.CssClass + " victor";
            lblMessage.Text = "<p>Loss! The opponent was just too strong.</p>";
        }
        Session.Remove("Results");
        Session.Remove("Opponent");
    }

    private void giveRewards(int userId, int reward)
    {
        DataController controller = new DataController();
        controller.awardUser(userId, reward);
    }
}