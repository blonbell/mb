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
            UserObject user = (UserObject)Session["User"];
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
    }
}