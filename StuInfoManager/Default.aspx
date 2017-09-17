<%@ Page Title="学生信息管理系统" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="StuInfoManager.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-2">
            <h4>欢迎，<%=UserInfo.DisplayName %></h4>
            <hr />
            <a href="/Student/Index.aspx" class="btn">学生信息管理</a>
            <br />
            <a href="/Account/Manage.aspx" class="btn">管理员信息</a>
            <br />
            <a href="/Account/ChangePass.aspx" class="btn">修改密码</a>
        </div>
        <div class="col-md-10">

        </div>
    </div>
</asp:Content>
