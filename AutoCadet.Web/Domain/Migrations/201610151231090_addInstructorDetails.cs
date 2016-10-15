namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInstructorDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instructors", "Metadata_Id", "dbo.Metadatas");
            DropIndex("dbo.Instructors", new[] { "Metadata_Id" });
            CreateTable(
                "dbo.InstructorDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Description = c.String(),
                        VehicleDescriprion = c.String(),
                        VehicleTitle = c.String(),
                        VehicleSecondaryTitle = c.String(),
                        FuelConsumption = c.String(),
                        DetailsImage_Id = c.Int(),
                        Metadata_Id = c.Int(),
                        VehicleImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFiles", t => t.DetailsImage_Id)
                .ForeignKey("dbo.Instructors", t => t.Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .ForeignKey("dbo.ImageFiles", t => t.VehicleImage_Id)
                .Index(t => t.Id)
                .Index(t => t.DetailsImage_Id)
                .Index(t => t.Metadata_Id)
                .Index(t => t.VehicleImage_Id);
            
            AddColumn("dbo.Comments", "InstructorDetails_Id", c => c.Int());
            AddColumn("dbo.Instructors", "Email", c => c.String());
            CreateIndex("dbo.Comments", "InstructorDetails_Id");
            AddForeignKey("dbo.Comments", "InstructorDetails_Id", "dbo.InstructorDetails", "Id");
            DropColumn("dbo.Instructors", "Metadata_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instructors", "Metadata_Id", c => c.Int());
            DropForeignKey("dbo.InstructorDetails", "VehicleImage_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.InstructorDetails", "Metadata_Id", "dbo.Metadatas");
            DropForeignKey("dbo.InstructorDetails", "Id", "dbo.Instructors");
            DropForeignKey("dbo.InstructorDetails", "DetailsImage_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Comments", "InstructorDetails_Id", "dbo.InstructorDetails");
            DropIndex("dbo.InstructorDetails", new[] { "VehicleImage_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "Metadata_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "DetailsImage_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "Id" });
            DropIndex("dbo.Comments", new[] { "InstructorDetails_Id" });
            DropColumn("dbo.Instructors", "Email");
            DropColumn("dbo.Comments", "InstructorDetails_Id");
            DropTable("dbo.InstructorDetails");
            CreateIndex("dbo.Instructors", "Metadata_Id");
            AddForeignKey("dbo.Instructors", "Metadata_Id", "dbo.Metadatas", "Id");
        }
    }
}
