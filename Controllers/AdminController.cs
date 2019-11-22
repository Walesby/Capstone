using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IdentityContext _context;
        public AdminController(IdentityContext context)
        {
            _context = context;
        }
        public IActionResult CreateItem()
        {
            return View();
        }
        public IActionResult UpdateItem()
        {
            return View();
        }
    }
}