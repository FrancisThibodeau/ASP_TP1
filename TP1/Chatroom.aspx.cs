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
        TableThreads currentThread;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentThread = new TableThreads((String)Application["MainDB"], this);
            currentThread.SelectByFieldName("ID", 1);
            currentThread.EndQuerySQL();
            ShowMessages();
        }

        protected void TimerChatroom_Tick(object sender, EventArgs e)
        {
            ShowMessages();
        }

        private void ShowMessages()
        {
            TableThreadsMessages messages = new TableThreadsMessages((String)Application["MainDB"], this);
            messages.SelectByFieldName("THREAD_ID", currentThread.ID);

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
                img.ImageUrl = img.ImageUrl = "~/Avatars/" + user.Avatar + ".png";
                img.Width = img.Height = 25;
                td.Controls.Add(img);
                tr.Cells.Add(td);

                // Nom et Date
                td = new TableCell();
                string content = user.Username + "<br/>" + messages.DateCreation + "<br/>";
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
    }
}