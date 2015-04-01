using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP1
{
    public class TableThreadAccess : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public long ThreadID { get; set; }
        public long UserID { get; set; }

        public TableThreadAccess(String connexionString, System.Web.UI.Page page)
            : base(connexionString, page)
        {
            SQLTableName = "THREADS_ACCESS";
        }

        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            ThreadID = long.Parse(this["THREAD_ID"]);
            UserID = long.Parse(this["USER_ID"]);
        }

        public override void Insert()
        {
            InsertRecord(ThreadID, UserID);
        }

        public override void Update()
        {
            UpdateRecord(ID, ThreadID, UserID);
        }
    }
}