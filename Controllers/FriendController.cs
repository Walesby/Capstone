using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Capstone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IdentityContext _context;
        public FriendController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> FriendList()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            if (name == null)
            {
                return View();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            if (user == null)
            {
                return View();
            }
            var userId = user.Id;

            var friendList = await _context.Friends.Where(a => a.UserId == userId).ToListAsync<FriendList>();
            var friendUserList = new List<User>();
            var friendAnimeUpdates = new List<AnimeItem>();
            var friendMangaUpdates = new List<MangaItem>();
            var friendNovelUpdates = new List<NovelItem>();
            foreach (var row in friendList)
            {
                var friend = await _context.User.FirstOrDefaultAsync(a => a.Id == row.FriendId);
                if (friend != null)
                {
                    friendUserList.Add(friend);
                }

                var animeList = await _context.AnimeList.OrderByDescending(a => a.Id).FirstOrDefaultAsync(a => a.UserId == row.FriendId);
                if (animeList != null)
                {
                    var animeItem = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == animeList.AnimeItemId);
                    if (animeItem != null)
                    {
                        friendAnimeUpdates.Add(animeItem);
                    }
                    else
                    {
                        var blankAnime = new AnimeItem();
                        friendAnimeUpdates.Add(blankAnime);
                    }
                }
                else
                {
                    var blankAnime = new AnimeItem();
                    friendAnimeUpdates.Add(blankAnime);
                }

                var mangaList = await _context.MangaList.OrderByDescending(a => a.Id).FirstOrDefaultAsync(a => a.UserId == row.FriendId);
                if (mangaList != null)
                {
                    var mangaItem = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == mangaList.MangaItemId);
                    if (mangaItem != null)
                    {
                        friendMangaUpdates.Add(mangaItem);
                    }
                    else
                    {
                        var blankManga = new MangaItem();
                        friendMangaUpdates.Add(blankManga);
                    }
                }
                else
                {
                    var blankManga = new MangaItem();
                    friendMangaUpdates.Add(blankManga);
                }

                var novelList = await _context.NovelList.OrderByDescending(a => a.Id).FirstOrDefaultAsync(a => a.UserId == row.FriendId);
                if (novelList != null)
                {
                    var novelItem = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == novelList.NovelItemId);
                    if (novelItem != null)
                    {
                        friendNovelUpdates.Add(novelItem);
                    }
                    else
                    {
                        var blankNovel = new NovelItem();
                        friendNovelUpdates.Add(blankNovel);
                    }
                }
                else
                {
                    var blankNovel = new NovelItem();
                    friendNovelUpdates.Add(blankNovel);
                }
            }
            var friendListViewModel = new FriendListViewModel
            {
                UserList = friendUserList,
                LastAnimeUpdateList = friendAnimeUpdates,
                LastMangaUpdateList = friendMangaUpdates,
                LastNovelUpdateList = friendNovelUpdates
            };
            return View(friendListViewModel);
        }
    }
}