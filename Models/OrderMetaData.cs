using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fruitkha_main.Models
{
    public class OrderMetaData
    {
        public int id { get; set; }
        public Nullable<int> bill_id { get; set; }
        public Nullable<int> us_id { get; set; }
        public Nullable<int> prd_id { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> old_price { get; set; }
        public Nullable<int> sale { get; set; }
        public Nullable<int> qty { get; set; }
        public string avatar { get; set; }
        public string name { get; set; }

        public virtual bill bill { get; set; }
        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
}