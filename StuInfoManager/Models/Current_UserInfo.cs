using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StuInfoManager.Models
{
    public class Current_UserInfo
    {

        #region 私有变量
        private bool isvalid;
        private int id;
        private string name;
        private string displayname;
        private bool? isadmin;
        private bool? issuper;
        #endregion

        public Current_UserInfo()
        {
            isvalid = true;
            id = 1;
            name = "admin";
            displayname = "管理员";
            isadmin = true;
            issuper = true;
        }
        public Current_UserInfo(string userName, string password)
        {
            Helper helper = new Helper();
            Users userInfo = helper.Login(userName, password);
            if (userInfo != null)
            {
                isvalid = true;
                id = userInfo.ID;
                name = userInfo.Name;
                displayname = userInfo.DisplayName;
                isadmin = userInfo.IsAdmin;
                issuper = userInfo.IsSuper;
            }
        }
        public int ID { get { return id; } }
        public bool IsValid { get { return isvalid; } }
        public string Name { get { return name; } }
        public string DisplayName { get { return displayname; } }
        public bool IsAdmin { get { return (bool)isadmin; } }
        public bool IsSuper { get { return (bool)issuper; } }

    }
}