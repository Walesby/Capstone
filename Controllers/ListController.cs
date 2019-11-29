using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Capstone.ViewModels;
using Microsoft.AspNetCore.Identity;
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
                var userScores = new List<AnimeList>();
                foreach (var anime in animeList)
                {
                    if (anime.UserId == userId)
                    {
                        var item = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == anime.AnimeItemId);
                        userAnimeList.Add(item);
                        userScores.Add(anime);
                    }
                }
                var viewModel = new UserAnimeListViewModel
                {
                    User = user,
                    AnimeInfoList = userAnimeList,
                    UserAnimeList = userScores
                };
                return View(viewModel);
            }
        }

        public async Task<IActionResult> MangaList(string username)
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
                var mangaList = await _context.MangaList.ToListAsync<MangaList>();
                var userMangaList = new List<MangaItem>();
                var userScores = new List<MangaList>();
                foreach (var manga in mangaList)
                {
                    if (manga.UserId == userId)
                    {
                        var item = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == manga.MangaItemId);
                        userMangaList.Add(item);
                        userScores.Add(manga);
                    }
                }
                var viewModel = new UserMangaListViewModel
                {
                    User = user,
                    MangaInfoList = userMangaList,
                    UserMangaList = userScores
                };
                return View(viewModel);
            }
        }

        public async Task<IActionResult> NovelList(string username)
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
                var novelList = await _context.NovelList.ToListAsync<NovelList>();
                var userNovelList = new List<NovelItem>();
                var userScores = new List<NovelList>();
                foreach (var novel in novelList)
                {
                    if (novel.UserId == userId)
                    {
                        var item = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == novel.NovelItemId);
                        userNovelList.Add(item);
                        userScores.Add(novel);
                    }
                }
                var viewModel = new UserNovelListViewModel
                {
                    User = user,
                    NovelInfoList = userNovelList,
                    UserNovelList = userScores
                };
                return View(viewModel);
            }
        }
    }
}