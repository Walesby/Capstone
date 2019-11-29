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
    [Route("api/crud")]
    [ApiController]
    public class CrudController : Controller
    {
        private readonly IdentityContext _context;
        public CrudController(IdentityContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("postanime")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAnime(AnimeItem animeItem)
        {
            _context.AnimeItem.Add(animeItem);
            await _context.SaveChangesAsync();
            return Ok(animeItem);
        }

        [HttpPost]
        [Route("postmanga")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostManga(MangaItem mangaItem)
        {
            _context.MangaItem.Add(mangaItem);
            await _context.SaveChangesAsync();
            return Ok(mangaItem);
        }

        [HttpPost]
        [Route("postnovel")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostNovel(NovelItem novelItem)
        {
            _context.NovelItem.Add(novelItem);
            await _context.SaveChangesAsync();
            return Ok(novelItem);
        }

        [HttpPut("putanime")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAnime(AnimeItem animeItem)
        {
            _context.Entry(animeItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(animeItem);
        }

        [HttpPut("putmanga")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutManga(MangaItem mangaItem)
        {
            _context.Entry(mangaItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(mangaItem);
        }

        [HttpPut("putnovel")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutNovel(NovelItem novelItem)
        {
            _context.Entry(novelItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(novelItem);
        }

        [HttpGet("getanimereviews/{id}")]
        public async Task<IActionResult> GetAnimeReviews(int id)
        {
            var reviews = await _context.AnimeReviews.Where(a => a.AnimeItemId == id).OrderByDescending(a => a.Id).Take(5).ToListAsync();
            var usernameList = new List<string>();
            var imageUrlList = new List<string>();
            foreach (var review in reviews)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                review.User = null;
                usernameList.Add(user.UserName);
                imageUrlList.Add(user.ProfileImagePath);
            }
            var userReviewViewModel = new UserReviewViewModel
            {
                ReviewsList = reviews,
                UsernameList = usernameList,
                UserImageUrl = imageUrlList
            };
            return Ok(userReviewViewModel);
        }

        [HttpGet("getmangareviews/{id}")]
        public async Task<IActionResult> GetMangaReviews(int id)
        {
            var reviews = await _context.MangaReviews.Where(a => a.MangaItemId == id).OrderByDescending(a => a.Id).Take(5).ToListAsync();
            var usernameList = new List<string>();
            var imageUrlList = new List<string>();
            foreach (var review in reviews)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                review.User = null;
                usernameList.Add(user.UserName);
                imageUrlList.Add(user.ProfileImagePath);
            }
            var userReviewViewModel = new UserMangaReviewViewModel
            {
                ReviewsList = reviews,
                UsernameList = usernameList,
                UserImageUrl = imageUrlList
            };
            return Ok(userReviewViewModel);
        }

        [HttpGet("getnovelreviews/{id}")]
        public async Task<IActionResult> GetNovelReviews(int id)
        {
            var reviews = await _context.NovelReviews.Where(a => a.NovelItemId == id).OrderByDescending(a => a.Id).Take(5).ToListAsync();
            var usernameList = new List<string>();
            var imageUrlList = new List<string>();
            foreach (var review in reviews)
            {
                var user = await _context.User.FirstOrDefaultAsync(a => a.Id == review.UserId);
                review.User = null;
                usernameList.Add(user.UserName);
                imageUrlList.Add(user.ProfileImagePath);
            }
            var userReviewViewModel = new UserNovelReviewViewModel
            {
                ReviewsList = reviews,
                UsernameList = usernameList,
                UserImageUrl = imageUrlList
            };
            return Ok(userReviewViewModel);
        }

        [HttpPost]
        [Route("postanimereview")]
        public async Task<IActionResult> PostAnimeReview(AnimeReviews review)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            review.PostDate = DateTime.Now;
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            review.UserId = userId;
            _context.AnimeReviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok("Posted Review");
        }

        [HttpPost]
        [Route("postmangareview")]
        public async Task<IActionResult> PostMangaReview(MangaReviews review)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            review.PostDate = DateTime.Now;
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            review.UserId = userId;
            _context.MangaReviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok("Posted Review");
        }

        [HttpPost]
        [Route("postnovelreview")]
        public async Task<IActionResult> PostNovelReview(NovelReviews review)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            review.PostDate = DateTime.Now;
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            review.UserId = userId;
            _context.NovelReviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok("Posted Review");
        }

        [HttpPost]
        [Route("postuseranimelist")]
        public async Task<IActionResult> PostUserAnimeList(AnimeList animeList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            animeList.UserId = user.Id;
            var currentAnimeList = await _context.AnimeList.Where(a => a.UserId == user.Id).ToListAsync<AnimeList>();
            foreach (var listItem in currentAnimeList)
            {
                if (listItem.AnimeItemId == animeList.AnimeItemId)
                {
                    return BadRequest("Item already on list");
                }
            }
            _context.AnimeList.Add(animeList);
            var updateAnime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == animeList.AnimeItemId);
            updateAnime.Members += 1;
            _context.Entry(updateAnime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Added to list");
        }

        [HttpPost]
        [Route("postusermangalist")]
        public async Task<IActionResult> PostUserMangaList(MangaList mangaList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            mangaList.UserId = user.Id;
            var currentMangaList = await _context.MangaList.Where(a => a.UserId == user.Id).ToListAsync<MangaList>();
            foreach (var listItem in currentMangaList)
            {
                if (listItem.MangaItemId == mangaList.MangaItemId)
                {
                    return BadRequest("Item already on list");
                }
            }
            _context.MangaList.Add(mangaList);
            var updateManga = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == mangaList.MangaItemId);
            updateManga.Members += 1;
            _context.Entry(updateManga).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Added to list");
        }

        [HttpPost]
        [Route("postusernovellist")]
        public async Task<IActionResult> PostUserNovelList(NovelList novelList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            novelList.UserId = user.Id;
            var currentNovelList = await _context.NovelList.Where(a => a.UserId == user.Id).ToListAsync<NovelList>();
            foreach (var listItem in currentNovelList)
            {
                if (listItem.NovelItemId == novelList.NovelItemId)
                {
                    return BadRequest("Item already on list");
                }
            }
            _context.NovelList.Add(novelList);
            var updateNovel = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == novelList.NovelItemId);
            updateNovel.Members += 1;
            _context.Entry(updateNovel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Added to list");
        }

        [HttpPut]
        [Route("putuseranimelist")]
        public async Task<IActionResult> PutUserAnimeList(AnimeList animeList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            animeList.UserId = user.Id;
            var animeListItem = await _context.AnimeList.FirstOrDefaultAsync(a => a.AnimeItemId == animeList.AnimeItemId && a.UserId == user.Id);
            var animeItem = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == animeListItem.AnimeItemId);
            if (animeList.UserRating >= 1 && animeList.UserRating <= 10 && animeList.UserProgress >= 0 && animeList.UserProgress <= animeItem.EpisodeCount)
            {
                animeListItem.UserRating = animeList.UserRating;
                animeListItem.UserProgress = animeList.UserProgress;
                if (animeListItem.UserProgress == animeItem.EpisodeCount)
                {
                    // Set to complete
                    animeListItem.CompleteStatus = (CompleteStatusAnime)1;
                }
                else if (animeListItem.UserProgress > 0)
                {
                    // Set to watching
                    animeListItem.CompleteStatus = (CompleteStatusAnime)2;
                }
                _context.Entry(animeListItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Updated list");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("putusermangalist")]
        public async Task<IActionResult> PutUserMangaList(MangaList mangaList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            mangaList.UserId = user.Id;
            var mangaListItem = await _context.MangaList.FirstOrDefaultAsync(a => a.MangaItemId == mangaList.MangaItemId && a.UserId == user.Id);
            var novelItem = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == mangaListItem.MangaItemId);
            if (mangaList.UserRating >= 1 && mangaList.UserRating <= 10 && mangaList.UserProgress >= 0 && mangaList.UserProgress <= novelItem.EpisodeCount)
            {
                mangaListItem.UserRating = mangaList.UserRating;
                mangaListItem.UserProgress = mangaList.UserProgress;
                if (mangaListItem.UserProgress == novelItem.EpisodeCount)
                {
                    // Set to complete
                    mangaListItem.CompleteStatus = (CompleteStatusManga)1;
                }
                else if (mangaListItem.UserProgress > 0)
                {
                    // Set to reading
                    mangaListItem.CompleteStatus = (CompleteStatusManga)2;
                }
                _context.Entry(mangaListItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Updated list");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("putusernovellist")]
        public async Task<IActionResult> PutUserNovelList(NovelList novelList)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            novelList.UserId = user.Id;
            var novelListItem = await _context.NovelList.FirstOrDefaultAsync(a => a.NovelItemId == novelList.NovelItemId && a.UserId == user.Id);
            var novelItem = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == novelListItem.NovelItemId);
            if (novelList.UserRating >= 1 && novelList.UserRating <= 10 && novelList.UserProgress >= 0 && novelList.UserProgress <= novelItem.EpisodeCount)
            {
                novelListItem.UserRating = novelList.UserRating;
                novelListItem.UserProgress = novelList.UserProgress;
                if (novelListItem.UserProgress == novelItem.EpisodeCount)
                {
                    // Set to complete
                    novelListItem.CompleteStatus = (CompleteStatusNovel)1;
                }
                else if (novelListItem.UserProgress > 0)
                {
                    // Set to reading
                    novelListItem.CompleteStatus = (CompleteStatusNovel)2;
                }
                _context.Entry(novelListItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Updated list");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("deleteuseranimelist/{animeId}")]
        public async Task<IActionResult> DeleteUserAnimeList(int animeId)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var animeListItem = await _context.AnimeList.FirstOrDefaultAsync(a => a.AnimeItemId == animeId && a.UserId == user.Id);
            _context.AnimeList.Remove(animeListItem);
            var updateAnime = await _context.AnimeItem.FirstOrDefaultAsync(a => a.Id == animeId);
            updateAnime.Members -= 1;
            _context.Entry(updateAnime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Deleted anime from list");
        }

        [HttpDelete]
        [Route("deleteusermangalist/{mangaId}")]
        public async Task<IActionResult> DeleteUserMangaList(int mangaId)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var mangaListItem = await _context.MangaList.FirstOrDefaultAsync(a => a.MangaItemId == mangaId && a.UserId == user.Id);
            _context.MangaList.Remove(mangaListItem);
            var updateManga = await _context.MangaItem.FirstOrDefaultAsync(a => a.Id == mangaId);
            updateManga.Members -= 1;
            _context.Entry(updateManga).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Deleted manga from list");
        }

        [HttpDelete]
        [Route("deleteusernovellist/{novelId}")]
        public async Task<IActionResult> DeleteUserNovelList(int novelId)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var novelListItem = await _context.NovelList.FirstOrDefaultAsync(a => a.NovelItemId == novelId && a.UserId == user.Id);
            _context.NovelList.Remove(novelListItem);
            var updateNovel = await _context.NovelItem.FirstOrDefaultAsync(a => a.Id == novelId);
            updateNovel.Members -= 1;
            _context.Entry(updateNovel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("addfriend")]
        public async Task<IActionResult> PostAddFriend(User newFriendObject)
        {
            var friendId = newFriendObject.Id;
            var friendUserName = newFriendObject.UserName;
            if (friendId == null && friendUserName == null)
            {
                return BadRequest();
            }
            var name = User.FindFirstValue(ClaimTypes.Name);
            if (name == null)
            {
                return BadRequest();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            if (user == null)
            {
                return BadRequest();
            }

            if (friendId != null)
            {
                var newFriend = new FriendList();
                var checkForFriendExists = await _context.Friends.FirstOrDefaultAsync(a => a.UserId == userId && a.FriendId == friendId);
                if (checkForFriendExists == null)
                {
                    newFriend.UserId = userId;
                    newFriend.FriendId = friendId;
                    newFriend.RequestSent = DateTime.Now;
                    newFriend.RequestStatus = 0;
                    _context.Friends.Add(newFriend);
                    await _context.SaveChangesAsync();
                    return Ok("Added friend");
                }
                else
                {
                    return BadRequest("Already added as friend");
                }
            }

            else if (friendUserName != null)
            {
                var friend = await _context.User.FirstOrDefaultAsync(a => a.UserName == friendUserName);
                var newFriend = new FriendList();
                var checkForFriendExists = await _context.Friends.FirstOrDefaultAsync(a => a.UserId == userId && a.FriendId == friend.Id);
                if (checkForFriendExists == null)
                {
                    newFriend.FriendId = friendId;
                    newFriend.UserId = userId;
                    newFriend.RequestSent = DateTime.Now;
                    newFriend.RequestStatus = 0;
                    _context.Friends.Add(newFriend);
                    await _context.SaveChangesAsync();
                    return Ok("Added friend");
                }
                else
                {
                    return BadRequest("Already added as friend");
                }
            }
            else
            {
                return BadRequest("Already added as friend");
            }
        }
        [HttpDelete]
        [Route("deletefriend")]
        public async Task<IActionResult> DeleteFriend(User removeFriend)
        {
            var friendId = removeFriend.Id;
            var friendUserName = removeFriend.UserName;
            if (friendId == null && friendUserName == null)
            {
                return BadRequest();
            }
            var name = User.FindFirstValue(ClaimTypes.Name);
            if (name == null)
            {
                return BadRequest();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            if (user == null)
            {
                return BadRequest();
            }

            if (friendId != null)
            {
                var checkForFriendExists = await _context.Friends.FirstOrDefaultAsync(a => a.UserId == userId && a.FriendId == friendId);
                if (checkForFriendExists != null)
                {
                    _context.Friends.Remove(checkForFriendExists);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest("Not on your friendlist");
                }
            }

            else if (friendUserName != null)
            {
                var friend = await _context.User.FirstOrDefaultAsync(a => a.UserName == friendUserName);
                var checkForFriendExists = await _context.Friends.FirstOrDefaultAsync(a => a.UserId == userId && a.FriendId == friend.Id);
                if (checkForFriendExists != null)
                {
                    _context.Friends.Remove(checkForFriendExists);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest("Is not on your friend list");
                }
            }
            else
            {
                return BadRequest("Is not on your friend list");
            }
        }

        [HttpPut]
        [Route("putfavoriteanime/{id}")]
        public async Task<IActionResult> PutFavoriteAnime(int id)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            if (name == null)
            {
                return BadRequest();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            if (user == null)
            {
                return BadRequest();
            }
            var favorite = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == userId);
            if (favorite == null)
            {
                await CreateFavorite();
                favorite = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == userId);
                if (id == 0)
                {
                    favorite.AnimeItemId = null;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    favorite.AnimeItemId = id;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            else
            {
                if (id == 0)
                {
                    favorite.AnimeItemId = null;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    favorite.AnimeItemId = id;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
        }

        [HttpPut]
        [Route("putfavoritemanga/{id}")]
        public async Task<IActionResult> PutFavoriteManga(int id)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            if (name == null)
            {
                return BadRequest();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            if (user == null)
            {
                return BadRequest();
            }
            var favorite = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == userId);
            if (favorite == null)
            {
                await CreateFavorite();
                favorite = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == userId);
                if (id == 0)
                {
                    favorite.MangaItemId = null;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    favorite.MangaItemId = id;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            else
            {
                if (id == 0)
                {
                    favorite.MangaItemId = null;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    favorite.MangaItemId = id;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
        }

        [HttpPut]
        [Route("putfavoritenovel/{id}")]
        public async Task<IActionResult> PutFavoriteNovel(int id)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            if (name == null)
            {
                return BadRequest();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            if (user == null)
            {
                return BadRequest();
            }
            var favorite = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == userId);
            if (favorite == null)
            {
                await CreateFavorite();
                favorite = await _context.Favorites.FirstOrDefaultAsync(a => a.UserId == userId);
                if (id == 0)
                {
                    favorite.NovelItemId = null;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    favorite.NovelItemId = id;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                
            }
            else
            {
                if (id == 0)
                {
                    favorite.NovelItemId = null;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    favorite.NovelItemId = id;
                    _context.Entry(favorite).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
        }

        private async Task<IActionResult> CreateFavorite()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            if (name == null)
            {
                return BadRequest();
            }
            var user = await _context.User.FirstOrDefaultAsync(a => a.UserName == name);
            var userId = user.Id;
            var newFavorite = new Favorite
            {
                UserId = userId
            };
            _context.Favorites.Add(newFavorite);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}