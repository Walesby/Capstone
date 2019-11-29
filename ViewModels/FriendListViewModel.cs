using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class FriendListViewModel
    {
        public List<User> UserList { get; set; }
        public List<AnimeItem> LastAnimeUpdateList { get; set; }
        public List<MangaItem> LastMangaUpdateList { get; set; }
        public List<NovelItem> LastNovelUpdateList { get; set; }
    }
}
