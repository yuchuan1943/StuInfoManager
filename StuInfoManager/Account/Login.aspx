<%@ Page Title="登陆" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="StuInfoManager.Account.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>请输入登陆信息</h4>
                    <hr />
                    <div id="tip"></div>
                    <div class="form-group">
                        <label for="userName" class="col-md-2 control-label">用户名</label>
                        <div class="col-md-10">
                            <input type="text" id="userName" name="userName" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="password" class="col-md-2 control-label">密码</label>
                        <div class="col-md-10">
                            <input type="password" id="password" name="password" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="validateCode" class="col-md-2 control-label">验证码</label>
                        <div class="col-md-4">
                            <input type="text" id="validateCode" name="validateCode" class="form-control" />
                        </div>
                        <div class="col-md-6">
                            <img src="/images/validatecode.aspx" title="看不清？换一张" alt="验证码" onclick="this.src='/images/validatecode.aspx?code=' + new Date().getTime()" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-10">
                            <input type="checkbox" id="rememberMe" name="rememberMe" />
                            <label for="rememberMe">记住我</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <button type="submit" class="btn btn-default">登陆<span id="loading"></span></button>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
    <script>
        $.validator.setDefaults({
            submitHandler: function (form) {
                $.ajax({
                    url: "/Account/Handlers.ashx?option=1",
                    data: $(form).serialize(),
                    type: "post",
                    dataType: "json",
                    beforeSend: function () {
                        $("#loading").html("&nbsp;<img style='height:15px;height:15px;' src='/images/loading.gif' />");
                    },
                    complete: function () {
                        $("#loading").html("");
                    },
                    success: function (result) {
                        if (result.success) {
                            $("#tip").html("<div class='alert alert-success' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>登录成功,正在跳转……</div>");
                            if (document.getElementById("rememberMe").checked) {
                                $.cookie("aspusername", $("#userName").val(), { path: "/", expires: 7 });
                            }
                            location = "/Default.aspx";
                        }
                        else {
                            $("#tip").html("<div class='alert alert-danger' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>" + result.message + "</div>");
                            $("#tip").focus();
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                    }
                });
            }
        });
        $(function () {
            $("form").validate({
                rules: {
                    userName: "required",
                    password: "required",
                    validateCode: "required"
                },
                messages: {
                    userName: "请输入用户名",
                    password: "请输入密码",
                    validateCode: "请输入验证码"
                }
            });
            if ($.cookie("aspusername") != undefined && $.cookie("aspusername") != null && $.cookie("aspusername") != "") {
                $("#userName").val($.cookie("aspusername"));
                document.getElementById("rememberMe").checked = true;
            }
        })
    </script>
</asp:Content>
