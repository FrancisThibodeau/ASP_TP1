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

            ShowThreadButtons();
        }

        protected void TimerChatroom_Tick(object sender, EventArgs e)
        {
            ShowMessages();
            ShowInvitedUsers();
            ShowThreadButtons();
        }

        private void ShowMessages()
        {
            TableThreadsMessages messages = new TableThreadsMessages((String)Application["MainDB"], this);
            messages.SelectByFieldName("THREAD_ID", (String)Session["CurrentThread"]);

            TableUsers user = new TableUsers((String)Application["MainDB"], this);

            Table table = new Table();
            TableRow tr;
            TableCell td;

            do
            {
                tr = new TableRow();
                td = new TableCell();

                user.SelectByID(messages.UserID.ToString());

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
                    td.Text = "X";
                }
                tr.Cells.Add(td);

                // Message
                td = new TableCell();
                td.Text = messages.Message;
                tr.Cells.Add(td);

                table.Rows.Add(tr);
            } while (messages.Next());

            PN_Messages.Controls.Clear();
            PN_Messages.Controls.Add(table);
        }

        private void ShowInvitedUsers()
        {
            TableThreadsAccess access = new TableThreadsAccess((String)Application["MainDB"], this);
            access.SelectByFieldName("THREAD_ID", (String)Session["CurrentThread"]);
            access.EndQuerySQL();

            if (access.UserID == 0)
                ShowAllUsers();
            else
                ShowSpecificUsers();
        }

        private void ShowAllUsers()
        {
            TableUsers users = new TableUsers((String)Application["MainDB"], this);
            users.SelectAll("ONLINE DESC");

            Table table = new Table();
            TableRow tr;
            TableCell td;

            while (users.Next())
            {
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
            }

            users.EndQuerySQL();
            PN_Users.Controls.Clear();
            PN_Users.Controls.Add(table);
        }

        private void ShowSpecificUsers()
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

                    Button btn = new Button();
                    btn.ID = "ThreadButton_" + thread.ID.ToString();
                    btn.Text = thread.Title;
                    btn.Click += BTN_Thread_Click;
                    td.Controls.Add(btn);
                    tr.Cells.Add(td);

                    table.Rows.Add(tr);
                } while (access.Next());
            }


            access.EndQuerySQL();

            access.SelectByFieldName("USER_ID", 0);

            do
            {
                thread.SelectByID(access.ThreadID.ToString());
                thread.EndQuerySQL();

                tr = new TableRow();
                td = new TableCell();

                Button btn = new Button();
                btn.ID = "ThreadButton_" + thread.ID.ToString();
                btn.Text = thread.Title;
                btn.Click += BTN_Thread_Click;
                td.Controls.Add(btn);
                tr.Cells.Add(td);

                table.Rows.Add(tr);
            } while (access.Next());

            access.EndQuerySQL();
            PN_Threads.Controls.Clear();
            PN_Threads.Controls.Add(table);
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
        }

        protected void BTN_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void BTN_Send_Click(object sender, EventArgs e)
        {
            if (TB_Message.Text != "")
                SendMessage();

            TB_Message.Text = "";
        }


    }
}