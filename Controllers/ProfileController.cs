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
    public class ProfileController : Controller
    {
        private readonly IdentityContext _context;
        public ProfileController(IdentityContext context)
        {
            _context = context;
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
        public IActionResult Search()
        {
            return View();
        }
        public async Task<IActionResult> SearchResults(string username, string startingLetter)
        {
            if (username == null && startingLetter == null)
            {
                var list = new List<User>();
                return View(list);
            }

            if (username != null)
            {
                username = username.ToLower();
                var userList = await _context.User.ToListAsync();
                var searchResultUserList = new List<User>();
                foreach (var user in userList)
                {
                    if (user.UserName.ToLower().Contains(username))
                    {
                        searchResultUserList.Add(user);
                    }
                }
                return View(searchResultUserList);
            }
            else if (startingLetter != null)
            {
                var userList = await _context.User.ToListAsync();
                var searchResultUserList = new List<User>();
                foreach (var user in userList)
                {
                    if (user.UserName.StartsWith(startingLetter))
                    {
                        searchResultUserList.Add(user);
                    }
                }
                return View(searchResultUserList);
            }
            else
            {
                var list = new List<User>();
                return View(list);
            }

        }
        public async Task<IActionResult> AnimeReviews(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var reviewList = await _context.Reviews.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).ToListAsync<Reviews>();
            var users = new List<User>();
            var animes = new List<AnimeItem>();
            foreach (var review in reviewList)
            {
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
    }
}