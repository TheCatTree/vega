using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeatureMakeJoin>()
                .HasKey(fmj => new { fmj.MakeId, fmj.FeatureId });  
            modelBuilder.Entity<FeatureMakeJoin>()
                .HasOne<Make>(fmj => fmj.Make)
                .WithMany(m => m.FeatureMakeJoins)
                .HasForeignKey(fmj => fmj.MakeId);  
            modelBuilder.Entity<FeatureMakeJoin>()
                .HasOne<Feature>(fmj => fmj.Feature)
                .WithMany(f => f.FeatureMakeJoins)
                .HasForeignKey(fmj => fmj.FeatureId);
        }

        public DbSet<Make> Makes {get; set;}
        public DbSet<Feature> Features {get; set;}
        public DbSet<FeatureMakeJoin> FeatureMakeJoins {get; set;}

    }
}