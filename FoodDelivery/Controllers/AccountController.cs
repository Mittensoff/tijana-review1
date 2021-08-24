using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.DBModel;
using FoodDelivery.Models;

namespace FoodDelivery.Controllers
{
    public class AccountController : Controller
    {
        FoodDeliveryDBEntities _dbEntities = new FoodDeliveryDBEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            UserModel user = new UserModel();
            return View(user);
        }
        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (!_dbEntities.Users.Any(m => m.Username == user.Username))
                {
                    User newUser = new User();
                    newUser.Updated = DateTime.Now;
                    newUser.Created = DateTime.Now;
                    newUser.Username = user.Username;
                    newUser.Password = user.Password;
                    newUser.Latitude = user.Latitude == null ? null : user.Latitude;
                    newUser.Longitude = user.Longitude == null ? null : user.Longitude;

                    _dbEntities.Users.Add(newUser);
                    _dbEntities.SaveChanges();
                    user = new UserModel();
                    user.SuccessMessage = "New user is successfully added.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    user.SuccessMessage = "User with " + user.Username + " username already exists.";
                    return View("Register");
                }
            }
            return RedirectToAction("Index", "Home");
        }



        public ActionResult Login()
        {
            UserLoginModel user = new UserLoginModel();
            return View(user);
        }
        [HttpPost]
        public ActionResult Login(UserLoginModel user)
        {
            if (ModelState.IsValid)
            {
                if (_dbEntities.Users.Where(m => m.Username == user.Username && m.Password.Equals(user.Password)).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error", "Invalid Username/Password.");
                    return View();
                }
                else
                {
                    Session["Username"] = user.Username;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}