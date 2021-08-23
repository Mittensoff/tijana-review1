using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDelivery.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}