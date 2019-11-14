using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IdentityContext _context;
        public ProfileController(IdentityContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public new async Task<IActionResult> User(string username)
        {
            if (username == null)
            {
                return NotFound();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return View(user);
            }
        }
    }
}