using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? AnimeItemId { get; set; }
        public int? MangaItemId { get; set; }
        public int? NovelItemId { get; set; }

        public virtual AnimeItem AnimeFavorite { get; set; }
        public virtual MangaItem MangaFavorite { get; set; }
        public virtual NovelItem NovelFavorite { get; set; }
    }
}
