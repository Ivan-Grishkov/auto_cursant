namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetaEnhancment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Metadatas", "Title", c => c.String());
            AddColumn("dbo.Metadatas", "H1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Metadatas", "H1");
            DropColumn("dbo.Metadatas", "Title");
        }
    }
}
