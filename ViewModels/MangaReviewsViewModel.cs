using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class MangaReviewsViewModel
    {
        public List<User> UserList { get; set; }
        public List<MangaItem> MangaInfoList { get; set; }
        public List<MangaReviews> ReviewsList { get; set; }
    }
}
