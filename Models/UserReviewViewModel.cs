using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class UserReviewViewModel
    {
        public List<string> UsernameList { get; set; }
        public List<Reviews> ReviewsList { get; set; }
    }
}
