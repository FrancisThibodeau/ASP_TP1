using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;


namespace TP1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTN_Login_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LoginUser();
            }
        }

        private void LoginUser()
        {
            // Création d'une TableUser pour cette session
            Session["User"] = new TableUsers((String)Application["MainDB"], this);
            ((TableUsers)Session["User"]).SelectByFieldName("USERNAME", TB_Username.Text);

            // Création d'une TableLogins pour cette session
            Session["Login"] = new TableLogins((String)Application["MainDB"], this);

            // Temporaire
            RecordLogin();
            Response.Redirect("UserRoom.aspx");

        }

        protected void CV_Username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Username.Text == "")
            {
                TB_Username.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Username.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_Password_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Password.Text == "")
            {
                TB_Password.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            //else if (user.Password == TB_Password.Text)
            //{
            //    
            //}
            else
            {
                TB_Password.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
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

        protected void BTN_Inscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inscription.aspx");
        }

        protected void BTN_Password_Click(object sender, EventArgs e)
        {
            // Nouvel objet EMail
            Email eMail = new Email();

            // Mon adresse, mon mot de passe, Nom de provenance
            eMail.From = "fake.mail.asp@gmail.com";
            eMail.Password = "514236789";
            eMail.SenderName = "Admin";

            // Chourot Stuff, Security Related 
            eMail.Host = "smtp.gmail.com";
            eMail.HostPort = 587;
            eMail.SSLSecurity = true;

            ///////////////////// Section a finioler ////////////////////////////////////
            TableUsers users = new TableUsers((string)Application["MainDB"], this);
            if (users.Exist(TB_Username.Text))
            {
                try
                {
                    users.GetEmailFromUsers(TB_Username.Text);
                }
                catch (Exception ex)
                {

                    MessageErreur(ex.Message);
                }

                //eMail.to = Email associer au contenu du textbox username
                eMail.To = users.GetEmailFromUsers(TB_Username.Text);
                eMail.Subject = "Voici votre nouveau mot de passe";
                //Generation d'un nombre random [0-1m] comme mot de passe
                Random rnd = new Random();
                int pass = rnd.Next(1000000);
                // contenu du mail
                eMail.Body = "Votre nouveau mot de passe est " + pass + ". Bonne journee!!";
                // Verification
                if (eMail.Send())
                {
                    ClientAlert(this, "This eMail has been sent with success!");
                    TB_Username.Text = "";
                    TB_Password.Text = "";
                }
                else
                    ClientAlert(this, "An error occured while sendind this eMail!!!");
            }
            else
            {
                MessageErreur("Nom d'utilisateur incorrect");
                TB_Username.Text = "";
            }
        }

        private void MessageErreur(string message)
        {
            string script = "alert(\"" + message + "\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        public static void ClientAlert(System.Web.UI.Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + message + "');", true);
        }
    }
}