namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class instructor_price_nummable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instructors", "Price", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "Price", c => c.Int(nullable: false));
        }
    }
}
