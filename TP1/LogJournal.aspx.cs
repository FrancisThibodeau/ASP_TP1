using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class LogJournal : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Room";
            AfficherListe();
        }

        private void AfficherListe()
        {
            TableLogins table = new TableLogins((String)Application["MainDB"], this);
            //if (Session["UserID"].ToString() == "admin")
                table.Admin = true;
            //else
            //    table.UserName = Session["UserName"].ToString();

            table.SelectAll();
            table.MakeGridView(PN_GridView, "");
            table.EndQuerySQL();
        }

        protected void BTN_Retour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}