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

public partial class Shop : System.Web.UI.Page
{
    public int trainingHour = 7;
    public int usage = 3;
    public int effect = 1;
    public System.DateTime? finishTime = null;

    public UserObject user;
    public int charID;

    DataController controller = new DataController();
    /*
     * Pre-checks
     * 1) User has CharacterObject
     * If failed, boot to default or equivalent
     */
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
            Session["ErrorMessage"] = "You can create a Card Character here first before visiting the shop.";
            Response.Redirect("~/CreateAChar.aspx");
        }
        updateCharacterPanel();
    }

    private void trainMonster(int trainingType)
    {
        // update CharacterObject training type and training time
        DateTime trainingTime = DateTime.Now;
        trainingTime = trainingTime.AddHours(trainingHour);

        int succ = controller.startTrainCharacter(user, user.character, trainingTime, trainingType, usage);
        if (succ == 0)
        {
            Session["ErrorMessage"] = "You do not have enough points to spend!";
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            showTrainingPanel(trainingTime, (CharacterObject.trainingTypeEnum)trainingType);
        }
    }

    /*
     * 1) Check pre-conditions. User points is >= cost.
     * 2) Set training_finish_time to date.now() + 45 minutes, training_type to ATK
     * 2') Deduct Cost. Stored-Procedure?
     * 4) Reload Page (automatic)
     */
    protected void btnAtk_Click(object sender, ImageClickEventArgs e)
    {
        trainMonster(1);
    }

    protected void btnHp_Click(object sender, ImageClickEventArgs e)
    {
        trainMonster(2);
    }

    protected void btnSpd_Click(object sender, ImageClickEventArgs e)
    {
        trainMonster(3);
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        string trType = user.character.trainingType.ToString();
        int succ = controller.finishTrainCharacter(user.character.charId, effect);
        if (succ != 0)
        {
            showShopPanel();
        }
        controller.attachUserCharacterObject(user);
        updateCharacterPanel();

        if (trType == CharacterObject.trainingTypeEnum.Move.ToString()) {
            lbl_popupMessage.Text = user.character.Name + "'s learnt a new move";
        } else {
            lbl_popupMessage.Text = user.character.Name + "'s " + trType + " has increased by " + effect;
        }
        popupext_vote.Show();
    }

    private void updateCharacterPanel()
    {
        CharacterObject self = user.character;
        charAtk.Text = "" + self.Attack;
        charHp.Text = String.Format("{0}/{1}", self.Health, self.MaxHealth);
        charSpd.Text = "" + self.Speed;
        charImg.ImageUrl = self.ImageUrl;
        if (self.trainingFinishTime == null)
        {
            showShopPanel();
        }
        else
        {
            showTrainingPanel(self.trainingFinishTime.Value, self.trainingType.Value);
        }
    }

    private void showTrainingPanel(DateTime trainingTime, CharacterObject.trainingTypeEnum type)
    {
        litTrainingTitle.Text = "Training In Progress";
        trainingPanel.Visible = true;
        shopPanel.Visible = false;
        finishTime = trainingTime;

        if (type == CharacterObject.trainingTypeEnum.Attack)
        {
            trainingImgIcon.ImageUrl = "~/images/Attack-Icon.png";
        }
        else if (type == CharacterObject.trainingTypeEnum.Health)
        {
            trainingImgIcon.ImageUrl = "~/images/HP-Icon.png";
        }
        else if (type == CharacterObject.trainingTypeEnum.Speed)
        {
            trainingImgIcon.ImageUrl = "~/images/Speed-Icon.png";
        }
        lblTraining.Text = lblTraining.Text = "Training Type: " + type.ToString();
    }

    private void showShopPanel()
    {
        litTrainingTitle.Text = "Training Shop";
        trainingPanel.Visible = false;
        shopPanel.Visible = true;
        populateMoveSetPanel();
    }

    private void populateMoveSetPanel() {
        MovesetPanel.Controls.Clear();
        List<Move> trainingMoveSet = controller.getTrainingCatalog(user.character.charId);

        foreach(Move move in trainingMoveSet) {
            Panel row = new Panel();
            row.ID = "Move " + move.moveId;

            Image imgMove = new Image();
            imgMove.ImageUrl = move.imageUrl;
            row.Controls.Add(imgMove);

            Label lblName = new Label();
            lblName.Text = move.name;
            row.Controls.Add(lblName);

            Label lblDesc = new Label();
            lblDesc.Text = move.description;
            row.Controls.Add(lblDesc);

            Label lblRedeemCost = new Label();
            lblRedeemCost.Text = "Redeem for " + move.redeemCost + "MP";
            row.Controls.Add(lblRedeemCost);

            if (move.ownerId.HasValue) { //character has already learned this move
                Label lblLearnt = new Label();
                lblLearnt.Text = "LEARNED";
                row.Controls.Add(lblLearnt);
            } else {
                Button btnLearn = new Button();
                btnLearn.Attributes["moveId"] = move.moveId.ToString();
                btnLearn.Text = "Train";
                btnLearn.Click += btnMove_Click;
                row.Controls.Add(btnLearn);
            }
            MovesetPanel.Controls.Add(row);
        }
    }

    protected void btnMove_Click(object sender, EventArgs e) {
        DateTime trainingTime = DateTime.Now;
        trainingTime = trainingTime.AddSeconds(trainingHour);
        int trainingType = 4;
        Button button = (Button) sender;
        string moveId = button.Attributes["moveId"];
        //call startTrainingMove
        int succ = controller.startMoveTrainCharacter(user, user.character, moveId, trainingTime, trainingType);
        if (succ == -1) {
            Session["ErrorMessage"] = "You do not have enough points to spend!";
            Response.Redirect("~/Default.aspx");
        } else {
            showTrainingPanel(trainingTime, (CharacterObject.trainingTypeEnum) trainingType);
        }
    }
}