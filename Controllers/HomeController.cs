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
            var reviewList = await _context.Reviews.ToListAsync<Reviews>();
            var animeList = await _context.AnimeItem.ToListAsync<AnimeItem>();
            var newestAnime = new List<AnimeItem>();
            var mostPopularAnime = new List<AnimeItem>();
            foreach (var anime in animeList)
            {
                if (anime.Popularity <= 5)
                {
                    mostPopularAnime.Add(anime);
                }
            }
            var homeViewModel = new HomePageViewModel();
            homeViewModel.NewestReviews = reviewList;
            homeViewModel.NewestAnime = animeList;
            homeViewModel.HighestRated = mostPopularAnime;
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
