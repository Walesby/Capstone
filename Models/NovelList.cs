using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public enum CompleteStatusNovel
    {
        Add, Complete, Reading
    }
    public class NovelList
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int NovelItemId { get; set; }
        public int UserRating { get; set; }
        public int UserProgress { get; set; }
        public CompleteStatusNovel CompleteStatus { get; set; }

        public virtual NovelItem Novel { get; set; }
        public virtual User User { get; set; }
    }
}
