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
                var records = _context.Records.Where(record => record.UserId == UId).OrderByDescending(a => a.CreatedAt).ToList();
                var balance = 0.00;
                foreach(var record in records){
                    balance += record.Amount;
                }
                ViewBag.user = _context.Users.SingleOrDefault(u => u.UserId == UId);
                ViewBag.records = records;
                ViewBag.balance = balance;
                ViewBag.error = TempData["error"];
                return View();
            }
            return RedirectToAction("Index", "User");
        }
        [HttpPost]
        [Route("record")]
        public IActionResult record(Record newRecord){
            int user_id = (int)HttpContext.Session.GetInt32("UserId");
            var records = _context.Records.Where(record => record.UserId == user_id).ToList();
            var balance = 0.00;
            foreach(var record in records){
                balance += record.Amount;
            }
            if((balance + newRecord.Amount) >0.00){
                System.Console.WriteLine(balance + "******************************************************************");
                newRecord.CreatedAt = DateTime.Now;
                newRecord.UpdatedAt = DateTime.Now;
                newRecord.UserId = user_id;
                _context.Add(newRecord);
                _context.SaveChanges();
            } else {
                System.Console.WriteLine("else *************************************************************************************************");
                TempData["error"] = "Withdrawal amount is higher than account balance";
            }
            return RedirectToAction("Account");
        }
    }
}