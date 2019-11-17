using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Microsoft.AspNetCore.Identity;
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
                var reviewList = await _context.Reviews.ToListAsync<Reviews>();
                var users = new List<User>();
                var reviewsOfAnime = new List<Reviews>();
                foreach(var review in reviewList)
                {
                    if (review.AnimeItemId == anime.Id)
                    {
                        var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                        users.Add(user);
                        reviewsOfAnime.Add(review);
                    }
                }
                var viewModel = new AnimeItemViewModel
                {
                    Anime = anime,
                    UserList = users,
                    ReviewsList = reviewsOfAnime
                };
                return View(viewModel);
            }
        }
        public IActionResult Search()
        {
            return View();
        }
        public async Task<IActionResult> SearchResults(string animeTitle, string startingLetter, string genre)
        {
            if (animeTitle == null && startingLetter == null && genre == null)
            {
                return View();
            }

            if (animeTitle != null)
            {
                animeTitle = animeTitle.ToLower();
                var animeList = await _context.AnimeItem.ToListAsync<AnimeItem>();
                var searchResultAnimeList = new List<AnimeItem>();
                foreach (var anime in animeList)
                {
                    if (anime.Title.ToLower().Contains(animeTitle))
                    {
                        searchResultAnimeList.Add(anime);
                    }
                }
                return View(searchResultAnimeList);
            }

            else if (startingLetter != null)
            {
                var animeList = await _context.AnimeItem.ToListAsync<AnimeItem>();
                var searchResultAnimeList = new List<AnimeItem>();
                foreach (var anime in animeList)
                {
                    if (anime.Title.StartsWith(startingLetter))
                    {
                        searchResultAnimeList.Add(anime);
                    }
                }
                return View(searchResultAnimeList);
            }

            else if (genre != null)
            {
                return View();
            }

            else
            {
                return View();
            }

        }
        public async Task<IActionResult> Reviews()
        {
            var reviewList = await _context.Reviews.ToListAsync<Reviews>();
            var users = new List<User>();
            var animes = new List<AnimeItem>();
            foreach (var review in reviewList)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                var anime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == review.AnimeItemId);
                users.Add(user);
                animes.Add(anime);
            }
            var viewModel = new ReviewsViewModel
            {
                UserList = users,
                AnimeInfoList = animes,
                ReviewsList = reviewList

            };
            return View(viewModel);
        }

        public async Task<IActionResult> Top()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null)
            {
                var noUserViewModel = new UserAnimeListViewModel();
                var topAnimeNoUserList = await _context.AnimeItem.ToListAsync<AnimeItem>();
                noUserViewModel.AnimeInfoList = topAnimeNoUserList;
                return View(noUserViewModel);
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var userId = user.Id;
            var animeList = await _context.AnimeItem.ToListAsync<AnimeItem>();
            var userStats = new List<AnimeList>();
            foreach (var anime in animeList)
            {
                var userScore = await _context.AnimeList.FirstOrDefaultAsync(a => a.AnimeItemId == anime.Id && a.UserId == userId);
                if (userScore == null)
                {
                    var notOnList = new AnimeList();
                    notOnList.UserRating = 0;
                    notOnList.UserProgress = 0;
                    notOnList.CompleteStatus = 0;
                    userStats.Add(notOnList);
                }
                else
                {
                    userStats.Add(userScore);
                }
            }
            var viewModel = new UserAnimeListViewModel
            {
                User = user,
                AnimeInfoList = animeList,
                UserAnimeList = userStats
            };
            return View(viewModel);
        }
    }
}