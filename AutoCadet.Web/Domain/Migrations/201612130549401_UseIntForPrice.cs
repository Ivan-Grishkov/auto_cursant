namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseIntForPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instructors", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
