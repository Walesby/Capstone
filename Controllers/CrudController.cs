using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class CrudController : Controller
    {
        private readonly IdentityContext _context;
        public CrudController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> PostAnime(AnimeItem animeItem)
        {
            _context.AnimeItem.Add(animeItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}