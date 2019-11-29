using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public Favorite Favorite { get; set; }
        public AnimeItem FavoriteAnime { get; set; }
        public MangaItem FavoriteManga { get; set; }
        public NovelItem FavoriteNovel{ get; set; }
        public bool UserIsFriend { get; set; }

        public List<MangaItem> LatestMangaUpdates { get; set; }
        public int MangaReading { get; set; }
        public int MangaComplete { get; set; }
        public int MangaAverageScore { get; set; }

        public List<AnimeItem> LatestAnimeUpdates { get; set; }
        public int AnimeWatching { get; set; }
        public int AnimeComplete { get; set; }
        public int AnimeAverageScore { get; set; }

        public List<NovelItem> LatestNovelUpdates { get; set; }
        public int NovelReading { get; set; }
        public int NovelComplete { get; set; }
        public int NovelAverageScore { get; set; }

    }
}
