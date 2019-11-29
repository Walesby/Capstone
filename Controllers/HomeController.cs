using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;
using Capstone.Context;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdentityContext _context;
        public HomeController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var animeReviewList = await _context.AnimeReviews.ToListAsync<AnimeReviews>();
            var mangaReviewList = await _context.MangaReviews.ToListAsync<MangaReviews>();
            var novelReviewList = await _context.NovelReviews.ToListAsync<NovelReviews>();

            var animeList = await _context.AnimeItem.OrderByDescending(a => a.Rating).Take(5).ToListAsync<AnimeItem>();
            var mangaList = await _context.MangaItem.OrderByDescending(a => a.Rating).Take(5).ToListAsync<MangaItem>();
            var novelList = await _context.NovelItem.OrderByDescending(a => a.Rating).Take(5).ToListAsync<NovelItem>();

            var newestAnime = await _context.AnimeItem.OrderByDescending(a => a.Id).Take(15).ToListAsync<AnimeItem>();
            var newestManga = await _context.MangaItem.OrderByDescending(a => a.Id).Take(15).ToListAsync<MangaItem>();
            var newestNovel = await _context.NovelItem.OrderByDescending(a => a.Id).Take(15).ToListAsync<NovelItem>();

            var homeViewModel = new HomePageViewModel
            {
                NewestReviewsAnime = animeReviewList,
                NewestAnime = newestAnime,
                HighestRatedAnime = animeList,
                NewestReviewsManga = mangaReviewList,
                NewestManga = newestManga,
                HighestRatedManga = mangaList,
                NewestReviewsNovel = novelReviewList,
                NewestNovel = newestNovel,
                HighestRatedNovel = novelList
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
