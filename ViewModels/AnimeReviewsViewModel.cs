using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels.ReviewsViewModel
{
    public class AnimeReviewsViewModel
    {
        public List<User> UserList { get; set; }
        public List<AnimeItem> AnimeInfoList { get; set; }
        public List<AnimeReviews> ReviewsList { get; set; }
    }
}
