using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class UserMangaListViewModel
    {
        public User User { get; set; }
        public List<MangaList> UserMangaList { get; set; }
        public List<MangaItem> MangaInfoList { get; set; }
    }
}
