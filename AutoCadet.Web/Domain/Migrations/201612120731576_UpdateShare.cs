namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateShare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShareEvents", "Metadata_Id", c => c.Int());
            CreateIndex("dbo.ShareEvents", "Metadata_Id");
            AddForeignKey("dbo.ShareEvents", "Metadata_Id", "dbo.Metadatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShareEvents", "Metadata_Id", "dbo.Metadatas");
            DropIndex("dbo.ShareEvents", new[] { "Metadata_Id" });
            DropColumn("dbo.ShareEvents", "Metadata_Id");
        }
    }
}
