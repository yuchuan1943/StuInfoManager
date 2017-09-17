<%@ Page Title="学生信息管理系统" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="StuInfoManager.Account.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <section id="registerForm">
                <div class="form-horizontal">
                    <h4>请输入注册信息</h4>
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
                        <label for="password2" class="col-md-2 control-label">再次输入密码</label>
                        <div class="col-md-10">
                            <input type="text" id="password2" name="password2" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="displayName" class="col-md-2 control-label">姓名</label>
                        <div class="col-md-10">
                            <input type="text" id="displayName" name="displayName" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-10">
                            <input type="checkbox" id="isAdmin" checked="checked" name="isAdmin" />
                            <label for="isAdmin">管理员权限</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label"></label>
                        <div class="col-md-10">
                            <input type="checkbox" id="isSuper" name="isSuper" />
                            <label for="isSuper">超级管理员权限</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <button type="submit" class="btn btn-default">确认<span id="loading"></span></button>
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
                    url: "/Account/Handlers.ashx?option=2",
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
                            $("#tip").html("<div class='alert alert-success' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>添加成功</div>");
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
                    userName: {
                        required: true,
                        remote: "/Account/Handlers.ashx?option=3"
                    },
                    password: "required",
                    password2: {
                        required: true,
                        equalTo: "#password"
                    },
                    displayName: "required"
                },
                messages: {
                    userName: {
                        required: "请输入用户名",
                        remote: "该用户名已存在"
                    },
                    password: "请输入密码",
                    password2: {
                        required: "请再次输入密码",
                        equalTo: "两次密码输入不一致"
                    },
                    displayName: "请输入姓名"
                }
            });
        })
    </script>
</asp:Content>
