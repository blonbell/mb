using MonBattle.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonBattle.Admin {
    public partial class AddMoveCatalog : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnAddMove_Click(object sender, EventArgs e) {
            DataController dc = new DataController();

            dc.addMove(
                txtName.Text,
                txtDescription.Text,
                txtTurn.Text,
                lingerList.SelectedValue,
                txtMeterCost.Text,
                txtCommandStr.Text,
                txtRedeemCost.Text,
                txtImageUrl.Text); 
        }
    }
}