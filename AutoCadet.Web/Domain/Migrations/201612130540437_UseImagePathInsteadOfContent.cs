namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseImagePathInsteadOfContent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Instructors", "ThumbnailImage_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Videos", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropIndex("dbo.Blogs", new[] { "ThumbnailImageFile_Id" });
            DropIndex("dbo.Instructors", new[] { "ThumbnailImage_Id" });
            DropIndex("dbo.Videos", new[] { "ThumbnailImageFile_Id" });
            AddColumn("dbo.Blogs", "ThumbnailImageName", c => c.String(nullable: false));
            AddColumn("dbo.Instructors", "ThumbnailImageName", c => c.String(nullable: false));
            AddColumn("dbo.Videos", "ThumbnailImageName", c => c.String(nullable: false));
            AlterColumn("dbo.Blogs", "ListHeader", c => c.String(nullable: false));
            DropColumn("dbo.Blogs", "ThumbnailImageFile_Id");
            DropColumn("dbo.Instructors", "ThumbnailImage_Id");
            DropColumn("dbo.Videos", "ThumbnailImageFile_Id");
            DropTable("dbo.ImageFiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bytes = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Videos", "ThumbnailImageFile_Id", c => c.Int());
            AddColumn("dbo.Instructors", "ThumbnailImage_Id", c => c.Int());
            AddColumn("dbo.Blogs", "ThumbnailImageFile_Id", c => c.Int());
            AlterColumn("dbo.Blogs", "ListHeader", c => c.String());
            DropColumn("dbo.Videos", "ThumbnailImageName");
            DropColumn("dbo.Instructors", "ThumbnailImageName");
            DropColumn("dbo.Blogs", "ThumbnailImageName");
            CreateIndex("dbo.Videos", "ThumbnailImageFile_Id");
            CreateIndex("dbo.Instructors", "ThumbnailImage_Id");
            CreateIndex("dbo.Blogs", "ThumbnailImageFile_Id");
            AddForeignKey("dbo.Videos", "ThumbnailImageFile_Id", "dbo.ImageFiles", "Id");
            AddForeignKey("dbo.Instructors", "ThumbnailImage_Id", "dbo.ImageFiles", "Id");
            AddForeignKey("dbo.Blogs", "ThumbnailImageFile_Id", "dbo.ImageFiles", "Id");
        }
    }
}
