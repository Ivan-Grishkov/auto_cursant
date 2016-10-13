namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMetadataToInstructor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Metadatas", "Instructor_Id", c => c.Int());
            CreateIndex("dbo.Metadatas", "Instructor_Id");
            AddForeignKey("dbo.Metadatas", "Instructor_Id", "dbo.Instructors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Metadatas", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Metadatas", new[] { "Instructor_Id" });
            DropColumn("dbo.Metadatas", "Instructor_Id");
        }
    }
}
