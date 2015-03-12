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
            TableUsers user = new TableUsers((String)Application["MainDB"], this);
            if(user.SelectByFieldName("USERNAME", TB_Username.Text))
            {
                user.GetValues();
                
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
            //else if (user.Password == TB_Password.Text)
            //{
            //    
            //}
            else
            {
                TB_Password.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
                Response.Redirect("Inscription.aspx");
            }
        }
    }
}