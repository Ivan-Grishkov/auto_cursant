namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhanceVideoLessons : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoLessons", "ListHeader", c => c.String());
            AddColumn("dbo.VideoLessons", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.VideoLessons", "SortingNumber", c => c.Int(nullable: false));
            AddColumn("dbo.VideoLessons", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.VideoLessons", "ThumbnailImageFile_Id", c => c.Int());
            CreateIndex("dbo.VideoLessons", "ThumbnailImageFile_Id");
            AddForeignKey("dbo.VideoLessons", "ThumbnailImageFile_Id", "dbo.ImageFiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoLessons", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropIndex("dbo.VideoLessons", new[] { "ThumbnailImageFile_Id" });
            DropColumn("dbo.VideoLessons", "ThumbnailImageFile_Id");
            DropColumn("dbo.VideoLessons", "CreatedDate");
            DropColumn("dbo.VideoLessons", "SortingNumber");
            DropColumn("dbo.VideoLessons", "IsActive");
            DropColumn("dbo.VideoLessons", "ListHeader");
        }
    }
}
