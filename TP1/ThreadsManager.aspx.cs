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
                    if (user.ID != ((TableUsers)Session["User"]).ID)
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
                }

                PN_User_Content.Controls.Clear();
                PN_User_Content.Controls.Add(table);
            }

            user.EndQuerySQL();
        }

        private void CreateNewThread()
        {
            TableThreads thread = new TableThreads((String)Application["MainDB"], this);
            thread.Creator = ((TableUsers)Session["User"]).ID;
            thread.Title = TBX_NewThread.Text;
            thread.DateCreation = DateTime.Now;

            thread.Insert();
            thread.SelectAll("ID DESC");
            thread.Next();

            String id = thread.ID.ToString();

            thread.EndQuerySQL();

            CreateThreadAccess(id);
        }

        private void CreateThreadAccess(String threadId)
        {
            Table table = (Table)PN_User_Content.Controls[0];
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);

            access.ThreadID = long.Parse(threadId);

            if (CBX_All.Checked)
            {
                access.UserID = 0;
                access.Insert();
            }
            else
            {
                access.UserID = ((TableUsers)Session["User"]).ID;
                access.Insert();

                foreach (TableRow tr in table.Rows)
                {
                    TableCell td = tr.Cells[0];
                    CheckBox cb = (CheckBox)td.Controls[0];
                    if (cb.Checked)
                    {
                        access.UserID = long.Parse(cb.ID.Replace("CB_", ""));
                        access.Insert();
                    }

                }
            }
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
            CreateNewThread();
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