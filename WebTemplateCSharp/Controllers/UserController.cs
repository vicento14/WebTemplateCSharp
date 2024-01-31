using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplateCSharp.Controllers
{
    [RedirectFilterLoginUser]
    public class UserController : Controller
    {
        public IActionResult Pagination()
        {
            return View();
        }

        public IActionResult LoadMore()
        {
            return View();
        }

        public IActionResult TableSwitching()
        {
            return View();
        }

        public IActionResult KeyupSearch()
        {
            return View();
        }
    }
}
