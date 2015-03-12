using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP1
{
    public class TableUsers : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Fullname { get; set; }
        public String Email { get; set; }
        public String Avatar { get; set; }

        public TableUsers(String connexionString, System.Web.UI.Page page)
            : base(connexionString, page)
        {
            SQLTableName = "USERS";
        }

        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            Username = this["USERNAME"];
            Password = this["PASSWORD"];
            Fullname = this["FULLNAME"];
            Email = this["EMAIL"];
            Avatar = this["AVATAR"];
        }

        public override void InitColumnsVisibility()
        {
            base.InitColumnsVisibility();
            SetColumnVisibility("PASSWORD", false);
        }

        public override void InitColumnsTitles()
        {
            base.InitColumnsTitles();
            SetColumnTitle("ID", "Id");
            SetColumnTitle("USERNAME", "Nom d'usager");
            SetColumnTitle("FULLNAME", "Nom complet");
            SetColumnTitle("EMAIL", "Courriel");
            SetColumnTitle("AVATAR", "Avatar");
        }

        public override void Insert()
        {
            InsertRecord(Username, Password, Fullname, Email, Avatar);
        }
    }
}