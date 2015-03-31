using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((TableUsers)Session["User"] != null)
            {
                IMG_Avatar.ImageUrl = "~/Avatars/" + ((TableUsers)Session["User"]).Avatar + ".png";
                LBL_User.Text = ((TableUsers)Session["User"]).Fullname;
            }
            else
            {
                IMG_Avatar.ImageUrl = "~/Images/Anonymous.png";
                LBL_User.Text = "Anonymous";
            }

            Session.Timeout = 1;
        }

        protected void TimerTime_Tick(object sender, EventArgs e)
        {

        }
    }
}