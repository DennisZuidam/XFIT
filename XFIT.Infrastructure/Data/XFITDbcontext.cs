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
        }
    }
}