using MonBattle.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace MonBattle.Admin {
    public partial class ResetBattleCounter : System.Web.UI.Page {
        int totalPoints, totalAttack, totalMaxHP, totalSpeed;

        protected void Page_Load(object sender, EventArgs e) {
            totalPoints = 0; totalAttack = 0; totalMaxHP = 0; totalSpeed = 0;
        }
        
        

        protected void btnRefresh_Click(object sender, EventArgs e) {
            DataController controller = new DataController();
            controller.updateBattleCounter();
            lblComp.Text = "Reset complete";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DataRowView datarows = (DataRowView) e.Row.DataItem;
                totalPoints += Convert.ToInt32(datarows[2]);
                totalAttack += Convert.ToInt32(datarows[3]);
                totalMaxHP += Convert.ToInt32(datarows[5]);
                totalSpeed += Convert.ToInt32(datarows[6]);
            } else if (e.Row.RowType == DataControlRowType.Footer) {
                e.Row.Cells[0].Text = "Summary";

                e.Row.Cells[2].Text = "" + totalPoints;
                e.Row.Cells[3].Text = "" + totalAttack;
                e.Row.Cells[5].Text = "" + totalMaxHP;
                e.Row.Cells[6].Text = "" + totalSpeed;
            }
        }
    }
}