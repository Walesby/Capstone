using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class NovelReviewsViewModel
    {
        public List<User> UserList { get; set; }
        public List<NovelItem> NovelInfoList { get; set; }
        public List<NovelReviews> ReviewsList { get; set; }
    }
}
