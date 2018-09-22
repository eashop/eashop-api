using EaShop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EaShop.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Goods> Goods { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Feedback>()
                .HasKey(f => new { f.GoodsId, f.UserId });

            builder.Entity<GoodsInOrder>()
                .HasKey(g => new { g.GoodsId, g.OrderId });

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Feedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasMany(c => c.Goods)
                .WithOne(g => g.Category)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Goods>()
                .HasMany(g => g.GoodsInOrder)
                .WithOne(o => o.Goods)
                .HasForeignKey(o => o.GoodsId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Goods>()
                .HasMany(g => g.Feedbacks)
                .WithOne(f => f.Goods)
                .HasForeignKey(f => f.GoodsId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasMany(o => o.GoodsInOrder)
                .WithOne(g => g.Order)
                .HasForeignKey(g => g.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}