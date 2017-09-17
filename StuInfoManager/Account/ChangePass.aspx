<%@ Page Title="学生信息管理系统" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChangePass.aspx.cs" Inherits="StuInfoManager.Account.ChangePass" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>修改密码</h4>
                    <hr />
                    <div id="tip"></div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">当前用户名</label>
                        <label class="col-md-10">
                            <input type="text" disabled="disabled" value="<%=UserInfo.Name %>" class="form-control" />
                        </label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">姓名</label>
                        <label class="col-md-10">
                            <input type="text" disabled="disabled" value="<%=UserInfo.DisplayName %>" class="form-control" />
                        </label>
                    </div>
                    <div class="form-group">
                        <label for="password" class="col-md-2 control-label">新密码</label>
                        <div class="col-md-10">
                            <input type="password" id="password" name="password" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="password2" class="col-md-2 control-label">再次输入密码</label>
                        <div class="col-md-10">
                            <input type="text" id="password2" name="password2" class="form-control" />
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
                        <div class="col-md-offset-2 col-md-10">
                            <input type="submit" value="确认" class="btn btn-default" />
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
                    url: "/Account/Handlers.ashx?option=4",
                    data: $(form).serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (result.success) {
                            $("#tip").html("<div class='alert alert-success' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>修改密码成功，请牢记新密码</div>");
                        }
                        else {
                            $("#tip").html("<div class='alert alert-danger' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>" + result.message + "</div>");
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
                    password: "required",
                    password2: {
                        required: true,
                        equalTo: "#password"
                    },
                    validateCode: "required"
                },
                messages: {
                    password: "请输入密码",
                    password2: {
                        required: "请再次输入密码",
                        equalTo: "两次输入密码不一致"
                    },
                    validateCode: "请输入验证码"
                }
            });
        })
    </script>
</asp:Content>
