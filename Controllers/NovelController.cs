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
    public class NovelController : Controller
    {
        private readonly IdentityContext _context;
        public NovelController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Item(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var novel = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == id);
            if (novel == null)
            {
                return NotFound();
            }
            else
            {
                var name = User.FindFirstValue(ClaimTypes.Name);
                if (name == null)
                {
                    var reviewList = await _context.NovelReviews.Where(a => a.NovelItemId == novel.Id).OrderByDescending(a => a.Id).Take(5).ToListAsync<NovelReviews>();

                    var users = new List<User>();
                    foreach (var review in reviewList)
                    {
                        var username = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                        users.Add(username);
                    }
                    var viewModel = new NovelItemViewModel
                    {
                        Novel = novel,
                        UserList = users,
                        ReviewsList = reviewList
                    };
                    return View(viewModel);
                }
                else
                {
                    var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
                    var novelList = await _context.NovelList.Where(a => a.UserId == user.Id).ToListAsync();

                    bool onList = false;
                    foreach (var animeListItem in novelList)
                    {
                        if (animeListItem.NovelItemId == novel.Id)
                        {
                            onList = true;
                        }
                    }

                    var reviewList = await _context.NovelReviews.Where(a => a.NovelItemId == novel.Id).OrderByDescending(a => a.Id).Take(5).ToListAsync<NovelReviews>();
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
                    else if (userFavorite.NovelItemId == novel.Id)
                    {
                        isUserFavorite = true;
                    }

                    var viewModel = new NovelItemViewModel
                    {
                        Novel = novel,
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

        public async Task<IActionResult> SearchResults(string novelTitle, string startingLetter)
        {
            if (novelTitle == null && startingLetter == null)
            {
                var emptyNovelList = new List<NovelItem>();
                return View(emptyNovelList);
            }

            if (novelTitle != null)
            {
                novelTitle = novelTitle.ToLower();
                var novelList = await _context.NovelItem.ToListAsync<NovelItem>();
                var searchResultNovelList = new List<NovelItem>();
                foreach (var novel in novelList)
                {
                    if (novel.Title.ToLower().Contains(novelTitle))
                    {
                        searchResultNovelList.Add(novel);
                    }
                }
                return View(searchResultNovelList);
            }

            else if (startingLetter != null)
            {
                var novelList = await _context.NovelItem.ToListAsync<NovelItem>();
                var searchResultNovelList = new List<NovelItem>();
                foreach (var novel in novelList)
                {
                    if (novel.Title.StartsWith(startingLetter))
                    {
                        searchResultNovelList.Add(novel);
                    }
                }
                return View(searchResultNovelList);
            }

            else
            {
                var emptyNovelList = new List<NovelItem>();
                return View(emptyNovelList);
            }
        }

        public async Task<IActionResult> Reviews()
        {
            var reviewList = await _context.NovelReviews.OrderByDescending(a => a.Id).ToListAsync<NovelReviews>();
            var usersList = new List<User>();
            var novelsList = new List<NovelItem>();
            foreach (var review in reviewList)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                var novel = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == review.NovelItemId);
                usersList.Add(user);
                novelsList.Add(novel);
            }
            var viewModel = new NovelReviewsViewModel
            {
                UserList = usersList,
                NovelInfoList = novelsList,
                ReviewsList = reviewList
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Top()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null)
            {
                var noUserViewModel = new UserNovelListViewModel();
                var topNovelNoUserList = await _context.NovelItem.OrderByDescending(a => a.Rating).ToListAsync<NovelItem>();
                noUserViewModel.NovelInfoList = topNovelNoUserList;
                return View(noUserViewModel);
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var userId = user.Id;
            var novelList = await _context.NovelItem.OrderByDescending(a => a.Rating).ToListAsync<NovelItem>();
            var userStats = new List<NovelList>();
            foreach (var novel in novelList)
            {
                var userScore = await _context.NovelList.FirstOrDefaultAsync(a => a.NovelItemId == novel.Id && a.UserId == userId);
                if (userScore == null)
                {
                    var notOnList = new NovelList
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
            var viewModel = new UserNovelListViewModel
            {
                User = user,
                NovelInfoList = novelList,
                UserNovelList = userStats
            };
            return View(viewModel);
        }
    }
}