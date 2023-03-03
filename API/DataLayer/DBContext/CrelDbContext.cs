using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Entities
{
    public partial class CRELDBContext : DbContext
    {
        public CRELDBContext()
        {
        }

        public CRELDBContext(DbContextOptions<CRELDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AreaManager> AreaManagers { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<BrandRequest> BrandRequests { get; set; } = null!;
        public virtual DbSet<Broker> Brokers { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<ContractTerm> ContractTerms { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Industry> Industries { get; set; } = null!;
        public virtual DbSet<IndustryLocation> IndustryLocations { get; set; } = null!;
        public virtual DbSet<AreaManagerTeam> AreaManagerTeams { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Media> Media { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<PropertyBrand> PropertyBrands { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StreetSegment> StreetSegments { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("host=host.docker.internal;port=1433;Database=CRELDB;username=sa;password=12341234Minh@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IndustryLocation>(entity =>
            {
                entity.HasKey(e => new { e.IndustryId, e.LocationId });
            });

            modelBuilder.Entity<PropertyBrand>(entity =>
            {
                entity.HasKey(e => new { e.BrandId, e.PropertyId });
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity  .HasOne(l => l.Ward)
                        .WithMany(w => w.Locations)
                        .OnDelete(DeleteBehavior.Restrict);
                
                entity  .HasOne(l => l.StreetSegment)
                        .WithMany(s => s.Locations)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
