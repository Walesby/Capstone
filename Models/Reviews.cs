using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime PostDate { get; set; }
        public int Rating { get; set; }
        public int Progress { get; set; }
    }
}
