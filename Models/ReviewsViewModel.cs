using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ReviewsViewModel
    {
        public List<User> UserList { get; set; }
        public List<AnimeItem> AnimeInfoList { get; set; }
        public List<Reviews> ReviewsList { get; set; }
    }
}
