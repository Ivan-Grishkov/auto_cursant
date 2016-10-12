namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceToInstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "Price");
        }
    }
}
