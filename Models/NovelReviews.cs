using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class NovelReviews
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }       
        public Guid UserId { get; set; }
        public int NovelItemId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }

        public virtual NovelItem Novel { get; set; }
        public virtual User User { get; set; }
    }
}
