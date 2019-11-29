using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class UserNovelReviewViewModel
    {
        public List<string> UsernameList { get; set; }
        public List<string> UserImageUrl { get; set; }
        public List<NovelReviews> ReviewsList { get; set; }
    }
}
