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
    public class AccountController : Controller
    {
        private UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Account")]
        public IActionResult Account(){
            int? UId = HttpContext.Session.GetInt32("UserId");
            if(UId != null){
                ViewBag.user = _context.Users.SingleOrDefault(u => u.UserId == UId);

                return View();
            }
            return RedirectToAction("User", "/");
        }
    }
}