using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class UserAnimeListViewModel
    {
        public User User { get; set; }
        public List<AnimeList> UserAnimeList { get; set; }
        public List<AnimeItem> AnimeInfoList { get; set; }
    }
}
