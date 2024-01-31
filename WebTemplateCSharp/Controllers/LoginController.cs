using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTemplateCSharp.Entities;

namespace WebTemplateCSharp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserAccountsDbContext _db;
        public const string UsernameSessionKey = "_username";
        public const string NameSessionKey = "_name";
        public const string SectionSessionKey = "_section";
        public const string RoleSessionKey = "_role";

        public LoginController(UserAccountsDbContext db)
        {
            _db = db;
        }

        private async Task<UserAccounts> GetSignInCred(string username, string password)
        {
            return await _db.UserAccounts.FirstOrDefaultAsync(ua => ua.Username == username && ua.Password == password);
        }
        [RedirectFilterAdminUser]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(IFormCollection fc)
        {
            var user_accounts = GetSignInCred(fc["username"], fc["password"]);
            if (user_accounts.Result != null)
            {
                HttpContext.Session.SetString(UsernameSessionKey, user_accounts.Result.Username);
                HttpContext.Session.SetString(NameSessionKey, user_accounts.Result.FullName);
                HttpContext.Session.SetString(RoleSessionKey, user_accounts.Result.Role);
                var role = user_accounts.Result.Role;

                if (role == "admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (role == "user")
                {
                    return RedirectToAction("Pagination", "User");
                }
                else
                {
                    HttpContext.Session.Clear();
                    return RedirectToAction("SignInFailed", "Login", new { sign_in_failed = 1 });
                }
            }
            else
            {
                return RedirectToAction("SignInFailed", "Login", new { sign_in_failed = 1 });
            }
        }

        [RedirectFilterAdminUser]
        [HttpGet("SignInFailed/{sign_in_failed:int?}")]
        public IActionResult SignInFailed(int sign_in_failed)
        {
            ViewData["SignInFailed"] = sign_in_failed;
            return View("Index");
        }

        [HttpGet]
        public IActionResult SigningOut()
        {
            //HttpContext.Session.Remove(NameSessionKey);
            //HttpContext.Session.Remove(UsernameSessionKey);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
