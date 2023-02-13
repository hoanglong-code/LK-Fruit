using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace fruitkha_main.Models
{
    public class BillMetaData
    {
        public int id { get; set; }
        public Nullable<int> us_id { get; set; }
        public Nullable<int> bill_id { get; set; }
        public Nullable<int> total { get; set; }
        public string address { get; set; }
        public string status { get; set; }

 
    }
}