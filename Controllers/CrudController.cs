using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : Controller
    {
        private readonly IdentityContext _context;
        public CrudController(IdentityContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("postanime")]
        public async Task<IActionResult> PostAnime(AnimeItem animeItem)
        {
            animeItem.Aired = DateTime.Now;
            
            _context.AnimeItem.Add(animeItem);
            await _context.SaveChangesAsync();
            return Ok(animeItem);
        }

        [HttpPost]
        [Route("postreview")]
        public async Task<IActionResult> PostReview(Reviews review)
        {
            var username = "crownd";
            review.PostDate = DateTime.Now;
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var userId = user.Id;
            review.UserId = userId;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok(review);
        }

        [HttpPost]
        [Route("postuseranimelist")]
        public async Task<IActionResult> PostUserAnimeList(AnimeList animeList)
        {
            _context.AnimeList.Add(animeList);
            await _context.SaveChangesAsync();
            return Ok(animeList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnime(int id, AnimeItem animeItem)
        {
            if (id != animeItem.Id)
            {
                return BadRequest();
            }
            _context.Entry(animeItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(animeItem);
        }
    }
}