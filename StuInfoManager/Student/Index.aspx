<%@ Page Title="学生信息管理系统" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Index.aspx.cs" Inherits="StuInfoManager.Student.Index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h4>学生信息管理</h4>
            <hr />
            <div id="tip1"></div>
            <button type="button" class="btn btn-primary btn-sm" onclick="add()" data-toggle="modal">新增</button>
            <button type="button" class="btn btn-danger btn-sm" onclick="removeRange()">删除</button>
        </div>
        <div class="col-md-12 table-responsive">
            <hr />
            <table id="data" class="table table-striped table-condensed">
                <thead>
                    <tr>
                        <th style="width: 15px;">
                            <input type="checkbox" id="checkall" onclick="checkAll()" />
                        </th>
                        <th>学号</th>
                        <th>姓名</th>
                        <th>性别</th>
                        <th>年级</th>
                        <th>班级</th>
                        <th>专业</th>
                        <th>宿舍</th>
                        <th>操作</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div id="operModal" class="modal" aria-hidden="true">
        <div class="modal-dialog" style="background: #fff">
            <div class="modal-header">
                <a class="close" data-dismiss="modal">×</a>
                <h4 id="operTitle"></h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div id="tip"></div>
                    <div class="form-group">
                        <label for="code" class="col-md-2 control-label">学号</label>
                        <div class="col-md-10">
                            <input type="hidden" id="id" name="id" value="0" />
                            <input type="text" id="code" name="code" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="name" class="col-md-2 control-label">姓名</label>
                        <div class="col-md-10">
                            <input type="text" id="name" name="name" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">选择性别</label>
                        <div class="col-md-10">
                            <label for="man">男</label>
                            <input type="radio" id="man" checked="checked" name="sex" value="man" />
                            <label for="girl">女</label>
                            <input type="radio" id="girl" name="sex" value="girl" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="birth" class="col-md-2 control-label">出生日期</label>
                        <div class="col-md-10">
                            <input type="text" id="birth" name="birth" class="form-control" onclick="WdatePicker()" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="nj" class="col-md-2 control-label">年级</label>
                        <div class="col-md-10">
                            <input type="text" id="nj" name="nj" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="bj" class="col-md-2 control-label">班级</label>
                        <div class="col-md-10">
                            <input type="text" id="bj" name="bj" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="zy" class="col-md-2 control-label">专业</label>
                        <div class="col-md-10">
                            <input type="text" id="zy" name="zy" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="ss" class="col-md-2 control-label">宿舍</label>
                        <div class="col-md-10">
                            <input type="text" id="ss" name="ss" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="ch" class="col-md-2 control-label">床号</label>
                        <div class="col-md-10">
                            <input type="text" id="ch" name="ch" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="address" class="col-md-2 control-label">地址</label>
                        <div class="col-md-10">
                            <input type="text" id="address" name="address" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="dh" class="col-md-2 control-label">电话</label>
                        <div class="col-md-10">
                            <input type="text" id="dh" name="dh" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <a class="btn btn-success" onclick="insertOrUpdate()">确认</a>
                <a class="btn" data-dismiss="modal">取消</a>
            </div>
        </div>
    </div>
    <div id="infoModal" class="modal" aria-hidden="true">
        <div class="modal-dialog" style="background: #fff">
            <div class="modal-header">
                <a class="close" data-dismiss="modal">×</a>
                <h4>详细信息</h4>
            </div>
            <div id="infoBody" class="modal-body">
               
            </div>
            <div class="modal-footer">
                <a href="#" class="btn" data-dismiss="modal">返回</a>
            </div>
        </div>
    </div>
    <script src="/js/common.js"></script>
    <script src="../js/WdatePicker.js"></script>
</asp:Content>
