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
    public class ListController : Controller
    {
        private readonly IdentityContext _context;
        public ListController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AnimeList(string username)
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
                var userId = user.Id;
                var animeList = await _context.AnimeList.ToListAsync<AnimeList>();
                var userAnimeList = new List<AnimeItem>();
                foreach (var anime in animeList)
                {
                    if (anime.UserId == userId)
                    {
                        var item = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == anime.AnimeItemId);
                        userAnimeList.Add(item);
                    }
                }
                
                return View(userAnimeList);
            }
        }
    }
}