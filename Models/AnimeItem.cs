using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class AnimeItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        public int EpisodeCount { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Premiered { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string RecommendedAge { get; set; }
        public double Rating { get; set; }
        public int Popularity { get; set; }
        [Required]
        public string Synopsis { get; set; }
        public string ImagePath { get; set; }
        public int Members { get; set; }

        public virtual ICollection<AnimeList> AnimeLists { get; set; }
        public virtual ICollection<AnimeReviews> Reviews { get; set; }

    }
}
