using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class MangaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Reviews()
        {
            return View();
        }
        public IActionResult Top()
        {
            return View();
        }
    }
}