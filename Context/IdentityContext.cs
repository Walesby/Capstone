using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Context
{
    public class IdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<AnimeItem> AnimeItem { get; set; }
        public virtual DbSet<MangaItem> MangaItem { get; set; }
        public virtual DbSet<NovelItem> NovelItem { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<AnimeList> AnimeList { get; set; }
    }
}
