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
            ((Label)Master.FindControl("LBL_Titre")).Text = "Login";
        }

        protected void BTN_Login_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
                LoginUser();
            }
        }

        private void LoginUser()
        {
            // Création d'une TableUser pour cette session
            Session["User"] = new TableUsers((String)Application["MainDB"], this);
            ((TableUsers)Session["User"]).SelectByFieldName("USERNAME", TB_Username.Text);
            ((TableUsers)Session["User"]).EndQuerySQL();

            // Flag l'usager en ligne
            ((TableUsers)Session["User"]).Online = 1;
            ((TableUsers)Session["User"]).Update();

            Session["StartTime"] = DateTime.Now;

            Response.Redirect("Index.aspx");
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
            else if (!ValiderMotDePasse())
            {
                TB_Password.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                CV_Password.ErrorMessage = "Nom d'usager et/ou mot de passe incorrect";
                args.IsValid = false;
            }
            else
            {
                TB_Password.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }

        private bool ValiderMotDePasse()
        {
            bool valide = false;

            TableUsers user = new TableUsers((String)Application["MainDB"], this);
            
            if (user.SelectByFieldName("USERNAME", TB_Username.Text))
            {
                valide = user.Password == TB_Password.Text;
            }

            user.EndQuerySQL();

            return valide;
        }

        protected void BTN_Inscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inscription.aspx");
        }

        protected void BTN_Password_Click(object sender, EventArgs e)
        {
            ///////////////////// Section a finioler ////////////////////////////////////
            TableUsers users = new TableUsers((string)Application["MainDB"], this);
            if (users.SelectByFieldName("USERNAME", TB_Username.Text))
            {
                try
                {
                    EnvoyerPasswordEmail(users);
                }
                catch
                {
                    ClientAlert(this, "Wrong Username!!");
                }
            }
            else
            {
                ClientAlert(this, "Wrong Username!!");
                TB_Username.Text = "";
                TB_Password.Text = "";
            }
            users.EndQuerySQL();
        }
        private void EnvoyerPasswordEmail(TableUsers connexion)
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
            //eMail.to = Email associer au contenu du textbox username
            eMail.To = connexion.Email;
            eMail.Subject = "Voici votre nouveau mot de passe";
            //Generation d'un nombre random [0-1m] comme mot de passe
            Random rnd = new Random();
            int pass = rnd.Next(1000000);
            connexion.Password = pass.ToString();
            connexion.Update();
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

        //private void MessageErreur(string message)
        //{
        //    string script = "alert(\"" + message + "\");";
        //    ScriptManager.RegisterStartupScript(this, GetType(),
        //                          "ServerControlScript", script, true);
        //}

        public static void ClientAlert(System.Web.UI.Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + message + "');", true);
        }
    }
}