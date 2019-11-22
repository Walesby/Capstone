using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
    [Authorize]
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
            _context.AnimeItem.Add(animeItem);
            await _context.SaveChangesAsync();
            return Ok(animeItem);
        }

        [HttpPut("putanime")]
        public async Task<IActionResult> PutAnime(AnimeItem animeItem)
        {
            _context.Entry(animeItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(animeItem);
        }

        [HttpGet("getreviews/{id}")]
        public async Task<IActionResult> GetReviews(int id)
        {
            var reviews = await _context.Reviews.Where(a => a.AnimeItemId == id).OrderByDescending(a => a.Id).Take(5).ToListAsync();
            var usernameList = new List<string>();
            foreach (var review in reviews)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                review.User = null;
                usernameList.Add(user.UserName);
            }
            var userReviewViewModel = new UserReviewViewModel();
            userReviewViewModel.ReviewsList = reviews;
            userReviewViewModel.UsernameList = usernameList;
            return Ok(userReviewViewModel);
        }

        [HttpPost]
        [Route("postreview")]
        public async Task<IActionResult> PostReview(Reviews review)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            review.PostDate = DateTime.Now;
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            review.UserId = userId;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok("Posted Review");
        }

        [HttpPost]
        [Route("postuseranimelist")]
        public async Task<IActionResult> PostUserAnimeList(AnimeList animeList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            animeList.UserId = user.Id;
            var currentAnimeList = await _context.AnimeList.Where(a => a.UserId == user.Id).ToListAsync<AnimeList>();
            foreach (var listItem in currentAnimeList)
            {
                if (listItem.AnimeItemId == animeList.AnimeItemId)
                {
                    return BadRequest("Item already on list");
                }
            }
            _context.AnimeList.Add(animeList);
            await _context.SaveChangesAsync();
            return Ok(animeList);
        }

        [HttpPut]
        [Route("putuseranimelist")]
        public async Task<IActionResult> PutUserAnimeList(AnimeList animeList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            animeList.UserId = user.Id;
            var animeListItem = await _context.AnimeList.FirstOrDefaultAsync(a => a.AnimeItemId == animeList.AnimeItemId && a.UserId == user.Id);
            animeListItem.UserRating = animeList.UserRating;
            animeListItem.UserProgress = animeList.UserProgress;
            _context.Entry(animeListItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(animeList);
        }

        [HttpDelete]
        [Route("deleteuseranimelist/{animeId}")]
        public async Task<IActionResult> DeleteUserAnimeList(int animeId)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var animeListItem = await _context.AnimeList.FirstOrDefaultAsync(a => a.AnimeItemId == animeId && a.UserId == user.Id);
            _context.AnimeList.Remove(animeListItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}