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

            if (orderBy != "")
                sql += " ORDER BY " + orderBy;
            QuerySQL(sql);
            return reader.HasRows;
        }

        public override bool SelectByFieldName(String FieldName, object value)
        {
            string SQL = "SELECT Logins.ID,UserId,LoginDate,LogoutDate,IPAddress,FullName,UserName,Email,Avatar FROM " + SQLTableName + " inner join Users on Users.ID = Logins.UserID WHERE " + FieldName + " = ";
            Type type = value.GetType();
            if (SqlExpressUtilities.SQLHelper.IsNumericType(type))
                SQL += value.ToString().Replace(',', '.');
            else
                if (type == typeof(DateTime))
                    SQL += "'" + SqlExpressUtilities.SQLHelper.DateSQLFormat((DateTime)value) + "'";
                else
                    SQL += "'" + SqlExpressUtilities.SQLHelper.PrepareForSql(value.ToString()) + "'";
            QuerySQL(SQL);
            if (reader.HasRows)
                Next();
            return reader.HasRows;
        }

        public override void Insert()
        {
            InsertRecord(UserID, LoginDate, LogoutDate, IPAddress);
        }

        public override void InitColumnsVisibility()
        {
            base.InitColumnsVisibility();
            SetColumnVisibility("ID", false);
            //SetColumnVisibility("PASSWORD", false);
        }

        public override void InitCellsContentDelegate()
        {
            base.InitCellsContentDelegate();
            //SetCellContentDelegate("ID", ContentDelegateID);
            SetCellContentDelegate("UserID", ContentDelegateUserID);
            SetCellContentDelegate("LoginDate", ContentDelegateLoginDate);
            SetCellContentDelegate("LogoutDate", ContentDelegateLogoutDate);
            SetCellContentDelegate("IpAddress", ContentDelegateIPAddress);
            SetCellContentDelegate("FullName", ContentDelegateFullName);
            SetCellContentDelegate("UserName", ContentDelegateUserName);
            SetCellContentDelegate("Email", ContentDelegateEmail);
            SetCellContentDelegate("Avatar", ContentDelegateAvatar);
        }

        public override void InitColumnsSortEnable()
        {
            base.InitColumnsSortEnable();
            SetColumnSortEnable("ID", false);
        }

        public override void InitColumnsTitles()
        {
            base.InitColumnsTitles();
            //SetColumnTitle("ID", "Id");
            SetColumnTitle("UserID", "User Id");
            SetColumnTitle("LoginDate", "Login date");
            SetColumnTitle("LogoutDate", "Durée");
            SetColumnTitle("IpAddress", "Ip Adresse");
            SetColumnTitle("UserName", "UserName");
            SetColumnTitle("FullName", "Nom complet");
            SetColumnTitle("Email", "Email");
            SetColumnTitle("Avatar", "Avatar");
        }

        //System.Web.UI.WebControls.WebControl ContentDelegateID()
        //{
        //    Label lbl = new Label();
        //    lbl.Text = ID.ToString();
        //    return lbl;
        //}
        System.Web.UI.WebControls.WebControl ContentDelegateUserID()
        {
            Label lbl = new Label();
            lbl.Text = UserID.ToString();
            return lbl;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateLoginDate()
        {
            Label lbl = new Label();
            lbl.Text = LoginDate.ToString();
            return lbl;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateLogoutDate()
        {
            Label lbl = new Label();
            lbl.Text = DurationToString(LoginDate, LogoutDate);
            return lbl;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateIPAddress()
        {
            Label lbl = new Label();
            lbl.Text = IPAddress;
            return lbl;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateUserName()
        {
            Label lbl = new Label();
            lbl.Text = UserName;
            return lbl;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateFullName()
        {
            Label lbl = new Label();
            lbl.Text = FullName;
            return lbl;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateEmail()
        {
            System.Web.UI.WebControls.HyperLink link = new System.Web.UI.WebControls.HyperLink();

            link.NavigateUrl = "mailto:" + Email;
            link.Text = Email;

            return link;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateAvatar()
        {
            Image img = new Image();
            if (Avatar != "")
            {
                img.ImageUrl = "Avatars/" + Avatar + ".png";
            }
            else
            {
                img.ImageUrl = "Images/Anonymous.png";
            }
            img.Width = img.Height = 40;
            return img;
        }

        public static String DurationToString(DateTime start, DateTime end)
        {
            TimeSpan duration = end - start;
            double hours = Math.Truncate(duration.TotalHours);
            double minutes = Math.Truncate(duration.TotalMinutes);
            double seconds = Math.Truncate(Math.Round(100 * duration.TotalSeconds) / 100);
            return (hours < 10 ? "0" + hours.ToString() : hours.ToString()) + ":" +
                   (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":" +
                   (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString());
        }
    }
}