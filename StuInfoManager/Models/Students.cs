using System;
using System.Collections.Generic;
using System.Web;

namespace StuInfoManager.Models
{
    public class Students
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public DateTime Birth { get; set; }
        public string Nj { get; set; }
        public string Bj { get; set; }
        public string Zy { get; set; }
        public string Ss { get; set; }
        public string Ch { get; set; }
        public string Address { get; set; }
        public string Dh { get; set; }
        public int IsValid { get; set; }
        public int UserId { get; set; }
        public string DisplayName { get; set; }
    }
}