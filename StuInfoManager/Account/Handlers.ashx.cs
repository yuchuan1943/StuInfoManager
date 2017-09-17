using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using StuInfoManager.Models;
using System.Web.SessionState;
using System.ComponentModel;

namespace StuInfoManager.Account
{
    /// <summary>
    /// Handlers 的摘要说明
    /// </summary>
    public class Handlers : IHttpHandler, IRequiresSessionState
    {
        Helper helper = new Helper();
        protected Current_UserInfo UserInfo
        {
            get
            {
                return HttpContext.Current.Session["aspnetuserinfo"] as Current_UserInfo;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            var result = new Result();
            try
            {
                if (!string.IsNullOrEmpty(context.Request["option"]))
                {
                    var option = int.Parse(context.Request["option"]);
                    switch (option)
                    {
                        //登陆
                        case (int)Option.Login:
                            result = Login(context);
                            break;

                        case (int)Option.Register:
                            result = Register(context);
                            break;
                        case (int)Option.CheckUser:
                            result = CheckUser(context);
                            context.Response.Write(JsonConvert.SerializeObject(result.success));
                            return;
                        case (int)Option.ChangePass:
                            result = ChangePass(context);
                            break;
                        case (int)Option.LoadStudents:
                            result = LoadStudents(context);
                            break;
                        case (int)Option.LoadStudent:
                            result = LoadStudent(context);
                            break;
                        case (int)Option.InsertOrUpdateStudent:
                            result = InsertOrUpdateStudent(context);
                            break;
                        case (int)Option.LoadUsers:
                            result = LoadUsers(context);
                            break;
                        case (int)Option.CheckStudent:
                            result = CheckStudent(context);
                            context.Response.Write(JsonConvert.SerializeObject(result.success));
                            return;
                        case (int)Option.RemoveStudents:
                            result = RemoveStudents(context);
                            break;
                        case (int)Option.RemoveUsers:
                            result = RemoveUsers(context);
                            break;
                        case (int)Option.RestorePass:
                            result = RestorePass(context);
                            break;
                        default:
                            result.message = "参数错误";
                            break;
                    }
                }
                else
                {
                    result.message = "参数错误";
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }

            //返回结果
            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        private Result RemoveUsers(HttpContext context)
        {
            var result = new Result();
            if (!UserInfo.IsSuper)
            {
                result.success = false;
                result.message = "非超级管理员没有操作权限";
            }
            else
            {
                var ids = context.Request["ids"].Trim();
                helper.RemoveUsers(ids);
                result.success = true;
            }
            return result;
        }

        private Result RemoveStudents(HttpContext context)
        {
            var result = new Result();
            if (!UserInfo.IsAdmin)
            {
                result.success = false;
                result.message = "非管理员没有操作权限";
            }
            else
            {
                var ids = context.Request["ids"].Trim();
                helper.RemoveStudents(ids);
                result.success = true;
            }
            return result;
        }

        private Result CheckStudent(HttpContext context)
        {
            var result = new Result();
            var id = int.Parse(context.Request["id"].Trim());
            var code = context.Request["code"].Trim();
            result.success = helper.CheckStudent(code, id);
            return result;
        }

        private Result LoadUsers(HttpContext context)
        {
            var result = new Result();
            result.success = true;
            result.data = helper.LoadUsers();
            return result;
        }

        private Result InsertOrUpdateStudent(HttpContext context)
        {
            var result = new Result();
            if (UserInfo == null)
            {
                result.success = false;
                result.message = "登陆超时，请重新登陆";
            }
            else if (!UserInfo.IsAdmin)
            {
                result.success = false;
                result.message = "非管理员没有操作权限";
            }
            else
            {
                var student = new Students
                {
                    ID = int.Parse(context.Request["id"] ?? "0"),
                    Code = context.Request["code"].Trim(),
                    Name = context.Request["name"].Trim(),
                    Birth = DateTime.Parse(context.Request["birth"].Trim()),
                    Nj = context.Request["nj"].Trim(),
                    Bj = context.Request["bj"].Trim(),
                    Zy = context.Request["zy"].Trim(),
                    Ss = context.Request["ss"].Trim(),
                    Ch = context.Request["ch"].Trim(),
                    Address = context.Request["address"].Trim(),
                    Dh = context.Request["dh"].Trim(),
                    UserId = UserInfo.ID
                };
                if (context.Request["sex"] == "man")
                    student.Sex = true;
                if (context.Request["sex"] == "girl")
                    student.Sex = false;
                if (student.ID == 0)
                {
                    helper.InsertStudent(student);
                }
                else
                {
                    helper.UpdateStudent(student);
                }
                result.success = true;
            }
            return result;
        }

        private Result LoadStudent(HttpContext context)
        {
            var result = new Result();
            var id = int.Parse(context.Request["id"]);
            result.success = true;
            result.data = helper.LoadStudent(id);
            return result;
        }

        //登陆
        private Result Login(HttpContext context)
        {
            var result = new Result();
            if (context.Request["validateCode"].Trim().ToLower() != (string)context.Session["ValidateCode"])
            {
                result.message = "验证码错误";
                return result;
            }
            var userInfo = new Current_UserInfo(context.Request["userName"].Trim(), context.Request["password"].Trim());
            if (!userInfo.IsValid)
            {
                result.message = "用户名或密码错误";
                return result;
            }
            context.Session["aspnetuserinfo"] = userInfo;
            result.success = true;
            return result;
        }

        //注册
        private Result Register(HttpContext context)
        {
            var result = new Result();
            var userinfo = new Users
            {
                Name = context.Request["userName"].Trim(),
                Password = context.Request["password"].Trim(),
                DisplayName = context.Request["displayName"].Trim(),
                IsAdmin = context.Request["isAdmin"] == "on",
                IsSuper = context.Request["isSuper"] == "on"
            };
            helper.InsertUser(userinfo);
            result.success = true;
            return result;
        }

        //校验用户名是否重复
        private Result CheckUser(HttpContext context)
        {
            var result = new Result();
            var userName = context.Request["userName"].Trim();
            result.success = helper.CheckUser(userName);
            return result;
        }

        //超级管理员重置密码
        private Result RestorePass(HttpContext context)
        {
            var result = new Result();
            if (UserInfo == null)
            {
                result.success = false;
                result.message = "登陆超时，请重新登陆";
            }
            else
            {
                helper.ChangePass(new Users
                {
                    ID = int.Parse(context.Request["id"].Trim()),
                    Password = context.Request["password"].Trim()
                });
                result.success = true;
            }
            return result;
        }

        //管理员自行修改密码
        private Result ChangePass(HttpContext context)
        {
            var result = new Result();
            if (UserInfo == null)
            {
                result.success = false;
                result.message = "登陆超时，请重新登陆";
            }
            else
            {
                helper.ChangePass(new Users { ID = UserInfo.ID, Password = context.Request["password"].Trim() });
                result.success = true;
            }
            return result;
        }

        private Result LoadStudents(HttpContext context)
        {
            var result = new Result();
            var data = helper.LoadStudents();
            result.success = true;
            result.data = data;
            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    enum Option
    {
        [Description("登陆")]
        Login = 1,
        [Description("添加管理员")]
        Register = 2,
        [Description("检查用户")]
        CheckUser = 3,
        [Description("修改密码")]
        ChangePass = 4,
        [Description("加载所有学生信息")]
        LoadStudents = 5,
        [Description("加载指定学生信息")]
        LoadStudent = 6,
        [Description("添加或更新学生信息")]
        InsertOrUpdateStudent = 7,
        [Description("加载所有管理员信息")]
        LoadUsers = 8,
        [Description("检查学号")]
        CheckStudent = 9,
        [Description("删除学生")]
        RemoveStudents = 10,
        [Description("删除管理员")]
        RemoveUsers = 11,
        [Description("重置密码")]
        RestorePass = 12,
    }

    public class Result
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

}