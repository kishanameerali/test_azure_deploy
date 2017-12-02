using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bank_accounts.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace bank_accounts.Controllers
{
    public class HomeController : Controller
    {
        private BankContext _context;

        public HomeController(BankContext context)
        {
            _context = context;
        }

        private bool UserExists(string email)
        {
            List<User> ReturnedUsers = _context.users.Where(user => user.email == email).ToList();
            return ReturnedUsers.Count > 0;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(UserExists(model.email))
            {
                ModelState.AddModelError("email", "That email has already been registered");
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User NewUser = new User();
                {
                    NewUser.first_name = model.first_name;
                    NewUser.last_name = model.last_name;
                    NewUser.email = model.email;
                    NewUser.password = Hasher.HashPassword(NewUser, model.password);
                    NewUser.balance = 0;
                    NewUser.created_at = DateTime.Now;
                    NewUser.updated_at = DateTime.Now;

                    // why do this in NewUser constructor?
                    _context.users.Add(NewUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("userid", NewUser.userid);
                    return Redirect($"account/{NewUser.userid}");
                };
            }
            return View("Index", model);
        }

        [HttpPost]
        [Route("loginuser")]
        public IActionResult Loginuser(LoginViewModel model)
        {
            User LoggedUser = _context.users.SingleOrDefault(l => l.email == model.email);
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            if(!UserExists(model.email) || Hasher.VerifyHashedPassword(LoggedUser, LoggedUser.password, model.password) == 0)
            {
                ModelState.AddModelError("email", "Email or Password was incorrect");
            }
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("userid", LoggedUser.userid);
                return Redirect($"account/{LoggedUser.userid}");
            }
            return View("login");
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
 
    }
}
    
