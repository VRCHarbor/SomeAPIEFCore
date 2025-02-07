using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using SomeAPIEFCore.Data.Entities;

namespace SomeAPIEFCore.Data.Context
{
    public class RoadMapDbContext : DbContext
    {
        public DbSet<RoadMap> roadMaps { get; set; }
        public DbSet<RoadMapElement> roadMapElements { get; set; }
        public DbSet<RoadMapCategory> roadMapCategories { get; set; }
        public DbSet<ImageEntity> images { get; set; }
        public RoadMapDbContext(DbContextOptions optons) : base(optons)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoadMap>(i =>
            {
                i.HasKey(i => i.Id);
                i.HasMany(i => i.RoadMapElements).WithOne(i=>i.Host).HasForeignKey(i => i.RoadMapId);
            });

            modelBuilder.Entity<ImageEntity>(i =>
            {
                i.HasKey(i => i.Id);
            });

            modelBuilder.Entity<RoadMapElement>(i =>
            {
                i.HasKey(i => i.Id);
                i.Property(i => i.Content).HasColumnType("ntext");
            });

            modelBuilder.Entity<RoadMapCategory>(i =>
            {
                i.HasKey(i => i.Id);
                i.HasMany(i => i.roadMaps).WithOne(i => i.Category).HasForeignKey(i => i.CategoryId);
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}
