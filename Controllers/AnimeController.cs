using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Capstone.ViewModels.ReviewsViewModel;
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
                var name = User.FindFirstValue(ClaimTypes.Name);
                if (name == null)
                {
                    var reviewList = await _context.AnimeReviews.Where(a => a.AnimeItemId == anime.Id).OrderByDescending(a => a.Id).Take(5).ToListAsync<AnimeReviews>();

                    var users = new List<User>();
                    foreach (var review in reviewList)
                    {
                        var username = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                        users.Add(username);
                    }
                    var viewModel = new AnimeItemViewModel
                    {
                        Anime = anime,
                        UserList = users,
                        ReviewsList = reviewList
                    };
                    return View(viewModel);
                }
                else
                {
                    var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
                    var animeList = await _context.AnimeList.Where(a => a.UserId == user.Id).ToListAsync();
                    bool onList = false;
                    foreach (var animeListItem in animeList)
                    {
                        if (animeListItem.AnimeItemId == anime.Id)
                        {
                            onList = true;
                        }
                    }

                    var reviewList = await _context.AnimeReviews.Where(a => a.AnimeItemId == anime.Id).OrderByDescending(a => a.Id).Take(5).ToListAsync<AnimeReviews>();

                    var users = new List<User>();
                    foreach (var review in reviewList)
                    {
                        var username = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                        users.Add(username);
                    }

                    var userFavorite = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == user.Id);
                    bool isUserFavorite = false;
                    if (userFavorite == null)
                    {
                        isUserFavorite = false;
                    }
                    else if (userFavorite.AnimeItemId == anime.Id)
                    {
                        isUserFavorite = true;
                    }

                    var viewModel = new AnimeItemViewModel
                    {
                        Anime = anime,
                        UserList = users,
                        ReviewsList = reviewList,
                        UserListContains = onList,
                        UserFavoriteContains = isUserFavorite
                    };
                    return View(viewModel);
                }
            }
        }
        public IActionResult Search()
        {
            return View();
        }
        public async Task<IActionResult> SearchResults(string animeTitle, string startingLetter)
        {
            if (animeTitle == null && startingLetter == null)
            {
                var emptyAnimeList = new List<AnimeItem>();
                return View(emptyAnimeList);
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

            else
            {
                var emptyAnimeList = new List<AnimeItem>();
                return View(emptyAnimeList);
            }

        }
        public async Task<IActionResult> Reviews()
        {
            var reviewList = await _context.AnimeReviews.OrderByDescending(a => a.Id).Take(25).ToListAsync<AnimeReviews>();
            var users = new List<User>();
            var animes = new List<AnimeItem>();
            foreach (var review in reviewList)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                var anime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == review.AnimeItemId);
                users.Add(user);
                animes.Add(anime);
            }
            var viewModel = new AnimeReviewsViewModel
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
                var topAnimeNoUserList = await _context.AnimeItem.OrderByDescending(a => a.Rating).ToListAsync<AnimeItem>();
                noUserViewModel.AnimeInfoList = topAnimeNoUserList;
                return View(noUserViewModel);
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var userId = user.Id;
            var animeList = await _context.AnimeItem.OrderByDescending(a => a.Rating).ToListAsync<AnimeItem>();
            var userStats = new List<AnimeList>();
            foreach (var anime in animeList)
            {
                var userScore = await _context.AnimeList.FirstOrDefaultAsync(a => a.AnimeItemId == anime.Id && a.UserId == userId);
                if (userScore == null)
                {
                    var notOnList = new AnimeList
                    {
                        UserRating = 0,
                        UserProgress = 0,
                        CompleteStatus = 0
                    };
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