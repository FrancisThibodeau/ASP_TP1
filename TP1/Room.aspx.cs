using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class ListerUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Room";

            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

            if (Session.IsNewSession)
                Server.Transfer("Login.aspx");

            AfficherListe();
        }

        private void AfficherListe()
        {
            TableUsers table = new TableUsers((String)Application["MainDB"], this);
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