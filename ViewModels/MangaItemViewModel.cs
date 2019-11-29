using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class MangaItemViewModel
    {
        public MangaItem Manga { get; set; }
        public List<User> UserList { get; set; }
        public List<MangaReviews> ReviewsList { get; set; }
        public bool UserListContains { get; set; }
        public bool UserFavoriteContains { get; set; }
    }
}
