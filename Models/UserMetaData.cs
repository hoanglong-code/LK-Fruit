using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fruitkha_main.Models
{
    public class UserMetaData
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string role { get; set; }
    }
}