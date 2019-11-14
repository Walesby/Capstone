using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IdentityContext _context;
        public AnimeController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Item(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == id);
            if(anime == null)
            {
                return NotFound();
            }
            else
            {
                return View(anime);
            }
        }
        public async Task<IActionResult> Search(string animeTitle)
        {
            if (animeTitle == null)
            {
                return View();
            }
            var anime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Title == animeTitle);
            if (anime == null)
            {
                return NotFound();
            }
            else
            {
                return View(anime);
            }
        }
        public IActionResult Reviews()
        {
            return View();
        }
        public async Task<IActionResult> Top()
        {
            var animeList = await _context.AnimeItem.ToListAsync<AnimeItem>();
            return View(animeList);
        }
    }
}