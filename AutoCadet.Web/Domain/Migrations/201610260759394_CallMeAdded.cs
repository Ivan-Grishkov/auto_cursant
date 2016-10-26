namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallMeAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CallMes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(nullable: false),
                        IsHandled = c.Boolean(nullable: false),
                        Instructor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.Instructor_Id)
                .Index(t => t.Instructor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CallMes", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.CallMes", new[] { "Instructor_Id" });
            DropTable("dbo.CallMes");
        }
    }
}
