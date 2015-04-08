using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TP1
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            string DB_Path = Server.MapPath(@"~\App_Data\MainDB.mdf");
            Application["MainDB"] = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + DB_Path + "';Integrated Security=True";
        }

        protected void Session_End(object sender, EventArgs e)
        {
            if ((TableUsers)Session["User"] != null)
            {
                ((TableUsers)Session["User"]).Online = 0;
                ((TableUsers)Session["User"]).Update();
                ((TableUsers)Session["User"]).EndQuerySQL();
            }
        }
    }
}