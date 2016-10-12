namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowHideComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "IsVisibleInList", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Comments", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Name", c => c.String());
            DropColumn("dbo.Comments", "IsActive");
            DropColumn("dbo.Comments", "IsVisibleInList");
        }
    }
}
