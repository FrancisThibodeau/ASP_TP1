using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class Inscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IMG_PreviewAvatar.ImageUrl = "~/Images/Anonymous.png";
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
        protected void BTN_Inscrire_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect("ListerUsers.aspx");
            }
        }
    }
}