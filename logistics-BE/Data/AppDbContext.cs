using System;
using logistics_BE.Domain;
using Microsoft.EntityFrameworkCore;

namespace logistics_BE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<City> Cities { get; set; }

        public DbSet<Road> Roads { get; set; }

        public DbSet<LogisticsCenter> LogisticsCenters {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Road>().HasOne<City>(x => x.StartCity).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Road>().HasOne<City>(x => x.EndCity).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
