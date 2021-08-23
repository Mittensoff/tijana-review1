using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.DBModel;

namespace FoodDelivery.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RestaurantController : Controller
    {
        FoodDeliveryDBEntities _dbEntities = new FoodDeliveryDBEntities();
        // GET: Restaurant
        public ActionResult List()
        {
            return View(_dbEntities.Restaurants);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Restaurant rest)
        {
            if (_dbEntities.Restaurants.Where(x => x.Name.ToLower().Equals(rest.Name.ToLower())).FirstOrDefault() != null)
            {
                ModelState.AddModelError("Error", "Food " + rest.Name + " already exists.");
                return View(rest);
            }
            Restaurant newRest = new Restaurant();
            newRest.Name = rest.Name;
            newRest.Latitude = rest.Latitude;
            newRest.Longitude = rest.Longitude;
            newRest.IsAvailable = true;
            newRest.Created = DateTime.Now;
            newRest.Updated = DateTime.Now;
            _dbEntities.Restaurants.Add(newRest);
            _dbEntities.SaveChanges();
            return View("List", _dbEntities.Restaurants);
        }
    }
}