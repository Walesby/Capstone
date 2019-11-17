using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class HomePageViewModel
    {
        public List<AnimeItem> NewestAnime { get; set; }
        public List<AnimeItem> HighestRated { get; set; }
        public List<Reviews> NewestReviews { get; set; }
    }
}
