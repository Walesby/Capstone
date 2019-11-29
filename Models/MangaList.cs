using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public enum CompleteStatusManga
    {
        Add, Complete, Reading
    }
    public class MangaList
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int MangaItemId { get; set; }
        public int UserRating { get; set; }
        public int UserProgress { get; set; }
        public CompleteStatusManga CompleteStatus { get; set; }

        public virtual MangaItem Manga { get; set; }
        public virtual User User { get; set; }
    }
}
