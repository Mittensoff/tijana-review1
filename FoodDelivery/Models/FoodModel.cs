using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDelivery.Models
{
    //TODO: 
    // Ovi modeli su u stvari DTO
    public class FoodModel
    {
        [Required(ErrorMessage = "Please enter name of your food.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price field cannot be empty.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Upload picture: ")]
        public string ImgLocation { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public string Message { get; set; }
    }
}