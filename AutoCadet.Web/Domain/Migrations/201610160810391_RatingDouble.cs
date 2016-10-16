namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Score", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Score", c => c.Int(nullable: false));
        }
    }
}
