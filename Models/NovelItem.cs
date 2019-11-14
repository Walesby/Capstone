using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class NovelItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int EpisodeCount { get; set; }
        public string Status { get; set; }
        public DateTime Aired { get; set; }
        public string Premiered { get; set; }
        public string Source { get; set; }
        public int EpisodeDuration { get; set; }
        public string RecommendedAge { get; set; }
        public double Rating { get; set; }
        public int Popularity { get; set; }
        public string Synopsis { get; set; }
        //public List<Reviews> Reviews { get; set; }
        //public List<Genre> Genres { get; set; }
    }
}
