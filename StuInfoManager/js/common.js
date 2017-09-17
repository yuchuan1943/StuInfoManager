var dataTable;
$(function () {
    dataTable = $("#data").dataTable({
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
            url: "/Account/Handlers.ashx?option=5",
            type: "post",
            dataSrc: function (data) {
                return data.data;
            }
        },
        columns: [
            {
                data: "ID", render: function (data, type, full, meta) {
                    return "<input type='checkbox' name='check' value='" + data + "' />";
                }
            },
            { data: "Code" },
            { data: "Name" },
            {
                data: "Sex", render: function (data, type, full, meta) {
                    return data ? "男" : "女";
                }
            },
            { data: "Nj" },
            { data: "Bj" },
            { data: "Zy" },
            { data: "Ss" },
            {
                data: "ID", render: function (data, type, full, meta) {
                    return "<a class='btn btn-xs tipinfo' title='详细' href='javascript:info(" + data + ")' ><span>详细</span></a>" +
                        "<a class='btn btn-xs' title='编辑' href='javascript:edit(" + data + ")'><span>编辑</span></a>" +
                        "<a class='btn btn-xs' title='删除' href='javascript:remove(" + data + ")'><span>删除</span></a>";
                }
            }
        ]
    })
    $("form").validate({
        rules: {
            code: {
                required: true,
                remote: {
                    url: "/Account/Handlers.ashx?option=9",
                    type: "post",
                    dataType: "json",
                    data: {
                        code: function () {
                            return $("#code").val();
                        },
                        id: function () {
                            return $("#id").val();
                        }
                    }
                },
            },
            name: "required",
            birth: {
                required: true,
                date: true
            },
            nj: "required",
            bj: "required",
            zy: "required",
            ss: "required",
            ch: "required"
        },
        messages: {
            code: {
                required: true,
                remote: "已存在此学号"
            },
            name: "请输入学生姓名",
            birth: {
                required: "请输入出生日期",
                date: "输入的日期不合法"
            },
            nj: "请输入年级",
            bj: "请输入班级",
            zy: "请输入专业",
            ss: "请输入宿舍号",
            ch: "请输入床号"
        }
    })
})
function checkAll() {
    var checks = document.getElementsByName("check");
    for (var i = 0; i < checks.length; i++) {
        checks[i].checked = document.getElementById("checkall").checked;
    }
}
function add() {
    $("form")[0].reset();
    $("form").valid();
    $("#operTitle").html("添加");
    $("#operModal").modal("show");
}
function info(id) {
    $("#infoBody").html("");
    $("#infoModal").modal("show");
    $.ajax({
        url: "/Account/Handlers.ashx?option=6",
        type: "post",
        data: { id: id },
        dataType: "json",
        error: function () {

        },
        success: function (result) {
            if (result.success) {
                var htmlr = '<div class="form-horizontal">';
                htmlr += '<div class="form-group"><label class="col-md-2">出生日期</label><div class="col-md-10">' + dateFormat(result.data.Birth) + '</div></div>';
                htmlr += '<div class="form-group"><label class="col-md-2">床号</label><div class="col-md-10">' + result.data.Ch + '</div></div>';
                htmlr += '<div class="form-group"><label class="col-md-2">地址</label><div class="col-md-10">' + result.data.Address + '</div></div>';
                htmlr += '<div class="form-group"><label class="col-md-2">电话</label><div class="col-md-10">' + result.data.Dh + '</div></div>';
                htmlr += '<div class="form-group"><label class="col-md-2">录入人员</label><div class="col-md-10">' + result.data.DisplayName + '</div></div>';
                htmlr += '</div>';
                $("#infoBody").html(htmlr);
            }
            else {
                $("#infoBody").text(result.message);
            }
        }
    })
}
function edit(id) {
    $("form")[0].reset();
    $("form").valid()
    //回填
    $.ajax({
        url: "/Account/Handlers.ashx?option=6",
        type: "post",
        data: { id: id },
        dataType: "json",
        error: function () {

        },
        success: function (result) {
            if (result.success) {
                $("#id").val(result.data.ID);
                $("#code").val(result.data.Code);
                $("#name").val(result.data.Name);
                $("#birth").val(dateFormat(result.data.Birth));
                if (result.data.Sex) {
                    $("#man").attr("checked", "checked");
                }
                else {
                    $("#girl").attr("checked", "checked");
                }
                $("#nj").val(result.data.Nj);
                $("#bj").val(result.data.Bj);
                $("#zy").val(result.data.Zy);
                $("#ss").val(result.data.Ss);
                $("#ch").val(result.data.Ch);
                $("#address").val(result.data.Address);
                $("#dh").val(result.data.Dh);
                $("#operTitle").html("修改");
                $("#operModal").modal("show");
            }
            else {
                $("#tip1").html("<div class='alert alert-danger' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>" + result.message + "</div>");
            }
        }
    })
}
function insertOrUpdate() {
    if ($("form").valid()) {
        $.ajax({
            url: "/Account/Handlers.ashx?option=7",
            type: "post",
            data: $("form").serialize(),
            dataType: "json",
            error: function () {

            },
            success: function (result) {
                if (result.success) {
                    $("#operModal").modal("hide");
                    $("#tip1").html("<div class='alert alert-success' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>保存成功</div>");
                    dataTable.api().ajax.reload();
                }
                else {
                    $("#tip").html("<div class='alert alert-danger' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>" + result.message + "</div>");
                }
            }
        })
    }
}
function removeRange() {
    var ids = "";
    $("[name=check]:checked").each(function () {
        if (ids != "") {
            ids += ",";
        }
        ids += this.value;
    })
    if (ids == "")
        return alert("请选择学生信息");
    remove(ids);
}
function remove(ids) {
    if (confirm("确定要删除吗？")) {
        $.ajax({
            url: "/Account/Handlers.ashx?option=10",
            type: "post",
            data: { ids: ids },
            dataType: "json",
            success: function (result) {
                if (result.success) {
                    $("#tip1").html("<div class='alert alert-success' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>删除成功</div>");
                    dataTable.api().ajax.reload();
                }
                else {
                    $("#tip1").html("<div class='alert alert-danger' role='alert'><button data-dismiss='alert' class='close' type='button'>×</button>" + result.message + "</div>");
                }
            }

        })
    }
}
function dateFormat(date) {
    var arr = date.split('T');
    return (arr[0]);
}