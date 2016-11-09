namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServicesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(nullable: false),
                        DetailText = c.String(),
                        ListHeader = c.String(nullable: false),
                        ListDescription = c.String(nullable: false),
                        ListIcon = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        SortingNumber = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Metadata_Id = c.Int(),
                        ThumbnailImageFile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .ForeignKey("dbo.ImageFiles", t => t.ThumbnailImageFile_Id)
                .Index(t => t.Metadata_Id)
                .Index(t => t.ThumbnailImageFile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Services", "Metadata_Id", "dbo.Metadatas");
            DropIndex("dbo.Services", new[] { "ThumbnailImageFile_Id" });
            DropIndex("dbo.Services", new[] { "Metadata_Id" });
            DropTable("dbo.Services");
        }
    }
}
