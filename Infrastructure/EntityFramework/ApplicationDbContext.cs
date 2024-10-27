using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Player> Players{ get; set; }
         public virtual DbSet<Depth> DepthChart{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Player>().HasKey(p => p.PlayerNumber);

            modelBuilder.Entity<Depth>().ToTable("DepthChart");
            modelBuilder.Entity<Depth>()
                .HasKey(dc => dc.DepthChartId); // Define the primary key
            modelBuilder.Entity<Depth>()
                .HasOne(dc => dc.Player)
                .WithMany()
                .HasForeignKey(dc => dc.PlayerNumber);
            modelBuilder.Entity<Depth>()
                .HasOne(dc => dc.Position)
                .WithMany()
                .HasForeignKey(dc => dc.PositionId);


            modelBuilder.Entity<Position>()
                .HasKey(p => p.PositionId); // Define primary key
            modelBuilder.Entity<Position>()
                .HasMany(p => p.Depths)
                .WithOne(dc => dc.Position)
                .HasForeignKey(dc => dc.PositionId); // Set up the foreign key relationship

        }
    }
}