using System;
using System.Collections.Generic;
using System.Web;

namespace StuInfoManager.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuper { get; set; }
        public bool IsValid { get; set; }
    }
}