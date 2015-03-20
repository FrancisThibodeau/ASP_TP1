using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP1
{
    public class TableUsers : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public int Online { get; set; }
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
            Online = int.Parse(this["ONLINE"]);
            Username = this["USERNAME"];
            Password = this["PASSWORD"];
            Fullname = this["FULLNAME"];
            Email = this["EMAIL"];
            Avatar = this["AVATAR"];
        }

        public override void InitColumnsVisibility()
        {
            base.InitColumnsVisibility();
            SetColumnVisibility("ID", false);
            SetColumnVisibility("PASSWORD", false);
        }

        public override void InitColumnsTitles()
        {
            base.InitColumnsTitles();
            SetColumnTitle("ONLINE", "En ligne");
            SetColumnTitle("USERNAME", "Nom d'usager");
            SetColumnTitle("FULLNAME", "Nom complet");
            SetColumnTitle("EMAIL", "Courriel");
            SetColumnTitle("AVATAR", "Avatar");
        }

        public override void InitCellsContentDelegate()
        {
            base.InitCellsContentDelegate();
            SetCellContentDelegate("AVATAR", ContentDelegateAvatar);
            SetCellContentDelegate("ONLINE", ContentDelegateOnline);
        }

        public override void Insert()
        {
            InsertRecord(Online,Username,Password,Fullname,Email,Avatar);
        }

        System.Web.UI.WebControls.WebControl ContentDelegateAvatar()
        {
            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

            if (Avatar != "")
                img.ImageUrl = "~/Avatars/" + Avatar + ".png";
            else
                img.ImageUrl = "~/Images/Anonymous.png";

            img.Height = img.Width = 40;
            return img;
        }

        System.Web.UI.WebControls.WebControl ContentDelegateOnline()
        {
            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

            if (Online != 0)
                img.ImageUrl = "~/Images/OnLine.png";
            else
                img.ImageUrl = "~/Images/OffLine.png";

            img.Height = img.Width = 40;
            return img;
        }
        public override void Update()
        {
            UpdateRecord(ID, Online, Username, Password,Fullname,Email,Avatar);
        }
        //public bool Exist(String Username)
        //{
        //    QuerySQL("SELECT * FROM " + SQLTableName + " WHERE USERNAME = '" + Username + "'");
        //    // if (reader.HasRows) GetValues();
        //    return reader.HasRows;
        //}

        //public bool GoodPassword(String Username, String Password)
        //{
        //    QuerySQL("SELECT * FROM " + SQLTableName + " WHERE USERNAME = '" + Username + "' AND PASSWORD = '" + Password + "'");
        //    // if (reader.HasRows) GetValues();
        //    return reader.HasRows;
        //}

        //public string GetEmailFromUsers(String Username)
        //{
        //    QuerySQL("SELECT EMAIL FROM " + SQLTableName + " WHERE USERNAME = '" + Username + "'");
        //    reader.Read();
        //    return reader.GetString(0);
        //}

        //public string GetPasswordFromUsers(String Username)
        //{
        //    QuerySQL("SELECT PASSWORD FROM " + SQLTableName + " WHERE USERNAME = '" + Username + "'");
        //    reader.Read();
        //    return reader.GetString(0);
        //}
        //public bool SelectByUserName(String USERNAME)
        //{
        //    string sql = "SELECT * FROM " + SQLTableName + " WHERE USERNAME = '" + USERNAME + "'";
        //    QuerySQL(sql);
        //    if (reader.HasRows)
        //        Next();
        //    return reader.HasRows;
        //}

    }
}