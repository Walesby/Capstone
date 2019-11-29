using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class NovelItemViewModel
    {
        public NovelItem Novel { get; set; }
        public List<User> UserList { get; set; }
        public List<NovelReviews> ReviewsList { get; set; }
        public bool UserListContains { get; set; }
        public bool UserFavoriteContains { get; set; }
    }
}
