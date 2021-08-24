using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FoodDelivery.DBModel;
using FoodDelivery.Models;

namespace FoodDelivery.Controllers
{

    public class FoodController : Controller
    {
        FoodDeliveryDBEntities _dbEntities = new FoodDeliveryDBEntities();
        // GET: Food
        //[Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult Add(FoodModel food)
        {
            if (ModelState.IsValid)
            {
                if (_dbEntities.Foods.Where(x => x.Name.ToLower().Equals(food.Name.ToLower())).FirstOrDefault() != null)
                {
                    food.Message = "Food " + food.Name + " already exists.";
                    ModelState.AddModelError("Error", "Food " + food.Name + " already exists.");
                    return View(food);
                }

                Food newFood = new Food();
                string fileName = Path.GetFileNameWithoutExtension(food.ImgFile.FileName);
                string extension = Path.GetExtension(food.ImgFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                food.ImgLocation = "~/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                food.ImgFile.SaveAs(fileName);

                newFood.Name = food.Name;
                newFood.Price = food.Price;
                newFood.Created = DateTime.Now;
                newFood.Updated = DateTime.Now;
                newFood.Description = String.IsNullOrEmpty(food.Description) ? null : food.Description;
                newFood.Picture = food.ImgLocation;

                _dbEntities.Foods.Add(newFood);
                _dbEntities.SaveChanges();
                ModelState.Clear();
            }
            return View(food);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Food food = new Food();
            food = _dbEntities.Foods.Where(x => x.FoodID == id).FirstOrDefault();
            return View(food);
        }

        [HttpGet]
        public ActionResult List()
        {
            List<Food> listFood = _dbEntities.Foods.ToList();
            return View(listFood);
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Food food = new Food();
            food = _dbEntities.Foods.Where(x => x.FoodID == id).FirstOrDefault();
            if (food != null)
            {
                _dbEntities.Foods.Remove(food);
                _dbEntities.SaveChanges();
            }
            return View("List", _dbEntities.Foods);
        }


        [HttpPost]
        //[Authorize]
        public ActionResult Order(int food_id, string username)
        {
            //if (System.Web.HttpContext.Current != null &&
            //    System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    MembershipUser usr = Membership.GetUser();
            //    if (usr != null)
            //    {
            //        userName = usr.UserName;
            //    }
            //}
            UpdateRestaurants();
            if (_dbEntities.Users.Where(x => x.Username == username).FirstOrDefault() == null)
            {
                ModelState.AddModelError("Error", "You have to be logged in if you want to order food.");
                return View("Order", null);
            }
            Food orderedFood = _dbEntities.Foods.Where(x => x.FoodID == food_id).FirstOrDefault();
            User orderingUser = _dbEntities.Users.Where(x => x.Username == username).FirstOrDefault();
            List<Restaurant> availableRestaurants = _dbEntities.Restaurants.Where(x => x.IsAvailable).ToList();
            List<RestaurantDistance> restDistList = new List<RestaurantDistance>();
            foreach (Restaurant rest in availableRestaurants)
            {
                double temp1 = distance((double)rest.Latitude, (double)rest.Longitude, (double)orderingUser.Latitude, (double)orderingUser.Longitude, 'M');
                RestaurantDistance temp = new RestaurantDistance(temp1, rest);
                restDistList.Add(temp);
            }
            Restaurant minDist = restDistList.OrderBy(x => x.Distance).FirstOrDefault().Restaurant;
            minDist.IsAvailable = false;
            minDist.Updated = DateTime.Now;
            _dbEntities.SaveChanges();
            OrderModel model = new OrderModel();
            model.FoodName = orderedFood.Name;
            model.Username = orderingUser.Username;
            model.RestaurantName = minDist.Name;
            return View("Order", model);
        }
        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;

            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }

            return (dist);

        }
        private double deg2rad(double deg)
        {

            return (deg * Math.PI / 180.0);

        }
        private double rad2deg(double rad)
        {

            return (rad / Math.PI * 180.0);
        }
        //Console.WriteLine(distance(32.9697, -96.80322, 29.46786, -98.53506, "M"));
        public void UpdateRestaurants()
        {
            DateTime now = DateTime.Now;
            List<Restaurant> listRest = _dbEntities.Restaurants.Where(x => !x.IsAvailable).ToList();
            foreach (Restaurant rest in listRest)
            {
                if (now.Subtract(rest.Updated).TotalMinutes > 15)
                {
                    rest.Updated = DateTime.Now;
                    rest.IsAvailable = true;
                    _dbEntities.SaveChanges();
                }
            }
        }
    }
}