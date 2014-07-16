using MonBattle.Controllers;
using MonBattle.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Duel : System.Web.UI.Page
{

    public UserObject user;
    public CharacterObject self, opponent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Session["ErrorMessage"] = "You need to be logged in first";
            Response.Redirect("~/Login.aspx");
        }
        user = (UserObject)Session["User"];

        if (user.character == null)
        {
            Session["ErrorMessage"] = "You need to have a card character before battling.";
            Response.Redirect("~/CreateAChar.aspx");
        }
        self = (CharacterObject)user.character;

        if (Session["Opponent"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        opponent = (CharacterObject)Session["Opponent"];

        litAtk.Text = "<span>" + self.Attack + "</span>";
        imgSelf.ImageUrl = self.ImageUrl;

        litHp.Text = "<span>?/?</span>";
        imgOther.ImageUrl = opponent.ImageUrl;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session.Remove("Opponent");
        Response.Redirect("~/Battle.aspx");
    }

    /**
     * Deduct one from battle counter.
     */
    protected void btnDuel_Click(object sender, EventArgs e)
    {
        DataController controller = new DataController();
        int succ = controller.useBattleCounter(user.userId.Value);

        if (succ == 1)
        {
            Session["Results"] = calculateBattle();
            Response.Redirect("~/DuelResult.aspx");
        }
        else
        {
            Session.Remove("Opponent");
            Session["ErrorMessage"] = "You have used up all your battle counters today.";
            Response.Redirect("~/Default.aspx");
        }
    }

    /**
     * True = Win. False = Lose.
     */
    private bool calculateBattle()
    {
        double atk = Convert.ToDouble(self.Attack);
        double rate = Double.Parse(abs.Value);
        double chance = (1 - Math.Sqrt(rate - 100) / 10);

        Random rand = new Random();
        double p = rand.NextDouble();
        if (chance > p)
        {
            atk = atk * (rate / 100);
        }

        double hp = Convert.ToDouble(opponent.Health);
        p = rand.NextDouble();
        if (p < 0.11)
        {
            hp = hp * 1.5;
        }
        else if (p < 0.22)
        {
            hp = hp * 1.3;
        }
        else if (p < 0.33)
        {
            hp = hp * 1.2;
        }

        Session["Damage"] = atk;
        if (atk > hp)
        { // Win
            return true;
        }
        else if (Math.Abs(atk - hp) < 0.05)
        {
            return calculateBattle();
        }
        else
        { //Lose
            return false;
        }
    }
}
