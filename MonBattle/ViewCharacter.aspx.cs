using MonBattle.Controllers;
using MonBattle.Data;
using MonBattle.Data.BattleMechanics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewCharacter : System.Web.UI.Page
{

    public CharacterObject character = null;
    public UserObject user = null;
    DataController dataController = new DataController();

    /**
     * User can provide query param (charID) to look at the Character.
     * If not provided, it will show the user's Character
     * If user has no Character, kick to create a char page.
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Session["ErrorMessage"] = "You need to be logged in first";
            Response.Redirect("~/Login.aspx");
        }

        int cId;
        bool querySucc = int.TryParse(Request.QueryString["charID"], out cId);
        if (querySucc)
        {
            character = dataController.getCharacterByCharId(cId);
        }
        else
        {
            user = (UserObject)Session["User"];
            if (user.character == null)
            {
                Session["ErrorMessage"] = "You can create a Card Character here first before viewing it.";
                Response.Redirect("~/CreateAChar.aspx");
            }
            else
            {
                character = user.character;
            }
        }

        if (character == null)
        {
            noCharFoundPanel.Visible = true;
            viewPanel.Visible = false;
        }
        else
        {
            noCharFoundPanel.Visible = false;
            viewPanel.Visible = true;

            imgAvatar.ImageUrl = character.ImageUrl;
        }

        if(character.charId == user.character.charId) {
            populateMoveSetPanel();
        }
    }

    private void populateMoveSetPanel() {
        MoveSetPanel.Controls.Clear();
        List<Move> activeMoveSet = dataController.getOwnMoveCatalog(user.character.charId);

        foreach(Move move in activeMoveSet) {
            Panel row = new Panel();
            row.ID = "Move " + move.moveId;

            Image imgMove = new Image();
            imgMove.ImageUrl = move.imageUrl;
            row.Controls.Add(imgMove);

            Label lblName = new Label();  //c.active
            lblName.Text = move.name;
            row.Controls.Add(lblName);

            Label lblDesc = new Label();
            lblDesc.Text = move.description;
            row.Controls.Add(lblDesc);

            Label lblMeterCost = new Label();
            lblMeterCost.Text = "Redeem for " + move.meterCost + "MP";
            row.Controls.Add(lblMeterCost);

            RadioButtonList rblEnable = new RadioButtonList();
            rblEnable.Attributes["moveId"] = move.moveId.ToString();
            rblEnable.ID = "r" + move.moveId;
            rblEnable.RepeatDirection = RepeatDirection.Horizontal;
            ListItem itemEnable = new ListItem();
            itemEnable.Text = "Enable";
            itemEnable.Value = "1";

            ListItem itemDisable = new ListItem();
            itemDisable.Text = "Disable";
            itemDisable.Value = "0";

            rblEnable.Items.Add(itemEnable);
            rblEnable.Items.Add(itemDisable);
            rblEnable.SelectedValue = (move.inUse)? "1" : "0";
            row.Controls.Add(rblEnable);

            MoveSetPanel.Controls.Add(row);
        }
    }

    protected void btnUpdateMove_Click(object sender, EventArgs e) {
        ControlCollection controls = MoveSetPanel.Controls;
        int c = controls.Count;
        List<string> activeMoveIds = new List<string>();

        foreach (Control control in controls) {
            Panel row = (Panel) control;
            RadioButtonList skillEnableList = (RadioButtonList) row.Controls[4];
            if(skillEnableList.SelectedValue.Equals("1")) {
                activeMoveIds.Add(skillEnableList.Attributes["moveId"]);
            }
        }

        dataController.assignMove(user.character.charId, activeMoveIds);
    }
}