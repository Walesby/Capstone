using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class HomePageViewModel
    {
        public List<AnimeItem> NewestAnime { get; set; }
        public List<AnimeItem> HighestRatedAnime { get; set; }
        public List<AnimeReviews> NewestReviewsAnime { get; set; }

        public List<MangaItem> NewestManga { get; set; }
        public List<MangaItem> HighestRatedManga { get; set; }
        public List<MangaReviews> NewestReviewsManga { get; set; }

        public List<NovelItem> NewestNovel { get; set; }
        public List<NovelItem> HighestRatedNovel { get; set; }
        public List<NovelReviews> NewestReviewsNovel { get; set; }
    }
}
