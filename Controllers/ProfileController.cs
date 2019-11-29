using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Context;
using Capstone.Models;
using Capstone.ViewModels;
using Capstone.ViewModels.ReviewsViewModel;
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
        public async Task<IActionResult> UserProfile(string username)
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
            var userId = user.Id;

            var animeList = await _context.AnimeList.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).ToListAsync();
            var mangaList = await _context.MangaList.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).ToListAsync();
            var novelList = await _context.NovelList.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).ToListAsync();

            var newestOnAnimeList = animeList.Take(5);
            var newestOnMangaList = mangaList.Take(5);
            var newestOnNovelList = novelList.Take(5);

            var latestAnimeUpdates = new List<AnimeItem>();

            foreach (var anime in newestOnAnimeList)
            {
                var newestAnime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == anime.AnimeItemId);
                if (newestAnime != null)
                {
                    latestAnimeUpdates.Add(newestAnime);
                }
            }

            var latestMangaUpdates = new List<MangaItem>();
            foreach (var manga in newestOnMangaList)
            {
                var newestManga = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == manga.MangaItemId);
                if (newestManga != null)
                {
                    latestMangaUpdates.Add(newestManga);
                }
            }

            var latestNovelUpdates = new List<NovelItem>();
            foreach (var novel in newestOnNovelList)
            {
                var newestNovel = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == novel.NovelItemId);
                if (newestNovel != null)
                {
                    latestNovelUpdates.Add(newestNovel);
                }
            }          

            var animeWatching = 0;
            var animeComplete = 0;

            var mangaReading = 0;
            var mangaComplete = 0;

            var novelReading = 0;
            var novelComplete = 0;

            var animeRatingSum = 0;
            foreach (var anime in animeList)
            {
                if (anime.CompleteStatus == (CompleteStatusAnime)1)
                {
                    animeComplete += 1;
                }
                else if (anime.CompleteStatus == (CompleteStatusAnime)2)
                {
                    animeWatching += 1;
                }
                animeRatingSum += anime.UserRating;
            }

            var mangaRatingSum = 0;
            foreach (var manga in mangaList)
            {
                if (manga.CompleteStatus == (CompleteStatusManga)1)
                {
                    mangaComplete += 1;
                }
                else if (manga.CompleteStatus == (CompleteStatusManga)2)
                {
                    mangaReading += 1;
                }
                mangaRatingSum += manga.UserRating;
            }

            var novelRatingSum = 0;
            foreach (var novel in novelList)
            {
                if (novel.CompleteStatus == (CompleteStatusNovel)1)
                {
                    novelComplete += 1;
                }
                else if (novel.CompleteStatus == (CompleteStatusNovel)2)
                {
                    novelReading += 1;
                }
                novelRatingSum += novel.UserRating;
            }
            var animeAverageRating = 0;
            var mangaAverageRating = 0;
            var novelAverageRating = 0;
            if (animeList.Count > 0)
            {
                animeAverageRating = animeRatingSum / animeList.Count;
            }
            if (mangaList.Count > 0)
            {
                mangaAverageRating = mangaRatingSum / mangaList.Count;
            }
            if (novelList.Count > 0)
            {
                novelAverageRating = novelRatingSum / novelList.Count;
            }

            var userIsFriend = false;
            var loggedInUsername = User.FindFirstValue(ClaimTypes.Name);
            if (loggedInUsername != null)
            {
                var loggedInUser = await _context.User.FirstOrDefaultAsync(a => a.UserName == loggedInUsername);
                var friend = await _context.Friends.FirstOrDefaultAsync(a => a.UserId == loggedInUser.Id && a.FriendId == userId);
                if (friend != null)
                {
                    userIsFriend = true;
                }
            }

            var userFavorites = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == userId);
            if (userFavorites == null)
            {
                var noFavoritesViewModel = new UserProfileViewModel
                {
                    User = user,
                    AnimeComplete = animeComplete,
                    AnimeWatching = animeWatching,
                    MangaComplete = mangaComplete,
                    MangaReading = mangaReading,
                    NovelComplete = novelComplete,
                    NovelReading = novelReading,
                    AnimeAverageScore = animeAverageRating,
                    MangaAverageScore = mangaAverageRating,
                    NovelAverageScore = novelAverageRating,
                    LatestAnimeUpdates = latestAnimeUpdates,
                    LatestMangaUpdates = latestMangaUpdates,
                    LatestNovelUpdates = latestNovelUpdates,
                    UserIsFriend = userIsFriend
                };
                return View(noFavoritesViewModel);
            }

            var favoriteAnime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == userFavorites.AnimeItemId);
            var favoriteManga = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == userFavorites.MangaItemId);
            var favoriteNovel = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == userFavorites.NovelItemId);

            var viewModel = new UserProfileViewModel
            {
                User = user,
                Favorite = userFavorites,
                FavoriteAnime = favoriteAnime,
                FavoriteManga = favoriteManga,
                FavoriteNovel = favoriteNovel,
                AnimeComplete = animeComplete,
                AnimeWatching = animeWatching,
                MangaComplete = mangaComplete,
                MangaReading = mangaReading,
                NovelComplete = novelComplete,
                NovelReading = novelReading,
                AnimeAverageScore = animeAverageRating,
                MangaAverageScore = mangaAverageRating,
                NovelAverageScore = novelAverageRating,
                LatestAnimeUpdates = latestAnimeUpdates,
                LatestMangaUpdates = latestMangaUpdates,
                LatestNovelUpdates = latestNovelUpdates,
                UserIsFriend = userIsFriend
            };
            return View(viewModel);
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
            var reviewList = await _context.AnimeReviews.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).ToListAsync<AnimeReviews>();
            var users = new List<User>();
            var animes = new List<AnimeItem>();
            if (reviewList.Count < 1)
            {
                users.Add(user);
            }
            else
            {
                foreach (var review in reviewList)
                {
                    var anime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == review.AnimeItemId);
                    users.Add(user);
                    animes.Add(anime);
                }
            }
            var viewModel = new AnimeReviewsViewModel
            {
                UserList = users,
                AnimeInfoList = animes,
                ReviewsList = reviewList

            };
            return View(viewModel);
        }

        public async Task<IActionResult> MangaReviews(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var reviewList = await _context.MangaReviews.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).ToListAsync<MangaReviews>();
            var users = new List<User>();
            var mangas = new List<MangaItem>();
            if (reviewList.Count < 1)
            {
                users.Add(user);
            }
            else
            {
                foreach (var review in reviewList)
                {
                    var anime = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == review.MangaItemId);
                    users.Add(user);
                    mangas.Add(anime);
                }
            }
            var viewModel = new MangaReviewsViewModel
            {
                UserList = users,
                MangaInfoList = mangas,
                ReviewsList = reviewList

            };
            return View(viewModel);
        }

        public async Task<IActionResult> NovelReviews(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            var reviewList = await _context.NovelReviews.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).ToListAsync<NovelReviews>();
            var users = new List<User>();
            var novels = new List<NovelItem>();
            if (reviewList.Count < 1)
            {
                users.Add(user);
            }
            else
            {
                foreach (var review in reviewList)
                {
                    var novel = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == review.NovelItemId);
                    users.Add(user);
                    novels.Add(novel);
                }
            }
            var viewModel = new NovelReviewsViewModel
            {
                UserList = users,
                NovelInfoList = novels,
                ReviewsList = reviewList

            };
            return View(viewModel);
        }
    }
}