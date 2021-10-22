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
        //TODO:
        // Nepotreban endpoint, ne treba ni dozvoliti (frontend validacija) da prazan request sa register view-a dodje do backenda
        public ActionResult Register()
        {
            UserModel user = new UserModel();
            return View(user);
        }

        //TODO:  
        // 1. Izraze u If naredbama, kao ovdje _dbEntities ..., treba u varijalbu pa u If, u slucaju kompleksnijeg If-a, nepregledno
        // 2. !_dbEntities.Users.Any(m => m.Username == user.Username treba ubaciti u custom Validator
        // 3. User user = new UserModel(...);
        //      nepotrebno vracati prazan model/dto, posebno ne zbog SuccessMessage-a
        // 4. newUser.Latitude = user.Latitude == null ? null : user.Latitude;
        //      nepotrebno linija, nista ne radi. ucitati neku default vrijednost za geografsku duzinu npr. 45.0000 ili null 
        // 5. Pogresna inicijalizacija User objekta, inicijalizovati na osnovu nekog od UserDto(s) (ovdje UserModel-User mapiranje) 
        // 6. Poruka SuccessMessage je dio validacije (vezano s 2.)
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
                    user.SuccessMessage = "New user is successfully added. Now, you can log in.";
                    return View("Register", user);
                }
                else
                {
                    user.SuccessMessage = "User with " + user.Username + " username already exists.";
                    return View("Register", user);
                }
            }
            return View("Register");
        }


        //TODO: 
        // Nepotreban endpoint, slicno kao za Register()
        public ActionResult Login()
        {
            UserLoginModel user = new UserLoginModel();
            return View(user);
        }

        //TODO:  
        // 1. Login se radi sa Claims i ClaimsPrincipal tokenom tj. autorizovani user je u memoriji, tako da je ovo skroz nevalidno,
        //    ali kad bi bilo validno opet bi trebalo izvjeci autorizaciju u kontroleru, vec je odraditi u middleware-u 
        // 2. Slicna prica kao za Register(UserModel user) metodu za validaciju 
        // 3. Koriscenje sesije za cuvanje bilo kakvih info je big no-no, ima vise negativnih strana - opterecuje se server,
        //    cuva se state sto nije pozeljno kad se rade RestAPI + mikroservisi, nije sigurno ni po pitanju security-ja,
        //    a ono sto je glavno - nije vise praksa, jer postoje druge i sigurnije metode, pa se preslo na razvijanje i odrzavanje tih tool-ova i paketa
        // 4. Ovoj liniji nije mjestu o kontroleru, vec u validatoru: ModelState.AddModelError("Error", "Invalid Username/Password.");
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