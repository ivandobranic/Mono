namespace Project.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure;
    using System.Globalization;
    using Model;

    public class VehicleContext : DbContext
    {

        public VehicleContext()
            : base("name=VehicleContext")
        {

        }
        public virtual DbSet<VehicleMake> VehicleMake { get; set; }
        public virtual DbSet<VehicleModel> VehicleModel { get; set; }
       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMake>()
                .HasMany(e => e.VehicleModel)
                .WithRequired(e => e.VehicleMake)
                .HasForeignKey(e => e.MakeId)
                .WillCascadeOnDelete();
            modelBuilder.Entity<VehicleMake>()
                .Property(e => e.Id)
                .IsRequired();
            modelBuilder.Entity<VehicleMake>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<VehicleMake>()
                .Property(e => e.Abrv)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<VehicleModel>()
               .Property(e => e.Id)
               .IsRequired();
            modelBuilder.Entity<VehicleModel>()
                .Property(e => e.MakeId)
                .IsRequired();
            modelBuilder.Entity<VehicleModel>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<VehicleModel>()
                .Property(e => e.Abrv)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
