using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplateCSharp.Controllers
{
    [RedirectFilterLoginAdmin]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Accounts()
        {
            return View();
        }
        public IActionResult Sample1()
        {
            return View();
        }

    }
}
