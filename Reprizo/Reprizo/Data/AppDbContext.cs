using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reprizo.Models;
using System.Reflection.Emit;

namespace Reprizo.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Essence> Essences { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<BestWorker> BestWorkers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Collection>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Essence>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Feature>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Setting>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Subscribe>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ContactMessage>().HasQueryFilter(m => !m.SoftDelete);
        }
    }
}
