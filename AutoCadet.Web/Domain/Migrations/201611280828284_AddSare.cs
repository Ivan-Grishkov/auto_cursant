namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShare : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShareEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false),
                        Text = c.String(),
                        TinyText = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShareEvents");
        }
    }
}
