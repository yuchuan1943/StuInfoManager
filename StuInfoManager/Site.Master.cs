using StuInfoManager.Models;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StuInfoManager
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var UserInfo = Session["aspnetuserinfo"] as Current_UserInfo;
            if (UserInfo != null)
            {
                //如果登陆
                loginStatus.InnerHtml = "<li><a title='我的个人信息'>欢迎," + UserInfo.DisplayName +
                    "!</a></li><li><a href ='/Account/Logout.aspx' >注销</a ></li>";
            }
            else
            {
                //如果未登录
                loginStatus.InnerHtml = "<li><a href='/Account/Login.aspx'>登录</a></li>";
            }
        }
    }

}