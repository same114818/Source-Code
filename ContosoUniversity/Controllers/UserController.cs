using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity.Controllers
{
    public class UserController : Controller
    {
        private readonly SchoolContext _context;

        public UserController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
         
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var info = _context.User.FirstOrDefault(o => o.UserName == user.UserName && o.Password == user.Password && o.Status == 1);
                if (info!=null)
                {
                    HttpContext.Session.SetString("user", info.UserName);
                    return RedirectToAction("Index", "Students");
                }
                ModelState.AddModelError("", "User or Password Error!");
                return View();
            }
            return View();
        }

        //GET: User/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost,ActionName("Create")]
        public async Task<IActionResult> Create ( User user)
        {
            if (ModelState.IsValid)
            {
                var newUser = _context.User.FirstOrDefault(o => o.UserName == user.UserName);
                if (newUser!=null)
                {
                    ModelState.AddModelError("", "User exists!");
                    return View("Register");
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("Register", "User");
        }
    }
}