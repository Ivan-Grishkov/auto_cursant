namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneMetaForInstrucor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Metadatas", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Metadatas", new[] { "Instructor_Id" });
            AddColumn("dbo.Instructors", "Metadata_Id", c => c.Int());
            CreateIndex("dbo.Instructors", "Metadata_Id");
            AddForeignKey("dbo.Instructors", "Metadata_Id", "dbo.Metadatas", "Id");
            DropColumn("dbo.Metadatas", "Instructor_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Metadatas", "Instructor_Id", c => c.Int());
            DropForeignKey("dbo.Instructors", "Metadata_Id", "dbo.Metadatas");
            DropIndex("dbo.Instructors", new[] { "Metadata_Id" });
            DropColumn("dbo.Instructors", "Metadata_Id");
            CreateIndex("dbo.Metadatas", "Instructor_Id");
            AddForeignKey("dbo.Metadatas", "Instructor_Id", "dbo.Instructors", "Id");
        }
    }
}
