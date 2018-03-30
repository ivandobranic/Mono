namespace Project.DAL
{
    using System.Data.Entity;
    using Entities;

    public class VehicleContext : DbContext
    {

        public VehicleContext()
            : base("name=VehicleContext")
        {

        }
        public virtual DbSet<VehicleMakeEntity> VehicleMake { get; set; }
        public virtual DbSet<VehicleModelEntity> VehicleModel { get; set; }
       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMakeEntity>()
                .HasMany(e => e.VehicleModelEntity)
                .WithRequired(e => e.VehicleMakeEntity)
                .HasForeignKey(e => e.MakeId)
                .WillCascadeOnDelete();
            modelBuilder.Entity<VehicleMakeEntity>()
                .Property(e => e.Id)
                .IsRequired();
            modelBuilder.Entity<VehicleMakeEntity>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<VehicleMakeEntity>()
                .Property(e => e.Abrv)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<VehicleModelEntity>()
               .Property(e => e.Id)
               .IsRequired();
            modelBuilder.Entity<VehicleModelEntity>()
                .Property(e => e.MakeId)
                .IsRequired();
            modelBuilder.Entity<VehicleModelEntity>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<VehicleModelEntity>()
                .Property(e => e.Abrv)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
