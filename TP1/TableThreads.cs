using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP1
{
    public class TableThreads : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public long Creator { get; set; }
        public String Title { get; set; }
        public DateTime DateCreation { get; set; }

        public TableThreads(String connexionString, System.Web.UI.Page page)
            : base(connexionString,page)
        {
            SQLTableName = "THREADS";
        }

        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            Creator = long.Parse(this["CREATOR"]);
            Title = this["TITLE"];
            DateCreation = DateTime.Parse(this["DATE_CREATION"]);
        }

        public override void Insert()
        {
            InsertRecord(Creator, Title, DateCreation);
        }

        public override void Update()
        {
            UpdateRecord(ID, Creator, Title, DateCreation);
        }
    }
}