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
        public bool Admin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Room";
            AfficherListe();
        }

        private void AfficherListe()
        {
            TableUsers table = new TableUsers((String)Application["MainDB"], this);
            if (Session["UserName"].ToString() == "admin")
                Admin = true;

            table.SelectAll();
            table.MakeGridView(PN_GridView, "");
            table.EndQuerySQL();
        }

       public override bool SelectAll(string orderBy = "")
       {
           string sql = "SELECT Logins.ID,UserId,LoginDate,LogoutDate,IPAddress,FullName,UserName,Email,Avatar FROM " + SQLTableName + " inner join Users on Users.ID = Logins.UserID where UserName = '" + UserName + "'";

           if (orderBy != "")
               sql += " ORDER BY " + orderBy;
           QuerySQL(sql);
           return reader.HasRows;
       }

        protected void BTN_Retour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}