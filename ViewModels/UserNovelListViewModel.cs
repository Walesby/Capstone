using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class UserNovelListViewModel
    {
        public User User { get; set; }
        public List<NovelList> UserNovelList { get; set; }
        public List<NovelItem> NovelInfoList { get; set; }
    }
}
