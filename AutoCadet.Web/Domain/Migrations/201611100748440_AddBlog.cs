namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(nullable: false),
                        YoutubeUrl = c.String(),
                        DetailsText = c.String(),
                        DetailsSectionHeader = c.String(),
                        ListHeader = c.String(),
                        ListDescription = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        SortingNumber = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        DetailsImageFile_Id = c.Int(),
                        Metadata_Id = c.Int(),
                        ThumbnailImageFile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFiles", t => t.DetailsImageFile_Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .ForeignKey("dbo.ImageFiles", t => t.ThumbnailImageFile_Id)
                .Index(t => t.DetailsImageFile_Id)
                .Index(t => t.Metadata_Id)
                .Index(t => t.ThumbnailImageFile_Id);
            
            AlterColumn("dbo.Trainings", "ListDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Blogs", "Metadata_Id", "dbo.Metadatas");
            DropForeignKey("dbo.Blogs", "DetailsImageFile_Id", "dbo.ImageFiles");
            DropIndex("dbo.Blogs", new[] { "ThumbnailImageFile_Id" });
            DropIndex("dbo.Blogs", new[] { "Metadata_Id" });
            DropIndex("dbo.Blogs", new[] { "DetailsImageFile_Id" });
            AlterColumn("dbo.Trainings", "ListDescription", c => c.String(nullable: false));
            DropTable("dbo.Blogs");
        }
    }
}
