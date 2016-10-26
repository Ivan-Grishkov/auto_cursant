namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallMeCreatedDateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CallMes", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CallMes", "CreatedDate");
        }
    }
}
