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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FriendList>().HasKey(r => new {r.UserId, r.FriendId });
            builder.Entity<FriendList>().HasOne(r => r.User).WithMany(u => u.UserFriends).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FriendList>().HasOne(r => r.Friend).WithMany(u => u.FriendUsers).HasForeignKey(r => r.FriendId).OnDelete(DeleteBehavior.Restrict);
        }
        public virtual DbSet<AnimeItem> AnimeItem { get; set; }
        public virtual DbSet<MangaItem> MangaItem { get; set; }
        public virtual DbSet<NovelItem> NovelItem { get; set; }
        public virtual DbSet<FriendList> Friends { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<AnimeList> AnimeList { get; set; }
        public virtual DbSet<MangaList> MangaList { get; set; }
        public virtual DbSet<NovelList> NovelList { get; set; }
        public virtual DbSet<AnimeReviews> AnimeReviews { get; set; }
        public virtual DbSet<MangaReviews> MangaReviews { get; set; }
        public virtual DbSet<NovelReviews> NovelReviews { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
    }
}
