using ApiProject.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Criminal> Criminals { get; set; }
        public DbSet<Address> Addresss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Criminal>()
                .HasOne(c => c.address)
                .WithOne(a => a.Criminal)
                .HasForeignKey<Address>(a => a.CriminalId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
