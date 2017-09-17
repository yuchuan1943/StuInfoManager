using StuInfoManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StuInfoManager
{
    public partial class Validate : System.Web.UI.Page
    {
        protected Current_UserInfo UserInfo
        {
            get
            {
                return Session["aspnetuserinfo"] as Current_UserInfo;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (UserInfo == null)
            {
                Response.Redirect("/Account/Login.aspx");
            }
        }
    }
}