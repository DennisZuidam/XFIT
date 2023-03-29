using Microsoft.EntityFrameworkCore;
using XFIT.Core.Entities;

namespace XFIT.Infrastructure.Data
{
    public class XfitDbContext : DbContext
    {
        public XfitDbContext(DbContextOptions<XfitDbContext> options) : base(options)
        {
        }
        
        public DbSet<Activity> Activities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasKey(a => a.Id);
            
            modelBuilder.Entity<Activity>()
                .Property(a => a.Calo)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Activity>()
                .Property(a => a.Distance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Activity>()
                .Property(a => a.Elev)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Activity>()
                .Property(a => a.EstSpeed)
                .HasColumnType("decimal(18,2)");
        }
    }
}