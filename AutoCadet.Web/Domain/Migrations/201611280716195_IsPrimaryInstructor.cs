namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsPrimaryInstructor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Obuchenies", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropIndex("dbo.Obuchenies", new[] { "ThumbnailImageFile_Id" });
            AddColumn("dbo.Instructors", "IsPrimary", c => c.Boolean(nullable: false));
            DropColumn("dbo.Obuchenies", "ThumbnailImageFile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Obuchenies", "ThumbnailImageFile_Id", c => c.Int());
            DropColumn("dbo.Instructors", "IsPrimary");
            CreateIndex("dbo.Obuchenies", "ThumbnailImageFile_Id");
            AddForeignKey("dbo.Obuchenies", "ThumbnailImageFile_Id", "dbo.ImageFiles", "Id");
        }
    }
}
