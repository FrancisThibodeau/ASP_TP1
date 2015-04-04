using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TP1
{
    public class TableThreadsMessages : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public long ThreadID { get; set; }
        public long UserID { get; set; }
        public DateTime DateCreation { get; set; }
        public String Message { get; set; }

        public TableThreadsMessages(String connexionString, System.Web.UI.Page page)
            : base(connexionString, page)
        {
            SQLTableName = "THREADS_MESSAGES";
        }

        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            ThreadID = long.Parse(this["THREAD_ID"]);
            UserID = long.Parse(this["USER_ID"]);
            DateCreation = DateTime.Parse(this["DATE_CREATION"]);
            Message = this["MESSAGE"];
        }

        public override void Insert()
        {
            InsertRecord(ThreadID, UserID, DateCreation, Message);
        }

        public override void Update()
        {
            UpdateRecord(ID, ThreadID, UserID, DateCreation, Message);
        }

    }
}