﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class ThreadsManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Gestion des discussions";
        }

        protected void CVal_TitreDiscussion_ServerValidate(object source, ServerValidateEventArgs args)
        {
        
        }

        protected void CVal_DiscussionExiste_Exists(object source, ServerValidateEventArgs args)
        {
            
        }

        protected void CBOX_AllUsers_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void BTN_New_Click(object sender, EventArgs e)
        {

        }

        protected void BTN_Modify_Click(object sender, EventArgs e)
        {

        }

        protected void BTN_Delete_Click(object sender, EventArgs e)
        {

        }

        protected void BTN_Retour_Click(object sender, EventArgs e)
        {

        }
    }
}