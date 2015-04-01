using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TP1
{
    public class TableLogins : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public String IPAddress { get; set; }

        public String FullName { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String Avatar { get; set; }

        public  bool Admin = false;

        public TableLogins(String connexionString, System.Web.UI.Page page)
            : base(connexionString, page)
        {
            SQLTableName = "LOGINS";
        }

        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            UserID = long.Parse(this["UserId"]);
            LoginDate = DateTime.Parse(this["LoginDate"]);
            LogoutDate = DateTime.Parse(this["LogoutDate"]);
            IPAddress = this["IPAddress"];
            FullName = this["FullName"];
            UserName = this["UserName"];
            Email = this["Email"];
            Avatar = this["Avatar"];
        }

        public override bool SelectAll(string orderBy = "")
        {
            string sql = "SELECT Logins.ID,UserId,LoginDate,LogoutDate,IPAddress,FullName,UserName,Email,Avatar FROM " + SQLTableName + " inner join Users on Users.ID = Logins.UserID ";
            if (!Admin)
            {
                sql += " where UserName = '" + UserName + "'";
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