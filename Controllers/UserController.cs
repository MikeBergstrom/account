using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bank.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace bank.Controllers
{
    public class UserController : Controller
    {
        private UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel user){
            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User newUser = new User {FirstName = user.FirstName, LastName= user.LastName, Email = user.Email, };
                newUser.Password = Hasher.HashPassword(newUser, user.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                User logUser = _context.Users.SingleOrDefault(u => u.Email == user.Email);
                HttpContext.Session.SetInt32("UserId", logUser.UserId);
                return RedirectToAction("Account", "Account");
            } else {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult GetLogin()
        {
            return View("Login");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string PasswordToCheck)
        {
            System.Console.WriteLine(PasswordToCheck + "*********************************************************************");
            System.Console.WriteLine(Email);
            User user = _context.Users.SingleOrDefault(u => u.Email == Email);
            System.Console.WriteLine(user.Password);
            if(user != null && PasswordToCheck != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, PasswordToCheck)){
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Account", "Account");
                }
            }
            ViewBag.error = "Email address of password Incorrect";
            return View("Login");
        }
    }
}
