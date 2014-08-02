using MonBattle.Controllers;
using MonBattle.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonBattle.Admin {
    public partial class Home : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            ltlTest.Text = "";
        }

        protected void btnFindVoters_Click(object sender, EventArgs e) {
            String date = txtDatePicker.Text;

            DataController controller = new DataController();
            List<CardVoterCounterObject> obj = controller.getVoteWinsAfterDate(date);

            if (obj != null) {
                foreach (var o in obj) {
                    ltlTest.Text += o.name + " " + o.winCount + "<br>";
                }
            }
        }
    }
}