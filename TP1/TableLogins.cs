﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP1
{
    public class TableLogins : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public String IPAddress { get; set; }

        public TableLogins(String connexionString, System.Web.UI.Page page)
            : base(connexionString, page)
        {
            SQLTableName = "LOGINS";
        }

        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            UserID = long.Parse(this["USERID"]);
            LoginDate = DateTime.Parse(this["LOGINDATE"]);
            LogoutDate = DateTime.Parse(this["LOGOUTDATE"]);
            IPAddress = this["IPADDRESS"];
        }

        public override bool SelectAll(string orderBy = "")
        {
            string sql = "SELECT Logins.ID,UserId,LoginDate,LogoutDate,IPAddress,FullName,UserName,Email,Avatar FROM " + SQLTableName + " inner join Users on Users.ID = Logins.UserID ";
            if (!LogJournal.Admin)
            {
                sql += " where UserName = '" + UserID + "'";
            }

            if (orderBy != "")
                sql += " ORDER BY " + orderBy;
            QuerySQL(sql);
            return reader.HasRows;
        }

        public override void Insert()
        {
            InsertRecord(UserID, LoginDate, LogoutDate, IPAddress);
        }
    }
}