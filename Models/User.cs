using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class User : IdentityUser<Guid>
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinedDate { get; set; }
        public string ProfileImagePath { get; set; }
        public virtual ICollection<AnimeList> AnimeList { get; set; }
        public virtual ICollection<AnimeReviews> AnimeReviews { get; set; }
        public virtual ICollection<MangaList> MangaList { get; set; }
        public virtual ICollection<MangaReviews> MangaReviews { get; set; }
        public virtual ICollection<NovelList> NovelList { get; set; }
        public virtual ICollection<NovelReviews> NovelReviews { get; set; }
        public virtual ICollection<FriendList> UserFriends { get; set; }
        public virtual ICollection<FriendList> FriendUsers { get; set; }
    }
}
