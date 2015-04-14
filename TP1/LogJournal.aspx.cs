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
            ((Label)Master.FindControl("LBL_Titre")).Text = "Journal Des Visites";
            AfficherListe();
        }

        private void AfficherListe()
        {
            TableLogins table = new TableLogins((String)Application["MainDB"], this);

            if (((TableUsers)Session["User"]).Username == "ADMIN")
                table.SelectAll();
            else
                table.SelectByFieldName("UserName", ((TableUsers)Session["User"]).Username);

            table.MakeGridView(PN_GridView, "");
            table.EndQuerySQL();
        }

        protected void BTN_Retour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void TimerJournal_Tick(object sender, EventArgs e)
        {
            AfficherListe();
        }
    }
}