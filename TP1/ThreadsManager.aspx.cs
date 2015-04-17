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

            Session["SelectedThread"] = LBL_ListDiscussions.SelectedItem;

            LBL_ListDiscussions.Items.Clear();

            if (threads.SelectByFieldName("CREATOR", ((TableUsers)Session["User"]).ID))
            {
                do
                {
                    ListItem item = new ListItem(threads.Title, threads.ID.ToString());

                    if (Session["SelectedThread"] != null)
                        item.Selected = threads.ID.ToString() == ((ListItem)Session["SelectedThread"]).Value;

                    LBL_ListDiscussions.Items.Add(item);
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

        private bool HasAccess(String userId, String threadId)
        {
            bool hasAccess = false;
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
            access.SelectByFieldName("THREAD_ID", threadId);

            do
            {
                hasAccess = access.UserID.ToString() == userId;
            } while (!hasAccess && access.Next());

            access.EndQuerySQL();

            return hasAccess;
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

        private void CheckInvitedUsers()
        {
            Table table = (Table)PN_User_Content.Controls[0];
            foreach (TableRow tr in table.Rows)
            {
                TableCell td = tr.Cells[0];
                CheckBox cb = (CheckBox)td.Controls[0];
                cb.Checked = HasAccess(cb.ID.Replace("CB_", ""), ((ListItem)Session["SelectedThread"]).Value);
            }
        }

        private void EditSelectedThread()
        {
            TableThreads thread = new TableThreads((String)Application["MainDB"], this);

            thread.SelectByID(((ListItem)Session["SelectedThread"]).Value);
            thread.EndQuerySQL();

            thread.Title = TBX_NewThread.Text;
            thread.Update();

            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
            access.NonQuerySQL("DELETE FROM " + access.SQLTableName + " WHERE THREAD_ID = " + thread.ID.ToString());
            CreateThreadAccess(thread.ID.ToString());
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

        private void ClearForm()
        {
            Session["SelectedThread"] = null;
            LBL_ListDiscussions.ClearSelection();
            BTN_Edit.Text = "Créer";
            TBX_NewThread.Text = "";

            ListThreads();
            ListUsers();
        }

        private void DeleteThread()
        {
            TableThreads thread = new TableThreads((String)Application["MainDB"], this);
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
            TableThreadsMessages messages = new TableThreadsMessages((String)Application["MainDB"], this);
            messages.NonQuerySQL("DELETE FROM " + messages.SQLTableName + " WHERE THREAD_ID = " + (((ListItem)Session["SelectedThread"]).Value));
            access.NonQuerySQL("DELETE FROM " + access.SQLTableName + " WHERE THREAD_ID = " + (((ListItem)Session["SelectedThread"]).Value));
            thread.DeleteRecordByID(((ListItem)Session["SelectedThread"]).Value);
        }

        protected void BTN_New_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void BTN_Edit_Click(object sender, EventArgs e)
        {
            if (Session["SelectedThread"] == null)
                CreateNewThread();
            else
                EditSelectedThread();

            ClearForm();
        }

        protected void BTN_Delete_Click(object sender, EventArgs e)
        {
            if (Session["SelectedThread"] != null)
            {
                DeleteThread();
                ClearForm();
            }
        }

        protected void BTN_Retour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void LBL_ListDiscussions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["SelectedThread"] != null)
            {
                CheckInvitedUsers();
                TBX_NewThread.Text = LBL_ListDiscussions.SelectedItem.Text;
                BTN_Edit.Text = "Modifier";
            }
        }

        protected void CBX_All_CheckedChanged(object sender, EventArgs e)
        {
            Table table = (Table)PN_User_Content.Controls[0];
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);

            foreach (TableRow tr in table.Rows)
            {
                TableCell td = tr.Cells[0];
                CheckBox cb = (CheckBox)td.Controls[0];
                cb.Checked = CBX_All.Checked;
            }
        }
    }
}