<%@ Page Title="学生信息管理系统" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Manage.aspx.cs" Inherits="StuInfoManager.Account.Manage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 table-responsive">
            <h4>管理员信息管理</h4>
            <hr />
            <a href="Register.aspx" class="btn btn-sm btn-primary">新增</a>
            <hr />
            <div id="tip"></div>
            <table id="table" class="table table-striped table-condensed">
                <thead>
                    <tr>
                        <th>编号</th>
                        <th>登录名</th>
                        <th>真实姓名</th>
                        <th>管理员</th>
                        <th>超级管理员</th>
                        <th>操作</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div id="restoreModal" class="modal" aria-hidden="true">
        <div class="modal-dialog" style="background: #fff">
            <div class="modal-header">
                <a class="close" data-dismiss="modal">×</a>
                <h4>重置密码</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div id="tip1"></div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">当前用户名</label>
                        <label class="col-md-10">
                            <input type="text" id="name" name="name" disabled="disabled" class="form-control" />
                        </label>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">姓名</label>
                        <label class="col-md-10">
                            <input type="text" id="displayName" name="displayName" disabled="disabled" class="form-control" />
                        </label>
                    </div>
                    <div class="form-group">
                        <label for="password" class="col-md-2 control-label">新密码</label>
                        <div class="col-md-10">
                            <input type="hidden" id="id" name="id" value="0" />
                            <input type="password" id="password" name="password" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="password2" class="col-md-2 control-label">再次输入密码</label>
                        <div class="col-md-10">
                            <input type="text" id="password2" name="password2" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <a class="btn btn-primary" onclick="restore()">确定</a>
                <a href="#" class="btn" data-dismiss="modal">返回</a>
            </div>
        </div>
    </div>
    <script>
        var dataTable;
        $(function () {
            dataTable = $("#table").dataTable({
                language: {
                    "sProcessing": "处理中...",
                    "sLengthMenu": "显示 _MENU_ 项结果",
                    "sZeroRecords": "没有匹配结果",
                    "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                    "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
                    "sInfoFiltered": "",
                    "sInfoPostFix": "",
                    "sSearch": "搜索:",
                    "sUrl": "",
                    "sEmptyTable": "表中数据为空",
                    "sLoadingRecords": "载入中...",
                    "sInfoThousands": ",",
                    "oPaginate": {
                        "sFirst": "首页",
                        "sPrevious": "上页",
                        "sNext": "下页",
                        "sLast": "末页"
                    },
                    "oAria": {
                        "sSortAscending": ": 以升序排列此列",
                        "sSortDescending": ": 以降序排列此列"
                    }
                },
                sort: false,
                ajax: {
                    url: "/Account/Handlers.ashx?option=8",
                    type: "post",
                    dataSrc: function (data) {
                        return data.data;
                    }
                },
                columns: [
                    { data: "ID" },
                    { data: "Name" },
                    { data: "DisplayName" },
                    {
                        data: "IsAdmin", render: function (data, type, full, meta) {
                            return data ? "是" : "否";
                        }
                    },
                    {
                        data: "IsSuper", render: function (data, type, full, meta) {
                            return data ? "是" : "否";
                        }
                    },
                    {
                        data: "ID", render: function (data, type, full, meta) {
                            return "<a class='btn btn-xs' title='重置密码' href='javascript:openRestore(" + data + ",\"" + full.Name + "\",\"" + full.DisplayName + "\")'><span>重置密码</span></a>" +
                                "<a class='btn btn-xs' title='删除' href='javascript:remove(" + data + ")'><span>删除</span></a>";
                        }
                    },
                ]
            })
            $("form").validate({
                rules: {
                    password: "required",
                    password2: {
                        required: true,
                        equalTo: "#password"
                    }
                },
                messages: {
                    password: "请输入密码",
                    password2: {
                        required: "请再次输入密码",
                        equalTo: "两次输入密码不一致"
                    }
                }
            })
        })
        function remove(ids) {
            if (confirm("确定要删除吗？")) {
                $.ajax({
                    url: "/Account/Handlers.ashx?option=11",
                    type: "post",
                    data: { ids: ids },
                    dataType: "json",
                    success: function (result) {
                        if (result.success) {
                            $("#tip").html("<div class='alert alert-success' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>删除成功</div>");
                            dataTable.api().ajax.reload();
                        }
                        else {
                            $("#tip").html("<div class='alert alert-danger' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>" + result.message + "</div>");
                        }
                    }

                })
            }
        }
        function openRestore(id, name, displayName) {
            $("form")[0].reset();
            $("form").valid();
            $("#restoreModal").modal("show");
            $("#id").val(id);
            $("#name").val(name);
            $("#displayName").val(displayName);
        }
        function restore() {
            if($("form").valid()){
                $.ajax({
                    url: "/Account/Handlers.ashx?option=12",
                    type: "post",
                    data: $("form").serialize(),
                    dataType: "json",
                    error: function () {

                    },
                    success: function (result) {
                        if (result.success) {
                            $("#restoreModal").modal("hide");
                            $("#tip").html("<div class='alert alert-success' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>保存成功</div>");
                        }
                        else {
                            $("#tip1").html("<div class='alert alert-danger' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>" + result.message + "</div>");
                        }
                    }
                })
            }
        }
    </script>
</asp:Content>
