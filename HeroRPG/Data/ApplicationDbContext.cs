using HeroRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroRPG.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Fireball", Damage = 20},
                new Skill { Id = 2, Name = "Blizzard", Damage = 10},
                new Skill { Id = 3, Name = "Freeze", Damage = 30}
            );
        }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
