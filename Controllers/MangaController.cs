using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Capstone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
    public class MangaController : Controller
    {
        private readonly IdentityContext _context;
        public MangaController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Item(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var manga = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == id);
            if (manga == null)
            {
                return NotFound();
            }
            else
            {
                var name = User.FindFirstValue(ClaimTypes.Name);
                if (name == null)
                {
                    var reviewList = await _context.MangaReviews.Where(a => a.MangaItemId == manga.Id).OrderByDescending(a => a.Id).Take(5).ToListAsync<MangaReviews>();

                    var users = new List<User>();
                    foreach (var review in reviewList)
                    {
                        var username = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                        users.Add(username);
                    }
                    var viewModel = new MangaItemViewModel
                    {
                        Manga = manga,
                        UserList = users,
                        ReviewsList = reviewList
                    };
                    return View(viewModel);
                }
                else
                {
                    var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
                    var mangaList = await _context.MangaList.Where(a => a.UserId == user.Id).ToListAsync();

                    bool onList = false;
                    foreach (var mangaListItem in mangaList)
                    {
                        if (mangaListItem.MangaItemId == manga.Id)
                        {
                            onList = true;
                        }
                    }

                    var reviewList = await _context.MangaReviews.Where(a => a.MangaItemId == manga.Id).OrderByDescending(a => a.Id).Take(5).ToListAsync<MangaReviews>();
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
                    else if (userFavorite.MangaItemId == manga.Id)
                    {
                        isUserFavorite = true;
                    }

                    var viewModel = new MangaItemViewModel
                    {
                        Manga = manga,
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

        public async Task<IActionResult> SearchResults(string mangaTitle, string startingLetter)
        {
            if (mangaTitle == null && startingLetter == null)
            {
                var emptyNovelList = new List<MangaItem>();
                return View(emptyNovelList);
            }

            if (mangaTitle != null)
            {
                mangaTitle = mangaTitle.ToLower();
                var mangaList = await _context.MangaItem.ToListAsync<MangaItem>();
                var searchResultMangaList = new List<MangaItem>();
                foreach (var manga in mangaList)
                {
                    if (manga.Title.ToLower().Contains(mangaTitle))
                    {
                        searchResultMangaList.Add(manga);
                    }
                }
                return View(searchResultMangaList);
            }

            else if (startingLetter != null)
            {
                var mangaList = await _context.MangaItem.ToListAsync<MangaItem>();
                var searchResultMangaList = new List<MangaItem>();
                foreach (var manga in mangaList)
                {
                    if (manga.Title.StartsWith(startingLetter))
                    {
                        searchResultMangaList.Add(manga);
                    }
                }
                return View(searchResultMangaList);
            }

            else
            {
                var emptyMangaList = new List<MangaItem>();
                return View(emptyMangaList);
            }
        }

        public async Task<IActionResult> Reviews()
        {
            var reviewList = await _context.MangaReviews.OrderByDescending(a => a.Id).ToListAsync<MangaReviews>();
            var usersList = new List<User>();
            var mangasList = new List<MangaItem>();
            foreach (var review in reviewList)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                var manga = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == review.MangaItemId);
                usersList.Add(user);
                mangasList.Add(manga);
            }
            var viewModel = new MangaReviewsViewModel
            {
                UserList = usersList,
                MangaInfoList = mangasList,
                ReviewsList = reviewList
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Top()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null)
            {
                var noUserViewModel = new UserMangaListViewModel();
                var topNovelNoUserList = await _context.MangaItem.OrderByDescending(a => a.Rating).ToListAsync<MangaItem>();
                noUserViewModel.MangaInfoList = topNovelNoUserList;
                return View(noUserViewModel);
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var userId = user.Id;
            var mangaList = await _context.MangaItem.OrderByDescending(a => a.Rating).ToListAsync<MangaItem>();
            var userStats = new List<MangaList>();
            foreach (var manga in mangaList)
            {
                var userScore = await _context.MangaList.FirstOrDefaultAsync(a => a.MangaItemId == manga.Id && a.UserId == userId);
                if (userScore == null)
                {
                    var notOnList = new MangaList
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
            var viewModel = new UserMangaListViewModel
            {
                User = user,
                MangaInfoList = mangaList,
                UserMangaList = userStats
            };
            return View(viewModel);
        }
    }
}