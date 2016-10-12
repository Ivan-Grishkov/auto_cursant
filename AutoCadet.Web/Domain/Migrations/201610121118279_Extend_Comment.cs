namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extend_Comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Score", c => c.String());
            DropColumn("dbo.Comments", "CreatedDate");
        }
    }
}
