namespace AutoCadet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameServiceToTraining : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Services", newName: "Trainings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Trainings", newName: "Services");
        }
    }
}
