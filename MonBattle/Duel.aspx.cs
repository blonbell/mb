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
    private DataController controller = new DataController();
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
        self.Meter = 0;
        self.clearMoveSet();
        self.moveset = controller.getOwnActiveMoveCatalog(self.charId);
        self.chooseMoves();

        opponent = new CharacterObject(4567, "Test Opponent", 5, "imageUrl");
        opponent.chooseMoves();

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
        MoveSetPanel.Controls.Clear();
        foreach (Move move in battleSystem.self.chosenMoves) {
            LiteralControl moveDesc = new LiteralControl();
            moveDesc.Text = "<p>id: " + move.moveId + " " + move.name + "/ " + move.meterCost + "</p>";
            MoveSetPanel.Controls.Add(moveDesc);
        }

        charName.Text = "<p>" + battleSystem.self.Name + "</p>";
        charHp.Text = "<p>" + battleSystem.self.Health + "/" + battleSystem.self.MaxHealth + "</p>";
        charMeter.Text = "<p>Meter:" + battleSystem.self.Meter + "</p>";

        opName.Text = "<p>" + battleSystem.opponent.Name + "</p>";
        opHp.Text = "<p>" + battleSystem.opponent.Health + "/" + battleSystem.opponent.MaxHealth + "</p>";
        opMeter.Text = "<p>Meter:" + battleSystem.opponent.Meter + "</p>";
    }
}
