using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDelivery.Models
{
    public class OrderModel
    {
        [Display(Name = "User who ordered: ")]
        public string UserName { get; set; }

        [Display(Name = "Food: ")]
        public string FoodName { get; set; }

        [Display(Name = "From Restaurant: ")]
        public string RestaurantName { get; set; }
    }
}