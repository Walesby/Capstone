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
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinedDate { get; set; }

        public string ProfileImagePath { get; set; }
        public char Gender { get; set; }
        public virtual ICollection<AnimeList> AnimeList { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
        //public List<MangaItem> MangaList { get; set; }
        //public List<NovelItem> NovelList { get; set; }

        //public virtual ICollection<Friend> FriendList { get; set; }
        //public List<Message> MessageList { get; set; }
        //public List<Favorite> Favorites { get; set; }
    }
}
