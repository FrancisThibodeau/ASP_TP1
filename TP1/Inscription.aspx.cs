using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class Inscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Inscription";


            if (!Page.IsPostBack)
            {
                IMG_PreviewAvatar.ImageUrl = "~/Images/Anonymous.png";
                Session["captcha"] = BuildCaptcha();
            }
        }
        private void AddUser()
        {
            TableUsers user = new TableUsers((String)Application["MainDB"], this);

            String avatar_ID = "";
            if (FU_Avatar.FileName != "")
            {
                String Avatar_Path = "";

                avatar_ID = Guid.NewGuid().ToString();
                Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
                FU_Avatar.SaveAs(Avatar_Path);
            }

            user.Online = 0;
            user.Username = TB_Username.Text;
            user.Password = TB_Password.Text;
            user.Fullname = TB_Fullname.Text;
            user.Email = TB_Email.Text;
            user.Avatar = avatar_ID;

            user.Insert();
        }

        protected void CV_Fullname_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Fullname.Text == "")
            {
                TB_Fullname.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Fullname.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_Username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TableUsers user = new TableUsers((String)Application["MainDB"], this);

            if (TB_Username.Text == "")
            {
                TB_Username.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else if (user.SelectByFieldName("USERNAME", TB_Username.Text))
            {
                TB_Username.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                CV_Username.ErrorMessage = "Ce nom d'usager est déjà pris";
                args.IsValid = false;
            }
            else
            {
                TB_Username.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }

            user.EndQuerySQL();
        }
        protected void CV_Password_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Password.Text == "")
            {
                TB_Password.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Password.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_PasswordConfirm_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_PasswordConfirm.Text == "" || TB_PasswordConfirm.Text != TB_Password.Text)
            {
                TB_PasswordConfirm.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_PasswordConfirm.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_Email_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Email.Text == "")
            {
                TB_Email.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else if (!ValiderEmail())
            {
                TB_Email.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                CV_Email.ErrorMessage = "Le courriel est syntaxiquement invalide!";
                args.IsValid = false;
            }
            else
            {
                TB_Email.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }

        private bool ValiderEmail()
        {
            Regex rgx = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", RegexOptions.IgnoreCase);
            Match match = rgx.Match(TB_Email.Text);
            return match.Value == TB_Email.Text;
        }

        protected void CV_EmailConfirm_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_EmailConfirm.Text == "" || TB_EmailConfirm.Text != TB_Email.Text)
            {
                TB_EmailConfirm.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_EmailConfirm.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }

        protected void CV_Captcha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Captcha.Text != (string)Session["captcha"])
            {
                TB_Captcha.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Captcha.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }

        protected void BTN_Inscrire_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AddUser();
                Response.Redirect("Login.aspx");
            }
        }

        //------------- CAPTCHA -------------
        Random random = new Random();
        char RandomChar()
        {
            // les lettres comportant des ambiguïtées ne sont pas dans la liste
            // exmple: 0 et O ont été retirés
            string chars = "abcdefghkmnpqrstuvwvxyzABCDEFGHKMNPQRSTUVWXYZ23456789";
            return chars[random.Next(0, chars.Length)];
        }

        Color RandomColor(int min, int max)
        {
            return Color.FromArgb(255, random.Next(min, max), random.Next(min, max), random.Next(min, max));
        }

        string Captcha()
        {
            string captcha = "";

            for (int i = 0; i < 5; i++)
                captcha += RandomChar();
            return captcha;//.ToLower();
        }

        string BuildCaptcha()
        {
            int width = 200;
            int height = 70;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics DC = Graphics.FromImage(bitmap);
            SolidBrush brush = new SolidBrush(RandomColor(0, 127));
            SolidBrush pen = new SolidBrush(RandomColor(172, 255));
            DC.FillRectangle(brush, 0, 0, 200, 100);
            Font font = new Font("Snap ITC", 32, FontStyle.Regular);
            PointF location = new PointF(5f, 5f);
            DC.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            string captcha = Captcha();
            DC.DrawString(captcha, font, pen, location);

            // noise generation
            for (int i = 0; i < 5000; i++)
            {
                bitmap.SetPixel(random.Next(0, width), random.Next(0, height), RandomColor(127, 255));
            }
            bitmap.Save(Server.MapPath("Captcha.png"), ImageFormat.Png);
            return captcha;
        }

        protected void RegenarateCaptcha_Click(object sender, ImageClickEventArgs e)
        {
            Session["captcha"] = BuildCaptcha();
            // + DateTime.Now.ToString() pour forcer le fureteur recharger le fichier
            IMGCaptcha.ImageUrl = "~/Captcha.png?ID=" + DateTime.Now.ToString();
            PN_Captcha.Update();
        }

        protected void BTN_Annuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}