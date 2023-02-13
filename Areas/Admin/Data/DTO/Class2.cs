using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fruitkha_main.Models;
using System.ComponentModel.DataAnnotations;

namespace fruitkha_main.Areas.Admin.Data.DTO
{
    public class Class2
    {
        [Required(ErrorMessage = "Not null")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Not null")]
        [DataType(DataType.Password)]
        public string Passwords { get; set; }
    }
}