using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1
{
    public partial class Chatroom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Titre")).Text = "Chatroom";

            ShowMessages();
            ShowInvitedUsers();
            ShowThreadButtons();
        }

        protected void TimerChatroom_Tick(object sender, EventArgs e)
        {
            UPN_Chatroom.Update();
        }

        private void ShowMessages()
        {
            TableThreadsMessages messages = new TableThreadsMessages((String)Application["MainDB"], this);
            if (Session["CurrentThread"] != null &&
                messages.SelectByFieldName("THREAD_ID", (String)Session["CurrentThread"]))
            {
                TableUsers user = new TableUsers((String)Application["MainDB"], this);

                Table table = new Table();
                TableRow tr;
                TableCell td;

                do
                {
                    tr = new TableRow();
                    td = new TableCell();

                    user.SelectByID(messages.UserID.ToString());
                    user.EndQuerySQL();

                    // Avatar
                    Image img = new Image();
                    img.ImageUrl = user.Avatar != "" ? "~/Avatars/" + user.Avatar + ".png" : "~/Images/Anonymous.png";
                    img.Width = img.Height = 40;
                    td.Controls.Add(img);
                    tr.Cells.Add(td);

                    // Nom et Date
                    td = new TableCell();
                    string date = messages.DateCreation.ToShortDateString() + " " + messages.DateCreation.ToShortTimeString();
                    string content = user.Fullname + "<br/>" + date + "<br/>";
                    td.Controls.Add(new LiteralControl(content));
                    tr.Cells.Add(td);

                    // Edit buttons
                    td = new TableCell();
                    if (user.ID == ((TableUsers)Session["User"]).ID)
                    {
                        td.Controls.Add(CreateDeleteButton(messages.ID.ToString()));
                        td.Controls.Add(CreateEditButton(messages.ID.ToString()));
                    }
                    tr.Cells.Add(td);

                    // Message
                    td = new TableCell();
                    td.Text = messages.Message;
                    tr.Cells.Add(td);

                    table.Rows.Add(tr);
                } while (messages.Next());

                messages.EndQuerySQL();

                PN_Messages.Controls.Clear();
                PN_Messages.Controls.Add(table);
            }

        }

        private void ShowInvitedUsers()
        {
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
            if (Session["CurrentThread"] != null &&
                access.SelectByFieldName("THREAD_ID", (String)Session["CurrentThread"]))
            {
                access.EndQuerySQL();

                CreateUsersList();
            }
        }

        private void CreateUsersList()
        {
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
            access.SelectByFieldName("THREAD_ID", (String)Session["CurrentThread"]);

            TableUsers users = new TableUsers((String)Application["MainDB"], this);

            Table table = new Table();
            TableRow tr;
            TableCell td;

            do
            {
                users.SelectByID(access.UserID.ToString());
                users.EndQuerySQL();

                tr = new TableRow();
                td = new TableCell();

                Image img = new Image();
                img.Height = img.Width = 25;
                img.ImageUrl = users.Online != 0 ? "~/Images/OnLine.png" : "~/Images/OffLine.png";
                td.Controls.Add(img);
                tr.Cells.Add(td);

                td = new TableCell();
                img = new Image();
                img.Height = img.Width = 25;
                img.ImageUrl = users.Avatar != "" ? "~/Avatars/" + users.Avatar + ".png" : "~/Images/Anonymous.png";
                td.Controls.Add(img);
                tr.Cells.Add(td);

                td = new TableCell();
                td.Text = users.Fullname;
                tr.Cells.Add(td);

                table.Rows.Add(tr);
            } while (access.Next());

            access.EndQuerySQL();
            PN_Users.Controls.Clear();
            PN_Users.Controls.Add(table);
        }

        private void SendMessage()
        {
            TableThreadsMessages message = new TableThreadsMessages((String)Application["MainDB"], this);
            message.ThreadID = long.Parse((String)Session["CurrentThread"]);
            message.UserID = ((TableUsers)Session["User"]).ID;
            message.DateCreation = DateTime.Now;
            message.Message = TB_Message.Text;
            message.Insert();
        }

        private void EditMessage()
        {
            TableThreadsMessages message = new TableThreadsMessages((String)Application["MainDB"], this);
            message.SelectByID((String)Session["EditID"]);

            message.Message = TB_Message.Text;
            message.Update();

            BTN_Send.Text = "Envoyer";

            Session["EditID"] = null;

            message.EndQuerySQL();
        }

        private Button CreateThreadButton(String threadId, String threadTitle)
        {
            Button btn = new Button();
            btn.ID = "ThreadButton_" + threadId;
            btn.Text = threadTitle;
            btn.CssClass = threadId == (String)Session["CurrentThread"] ? "CurrentThreadButton" : "ThreadButton";
            btn.Click += BTN_Thread_Click;
            return btn;
        }

        private void ShowThreadButtons()
        {
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
            TableThreads thread = new TableThreads((String)Application["MainDB"], this);

            Table table = new Table();
            TableRow tr;
            TableCell td;

            if (access.SelectByFieldName("USER_ID", ((TableUsers)Session["User"]).ID))
            {
                do
                {
                    thread.SelectByID(access.ThreadID.ToString());
                    thread.EndQuerySQL();

                    tr = new TableRow();
                    td = new TableCell();

                    td.Controls.Add(CreateThreadButton(thread.ID.ToString(), thread.Title));
                    tr.Cells.Add(td);

                    table.Rows.Add(tr);
                } while (access.Next());
            }

            access.EndQuerySQL();

            PN_Threads.Controls.Clear();
            PN_Threads.Controls.Add(table);
        }

        private ImageButton CreateDeleteButton(String messageId)
        {
            ImageButton btn = new ImageButton();
            btn.ID = "BTN_Delete_" + messageId;
            btn.ImageUrl = @"~/Images/delete.png";
            btn.Width = btn.Height = 26;
            btn.Click += BTN_Delete_Click;

            return btn;
        }

        private ImageButton CreateEditButton(String messageId)
        {
            ImageButton btn = new ImageButton();
            btn.ID = "BTN_Edit_" + messageId;
            btn.ImageUrl = @"~/Images/edit.png";
            btn.Width = btn.Height = 26;
            btn.Click += BTN_Edit_Click;

            return btn;
        }


        protected void BTN_Thread_Click(object sender, EventArgs e)
        {
            String threadId = ((Button)sender).ID;

            threadId = threadId.Replace("ThreadButton_", "");

            Session["CurrentThread"] = threadId;

            TableThreads thread = new TableThreads((String)Application["MainDB"], this);
            thread.SelectByID(threadId);
            LBL_Title.Text = thread.Title;
            thread.EndQuerySQL();

            ShowMessages();
            ShowInvitedUsers();
            ShowThreadButtons();
            UPN_Chatroom.Update();
        }

        protected void BTN_Delete_Click(object sender, ImageClickEventArgs e)
        {
            String messageId = ((ImageButton)sender).ID;

            messageId = messageId.Replace("BTN_Delete_", "");

            TableThreadsMessages message = new TableThreadsMessages((String)Application["MainDB"], this);
            message.DeleteRecordByID(messageId);

            ShowMessages();
        }

        protected void BTN_Edit_Click(object sender, ImageClickEventArgs e)
        {
            String messageId = ((ImageButton)sender).ID;

            messageId = messageId.Replace("BTN_Edit_", "");
            TableThreadsMessages message = new TableThreadsMessages((String)Application["MainDB"], this);
            message.SelectByID(messageId);

            Session["EditID"] = messageId;

            TB_Message.Text = message.Message;
            BTN_Send.Text = "Modifier";
            PN_Message.Update();

            message.EndQuerySQL();
        }

        protected void BTN_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void BTN_Send_Click(object sender, EventArgs e)
        {
            if (TB_Message.Text != "")
            {
                if (Session["EditID"] == null)
                    SendMessage();
                else
                    EditMessage();
            }

            TB_Message.Text = "";
        }
    }
}