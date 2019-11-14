using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public enum CompleteStatus
    {
        complete,watching,plan
    }
    public class AnimeList
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int AnimeItemId { get; set; }
        public int UserRating { get; set; }
        public int UserProgress { get; set; }
        public CompleteStatus CompleteStatus { get; set; }

        public virtual AnimeItem Anime { get; set; }
        public virtual User User { get; set; }
    }
}
