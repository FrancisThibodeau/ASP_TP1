using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class Profil : System.Web.UI.Page
    {
        TableUsers user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Modifier votre profil";

            user = (TableUsers)Session["User"];

            if (!Page.IsPostBack)
            {
                LoadUserInfo();
            }
        }

        protected void BTN_Update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UpdateUser();
                Session["User"] = user;
                Response.Redirect("Index.aspx");
            }
        }

        private void LoadUserInfo()
        {
            TB_Fullname.Text = user.Fullname;
            TB_Username.Text = user.Username;
            TB_Password.Text = TB_PasswordConfirm.Text = user.Password;
            TB_Email.Text = TB_EmailConfirm.Text = user.Email;

            if (user.Avatar != "")
                IMG_PreviewAvatar.ImageUrl = @"~\Avatars\" + user.Avatar + ".png";
        }

        private void UpdateUser()
        {
            user.Fullname = TB_Fullname.Text;
            user.Username = TB_Username.Text;
            user.Password = TB_Password.Text;
            user.Email = TB_Email.Text;

            String avatar_ID;
            if (FU_Avatar.FileName != "")
            {
                String Avatar_Path = "";

                avatar_ID = Guid.NewGuid().ToString();
                Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
                FU_Avatar.SaveAs(Avatar_Path);
                user.Avatar = avatar_ID;
            }

            user.Update();
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
            else
            {
                TB_Email.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
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

    }
}