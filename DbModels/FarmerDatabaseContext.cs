using Microsoft.EntityFrameworkCore;

namespace FarmerDB.DataAccess.DbModels;

public partial class FarmerDatabaseContext : DbContext
{
    public FarmerDatabaseContext()
    {
    }

    public FarmerDatabaseContext(DbContextOptions<FarmerDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCounty> TblCounties { get; set; }

    public virtual DbSet<TblFarmerParcel> TblFarmerParcels { get; set; }

    public virtual DbSet<TblFarmerProfile> TblFarmerProfiles { get; set; }

    public virtual DbSet<TblSubCounty> TblSubCounties { get; set; }

    public virtual DbSet<TblValueChain> TblValueChains { get; set; }

    public virtual DbSet<TblWard> TblWards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=10.101.100.75;Database=Farmer_Database; Integrated Security=True; Trusted_Connection=False; TrustServerCertificate=True; User ID=Allan; Password=AO5050#$");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCounty>(entity =>
        {
            entity.HasKey(e => e.CountyId);

            entity.ToTable("tblCounty");

            entity.Property(e => e.CountyId)
                .ValueGeneratedNever()
                .HasColumnName("county_id");
            entity.Property(e => e.CountyName)
                .HasMaxLength(50)
                .HasColumnName("county_name");
        });

        modelBuilder.Entity<TblFarmerParcel>(entity =>
        {
            entity.HasKey(e => e.ParcelId);

            entity.ToTable("tblFarmerParcels");

            entity.Property(e => e.ParcelId).HasColumnName("parcel_id");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(32)
                .HasColumnName("farmer_id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.ValueChainId).HasColumnName("value_chain_id");

            entity.HasOne(d => d.Farmer).WithMany(p => p.TblFarmerParcels)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblFarmerParcels_tblFarmerProfiles");

            entity.HasOne(d => d.ValueChain).WithMany(p => p.TblFarmerParcels)
                .HasForeignKey(d => d.ValueChainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblFarmerParcels_tblValueChains");
        });

        modelBuilder.Entity<TblFarmerProfile>(entity =>
        {
            entity.HasKey(e => e.FarmerId);

            entity.ToTable("tblFarmerProfiles");

            entity.Property(e => e.FarmerId)
                .HasMaxLength(32)
                .HasColumnName("Farmer_Id");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.DataSource)
                .HasMaxLength(50)
                .HasColumnName("Data_Source");
            entity.Property(e => e.EducationLevel)
                .HasMaxLength(128)
                .HasColumnName("Education_Level");
            entity.Property(e => e.FirstName)
                .HasMaxLength(128)
                .HasColumnName("First_Name");
            entity.Property(e => e.Gender).HasMaxLength(128);
            entity.Property(e => e.HouseholdSize)
                .HasMaxLength(256)
                .HasColumnName("Household_Size");
            entity.Property(e => e.KcsapBeneficiary)
                .HasMaxLength(50)
                .HasColumnName("KCSAP_Beneficiary");
            entity.Property(e => e.LastName)
                .HasMaxLength(128)
                .HasColumnName("Last_Name");
            entity.Property(e => e.NationalId)
                .HasMaxLength(128)
                .HasColumnName("National_Id");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(128)
                .HasColumnName("Phone_No");
            entity.Property(e => e.PreferredAdvisoryFormat)
                .HasMaxLength(3500)
                .HasColumnName("Preferred_Advisory_Format");
            entity.Property(e => e.PreferredAdvisoryLanguage)
                .HasMaxLength(256)
                .HasColumnName("Preferred_Advisory_Language");
            entity.Property(e => e.PreferredAdvisoryTime)
                .HasMaxLength(256)
                .HasColumnName("Preferred_Advisory_Time");
            entity.Property(e => e.RecordDate)
                .HasMaxLength(128)
                .HasColumnName("Record_Date");
            entity.Property(e => e.WardId).HasColumnName("ward_id");
            entity.Property(e => e.Yob).HasColumnName("YOB");
        });

        modelBuilder.Entity<TblSubCounty>(entity =>
        {
            entity.HasKey(e => e.SubcountyId);

            entity.ToTable("tblSubCounty");

            entity.Property(e => e.SubcountyId)
                .ValueGeneratedNever()
                .HasColumnName("subcounty_id");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.SubcountyName)
                .HasMaxLength(50)
                .HasColumnName("subcounty_name");

            entity.HasOne(d => d.County).WithMany(p => p.TblSubCounties)
                .HasForeignKey(d => d.CountyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSubCounty_tblCounty");
        });

        modelBuilder.Entity<TblValueChain>(entity =>
        {
            entity.HasKey(e => e.ValueChainId);

            entity.ToTable("tblValueChains");

            entity.Property(e => e.ValueChainId).HasColumnName("value_chain_id");
            entity.Property(e => e.ValueChainName)
                .HasMaxLength(256)
                .HasColumnName("value_chain_name");
            entity.Property(e => e.ValueChainTypeId).HasColumnName("value_chain_type_id");
        });

        modelBuilder.Entity<TblWard>(entity =>
        {
            entity.HasKey(e => e.WardId);

            entity.ToTable("tblWard");

            entity.Property(e => e.WardId)
                .ValueGeneratedNever()
                .HasColumnName("ward_id");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.SubcountyId).HasColumnName("subcounty_id");
            entity.Property(e => e.WardName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ward_name");

            entity.HasOne(d => d.County).WithMany(p => p.TblWards)
                .HasForeignKey(d => d.CountyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblWard_tblCounty");

            entity.HasOne(d => d.Subcounty).WithMany(p => p.TblWards)
                .HasForeignKey(d => d.SubcountyId)
                .HasConstraintName("FK_tblWard_tblSubCounty");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
