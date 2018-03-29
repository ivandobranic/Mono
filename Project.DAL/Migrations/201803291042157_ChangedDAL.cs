namespace Project.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDAL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleModels", "MakeId", "dbo.VehicleMakes");
            DropIndex("dbo.VehicleModels", new[] { "MakeId" });
            CreateTable(
                "dbo.VehicleMake",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Abrv = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        MakeId = c.Int(nullable: false),
                        Abrv = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleMake", t => t.MakeId, cascadeDelete: true)
                .Index(t => t.MakeId);
            
            DropTable("dbo.VehicleMakes");
            DropTable("dbo.VehicleModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VehicleModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        MakeId = c.Int(nullable: false),
                        Abrv = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleMakes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Abrv = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.VehicleModel", "MakeId", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "MakeId" });
            DropTable("dbo.VehicleModel");
            DropTable("dbo.VehicleMake");
            CreateIndex("dbo.VehicleModels", "MakeId");
            AddForeignKey("dbo.VehicleModels", "MakeId", "dbo.VehicleMakes", "Id", cascadeDelete: true);
        }
    }
}
