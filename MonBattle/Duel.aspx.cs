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
        
        Button btn = (Button)sender;
        battleSystem.processInput(Convert.ToInt32(btn.Attributes["ind"]));
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
        //btnDuel.Visible = true;
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
            //btnDuel.Visible = false;
            btnAtk.Visible = false;
            btnRematch.Visible = true;
        }
        displayBattleLog();
        refreshLayout();
    }

    private void refreshLayout() {

        //MoveSetPanel.Controls.Clear();

        if (battleSystem.self.Meter > 10)
        {
            battleSystem.self.Meter = 10;
        }

        int i = 1;
        foreach (Move move in battleSystem.self.chosenMoves) {

            Button btn_ability = (Button)monster1.FindControl("btn_ability" + i.ToString());

            btn_ability.Text = move.name;

            if (move.meterCost <= battleSystem.self.Meter)
            {
                btn_ability.Enabled = true;
                btn_ability.CssClass = "button-ability-enabled";
            }
            else
            {
                btn_ability.Enabled = false;
                btn_ability.CssClass = "button-ability-disabled";
            }
            i++;
        }

        //lbl_characterName.Text = "<p>" + battleSystem.self.Name + "</p>";
        //charHp.Text = "<p>" + battleSystem.self.Health + "/" + battleSystem.self.MaxHealth + "</p>";

        // Update player stats

        lbl_playerHP.Text = battleSystem.self.Health + "/" + battleSystem.self.MaxHealth;
        
        double hpPercentage = Math.Round(((Double)battleSystem.self.Health / (Double)battleSystem.self.MaxHealth) * 100.0, 2);

        if (hpPercentage < 0)
        {
            hpPercentage = 0;
        }

        img_playerHP.Width = new Unit(hpPercentage.ToString() + "%");

        lbl_playerAP.Text = battleSystem.self.Meter + "/10";
        img_playerAP.Width = new Unit((battleSystem.self.Meter * 10) + "%");

        // Update enemy stats

        lbl_enemyHP.Text = battleSystem.opponent.Health + "/" + battleSystem.opponent.MaxHealth;
        hpPercentage = Math.Round(((Double)battleSystem.opponent.Health / (Double)battleSystem.opponent.MaxHealth) * 100.0, 2);

        if (hpPercentage < 0)
        {
            hpPercentage = 0;
        }

        img_enemyHP.Width = new Unit(hpPercentage.ToString() + "%");


        //charMeter.Text = "<p>Meter:" + battleSystem.self.Meter + "</p>";

        //opName.Text = "<p>" + battleSystem.opponent.Name + "</p>";
        //opHp.Text = "<p>" + battleSystem.opponent.Health + "/" + battleSystem.opponent.MaxHealth + "</p>";
        //opMeter.Text = "<p>Meter:" + battleSystem.opponent.Meter + "</p>";
    }
}
