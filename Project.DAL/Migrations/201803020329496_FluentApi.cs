namespace Project.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluentApi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleMakes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.VehicleMakes", "Abrv", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.VehicleModels", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.VehicleModels", "Abrv", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleModels", "Abrv", c => c.String());
            AlterColumn("dbo.VehicleModels", "Name", c => c.String());
            AlterColumn("dbo.VehicleMakes", "Abrv", c => c.String());
            AlterColumn("dbo.VehicleMakes", "Name", c => c.String());
        }
    }
}
