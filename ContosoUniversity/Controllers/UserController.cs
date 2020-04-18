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
            if (ModelState.IsValid)//test
            {
                var info = _context.User.FirstOrDefault(o => o.UserName == user.UserName && o.Password == user.Password && o.Status == 1);
                if (info!=null)
                {
                    HttpContext.Session.SetString("user", info.UserName);
                    return RedirectToAction("Index", "Students");
                }
                ModelState.AddModelError("", "用户名或密码错误。");
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

    }
}