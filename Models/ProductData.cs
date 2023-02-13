using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fruitkha_main.Models
{
    public class ProductData
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> category_id { get; set; }
        public string avatar { get; set; }
        public string origin { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> supplier_id { get; set; }
        public string detail { get; set; }
        public Nullable<int> sale { get; set; }
        public Nullable<int> qty { get; set; }
    }
}