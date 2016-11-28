namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDetailsImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "DetailsImageFile_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.InstructorDetails", "DetailsImage_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.InstructorDetails", "VehicleImage_Id", "dbo.ImageFiles");
            DropIndex("dbo.Blogs", new[] { "DetailsImageFile_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "DetailsImage_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "VehicleImage_Id" });
            DropColumn("dbo.Blogs", "DetailsImageFile_Id");
            DropColumn("dbo.InstructorDetails", "FuelConsumption");
            DropColumn("dbo.InstructorDetails", "DetailsImage_Id");
            DropColumn("dbo.InstructorDetails", "VehicleImage_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InstructorDetails", "VehicleImage_Id", c => c.Int());
            AddColumn("dbo.InstructorDetails", "DetailsImage_Id", c => c.Int());
            AddColumn("dbo.InstructorDetails", "FuelConsumption", c => c.String());
            AddColumn("dbo.Blogs", "DetailsImageFile_Id", c => c.Int());
            CreateIndex("dbo.InstructorDetails", "VehicleImage_Id");
            CreateIndex("dbo.InstructorDetails", "DetailsImage_Id");
            CreateIndex("dbo.Blogs", "DetailsImageFile_Id");
            AddForeignKey("dbo.InstructorDetails", "VehicleImage_Id", "dbo.ImageFiles", "Id");
            AddForeignKey("dbo.InstructorDetails", "DetailsImage_Id", "dbo.ImageFiles", "Id");
            AddForeignKey("dbo.Blogs", "DetailsImageFile_Id", "dbo.ImageFiles", "Id");
        }
    }
}
