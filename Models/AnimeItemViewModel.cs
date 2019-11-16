using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class AnimeItemViewModel
    {
        public AnimeItem Anime { get; set; }
        public List<User> UserList { get; set; }
        public List<Reviews> ReviewsList { get; set; }
    }
}
