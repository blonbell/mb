using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MonBattle.Data;
using MonBattle.Controllers;
using System.Data;

namespace MonBattle.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        DataController dataController = new DataController();

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if (!IsPostBack)
            {
                bindGridData();
            }*/
        }
        /*
        /// <summary>
        /// Gets users from database and populates grid
        /// </summary>
        private void bindGridData()
        {
            DataTable usersTable = dataController.getUsers();

            if (usersTable != null)
            {
                grid_users.DataSource = usersTable;
                grid_users.DataBind();
            }
        }*/
    }
}