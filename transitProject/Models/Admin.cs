using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace transitProject.Models
{
    public class Admin
    {
        [Required(ErrorMessage = "Please enter the Admin Id")]
        [DisplayName("Admin ID")]
   
        public int adminId { get; set; }
        //
        [Required(ErrorMessage = "Please enter the User name")]
        [DisplayName("UserName")]
        public string userName { get; set; }
        //
        [Required(ErrorMessage = "Please enter the password")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}