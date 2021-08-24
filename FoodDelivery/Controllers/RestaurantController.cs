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
            if (!string.IsNullOrEmpty(rest.Name))
            {
                if (_dbEntities.Restaurants.Where(x => x.Name.ToLower().Equals(rest.Name.ToLower())).FirstOrDefault() != null)
                {
                    ModelState.AddModelError("Error", "Restaurant " + rest.Name + " already exists.");
                    return View(rest);
                }
            }
            Restaurant newRest = new Restaurant();
            newRest.Name = string.IsNullOrEmpty(rest.Name) ? null : rest.Name;
            if (rest.Latitude == 0 || rest.Longitude == 0)
            {
                ModelState.AddModelError("Error", "You have to insert lat/long values.");
                return View(rest);
            }

            else
            {
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
}