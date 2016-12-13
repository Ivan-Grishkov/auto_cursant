namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgFileNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "ThumbnailImageName", c => c.String());
            AlterColumn("dbo.Instructors", "ThumbnailImageName", c => c.String());
            AlterColumn("dbo.Videos", "ThumbnailImageName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "ThumbnailImageName", c => c.String(nullable: false));
            AlterColumn("dbo.Instructors", "ThumbnailImageName", c => c.String(nullable: false));
            AlterColumn("dbo.Blogs", "ThumbnailImageName", c => c.String(nullable: false));
        }
    }
}
