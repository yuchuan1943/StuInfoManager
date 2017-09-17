using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StuInfoManager.Account
{
    public partial class Manage : Validate
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserInfo.IsSuper)
            {
                Response.Write("<font color='red'>您没有访问权限</font>");
                Response.End();
            }
        }
    }
}