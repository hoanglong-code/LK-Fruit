using fruitkha_main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fruitkha_main.Areas.Admin.Data.DAO
{
    public class Class1
    {
        private static Models.LKFRUITEntities db = new Models.LKFRUITEntities();
        public Class1()
        {
        }
        public static int CheckLogin(String Username,String Passwords)
        {
            int num = db.admins.Count(x => x.username == Username && x.password == Passwords );
            if (num > 0) { return 1; }        
            return 0;
        }
    }
}
