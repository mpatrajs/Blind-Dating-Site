using BDate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().HasKey(table => new {
                table.fromProfileId,
                table.toProfileId
            });
            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }
        //Here we are linking fresh Entity to Table in DB that is going to be named Tests 
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserToCategories> UserToCategories { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Personality> Personalities { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
