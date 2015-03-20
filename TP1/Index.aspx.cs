using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Acceuil";
        }

        // Cette fonction va dans le logout, je l'ai mis là pour la tester
        private void RecordLogin()
        {
            TableLogins login = (TableLogins)Session["Login"];
            TableUsers user = (TableUsers)Session["User"];
            login.UserID = user.ID;
            login.LoginDate = (DateTime)Session["StartTime"];
            login.LogoutDate = DateTime.Now;
            login.IPAddress = GetUserIP(); // "GetUserIp"
            login.Insert();
        }

        public string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipList))
                return ipList.Split(',')[0];
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (ipAddress == "::1") // local host
                ipAddress = "127.0.0.1";
            return ipAddress;
        }

        private void LogoutUser()
        {
            RecordLogin();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void BTN_Logout_Click(object sender, EventArgs e)
        {
            LogoutUser();
        }

        protected void BTN_Profil_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profil.aspx");
        }

        protected void BTN_EnLigne_Click(object sender, EventArgs e)
        {
            Response.Redirect("Room.aspx");
        }
    }
}