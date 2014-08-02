using MonBattle.Controllers;
using MonBattle.Data;
using MonBattle.Data.BattleMechanics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Duel : System.Web.UI.Page {

    public UserObject user;
    public CharacterObject self, opponent;
    private BattleSystem battleSystem;

    protected void Page_Load(object sender, EventArgs e) {
        if (Session["User"] == null) {
            Session["ErrorMessage"] = "You need to be logged in first";
            Response.Redirect("~/Login.aspx");
        }
        user = (UserObject)Session["User"];

        if (user.character == null)
        {
            Session["ErrorMessage"] = "You need to have a card character before battling.";
            Response.Redirect("~/CreateAChar.aspx");
        }

        //make sure that battlesystem is only initialized once along with the characters.
        if (Session["BattleSystem"] == null) {
            createBattleSession();
        } else {
            battleSystem = (BattleSystem) Session["BattleSystem"];
        }

        refreshLayout();
    }

    protected void btnDuel_Click(object sender, EventArgs e) {
        battleSystem.processInput(Convert.ToInt32(TextBox1.Text));
        processTurn();
    }
    
    protected void btnAtk_Click(object sender, EventArgs e) {
        battleSystem.processAttack();
        processTurn();
    }

    private void displayBattleLog() {
        battleLog.Text = "";
        foreach(String msg in battleSystem.battleLog) {
            battleLog.Text += "<p>" + msg + "</p>";
        }
        battleSystem.clearLog();
    }

    protected void btnRematch_Click(object sender, EventArgs e) {
        btnDuel.Visible = true;
        btnAtk.Visible = true;
        btnRematch.Visible = false;
        battleLog.Text = "";
    }

    private void createBattleSession() {
        self = (CharacterObject) user.character;
        Move m1 = new Move("DMG050-HEP005", "desc", true, 2, 4, "DMG040-WEK050", self.charId);
        Move m2 = new Move("DMP020-BST020", "desc", false, 3, 3, "DMP020-RAV050", self.charId);
        Move m3 = new Move("REP030-TUR003", "desc", true, 3, 2, "REP030", self.charId);
        Move m4 = new Move("DMG099-END003", "desc", false, 3, 3, "DMG099", self.charId);
        self.moveset[0] = m1;
        self.moveset[1] = m2;
        self.moveset[2] = m3;
        self.moveset[3] = m4;

        opponent = new CharacterObject(45, "Test Opponent", 5, "imageUrl");
        Move k1 = new Move("DMG050-HEP005", "desc", true, 1, 4, "DMG050-HEP005", opponent.charId);
        Move k2 = new Move("DMP020-BST020", "desc", true, 1, 4, "DMG050-HEP005", opponent.charId);
        Move k3 = new Move("REP030-TUR003", "desc", true, 1, 4, "DMG050-HEP005", opponent.charId);
        Move k4 = new Move("DMG099-END003", "desc", true, 1, 4, "DMG050-HEP005", opponent.charId);
        opponent.moveset[0] = k1;
        opponent.moveset[1] = k2;
        opponent.moveset[2] = k3;
        opponent.moveset[3] = k4;

        battleSystem = new BattleSystem(self, opponent);
        Session["BattleSystem"] = battleSystem;
    }

    private void processTurn()
    {
        if (battleSystem.isBattleFinished())
        {
            Session.Remove("BattleSystem");
            if (battleSystem.playerIsWinner())
            {
                battleSystem.battleLog.Add("You win. Play again?");
            }
            else
            {
                battleSystem.battleLog.Add("You lost. Play again?");
            }
            btnDuel.Visible = false;
            btnAtk.Visible = false;
            btnRematch.Visible = true;
        }
        displayBattleLog();
        refreshLayout();
    }

    private void refreshLayout() {
        Literal1.Text = "<p>" + battleSystem.self.moveset[0].name + "/ " + battleSystem.self.moveset[0].meterCost + "</p>";
        Literal2.Text = "<p>" + battleSystem.self.moveset[1].name + "/ " + battleSystem.self.moveset[1].meterCost + "</p>";
        Literal3.Text = "<p>" + battleSystem.self.moveset[2].name + "/ " + battleSystem.self.moveset[2].meterCost + "</p>";
        Literal4.Text = "<p>" + battleSystem.self.moveset[3].name + "/ " + battleSystem.self.moveset[3].meterCost + "</p>";

        charName.Text = "<p>" + battleSystem.self.Name + "</p>";
        charHp.Text = "<p>" + battleSystem.self.Health + "/" + battleSystem.self.MaxHealth + "</p>";
        charMeter.Text = "<p>Meter:" + battleSystem.self.Meter + "</p>";

        opName.Text = "<p>" + battleSystem.opponent.Name + "</p>";
        opHp.Text = "<p>" + battleSystem.opponent.Health + "/" + battleSystem.opponent.MaxHealth + "</p>";
        opMeter.Text = "<p>Meter:" + battleSystem.opponent.Meter + "</p>";
    }
}
