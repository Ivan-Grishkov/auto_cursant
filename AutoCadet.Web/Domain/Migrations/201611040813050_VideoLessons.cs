namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoLessons : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoLessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(nullable: false),
                        YoutubeUrl = c.String(nullable: false),
                        Text = c.String(),
                        Metadata_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .Index(t => t.Metadata_Id);
            
            AddColumn("dbo.CallMes", "RequesterName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoLessons", "Metadata_Id", "dbo.Metadatas");
            DropIndex("dbo.VideoLessons", new[] { "Metadata_Id" });
            DropColumn("dbo.CallMes", "RequesterName");
            DropTable("dbo.VideoLessons");
        }
    }
}
