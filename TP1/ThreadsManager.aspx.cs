using System;
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
            ListThreads();
            ListUsers();
        }

        private void ListThreads()
        {
            TableThreads threads = new TableThreads((String)Application["MainDB"], this);

            if (threads.SelectByFieldName("CREATOR", ((TableUsers)Session["User"]).ID))
            {
                do
                {
                    LBL_ListDiscussions.Items.Add(new ListItem(threads.Title, threads.ID.ToString()));
                } while (threads.Next());
            }

            threads.EndQuerySQL();
        }

        private void ListUsers()
        {
            TableUsers user = new TableUsers((String)Application["MainDB"], this);
            if (user.SelectAll())
            {
                Table table = new Table();
                TableRow tr;
                TableCell td;

                while (user.Next())
                {
                    tr = new TableRow();
                    td = new TableCell();

                    CheckBox cb = new CheckBox();
                    cb.ID = "CB_" + user.ID.ToString();
                    td.Controls.Add(cb);
                    tr.Cells.Add(td);

                    td = new TableCell();
                    Image img = new Image();
                    img.Height = img.Width = 25;
                    img.ImageUrl = user.Avatar != "" ? "~/Avatars/" + user.Avatar + ".png" : "~/Images/Anonymous.png";
                    td.Controls.Add(img);
                    tr.Cells.Add(td);

                    td = new TableCell();
                    td.Text = user.Fullname;
                    tr.Cells.Add(td);

                    table.Rows.Add(tr);
                }

                PN_User_Content.Controls.Clear();
                PN_User_Content.Controls.Add(table);
            }

            user.EndQuerySQL();
        }

        //protected void CVal_TitreDiscussion_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //
        //}
        //
        //protected void CVal_DiscussionExiste_Exists(object source, ServerValidateEventArgs args)
        //{
        //    
        //}
        //
        //protected void CBOX_AllUsers_CheckedChanged(object sender, EventArgs e)
        //{
        //
        //}

        protected void BTN_New_Click(object sender, EventArgs e)
        {
            if (TBX_NewThread.Text != null)
            {
                ListItem newItem = new ListItem(TBX_NewThread.Text, "0");
                LBL_ListDiscussions.Items.Add(newItem);
                TBX_NewThread.Text = null;
                // call de update panel
            }
            else
            {
                TBX_NewThread.Text = null;
                // call de update panel
            }
        }

        protected void BTN_Modify_Click(object sender, EventArgs e)
        {

        }

        protected void BTN_Delete_Click(object sender, EventArgs e)
        {
            LBL_ListDiscussions.Items.Remove(LBL_ListDiscussions.SelectedItem);
        }

        protected void BTN_Retour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}