using System.Data.Entity.Migrations;

namespace AutoCadet.Domain.Migrations
{
    public partial class InitMySql : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(nullable: false, unicode: false),
                        YoutubeUrl = c.String(unicode: false),
                        DetailsText = c.String(unicode: false),
                        DetailsSectionHeader = c.String(unicode: false),
                        ListHeader = c.String(unicode: false),
                        ListDescription = c.String(unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        SortingNumber = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                        DetailsImageFile_Id = c.Int(),
                        Metadata_Id = c.Int(),
                        ThumbnailImageFile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFiles", t => t.DetailsImageFile_Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .ForeignKey("dbo.ImageFiles", t => t.ThumbnailImageFile_Id)
                .Index(t => t.DetailsImageFile_Id)
                .Index(t => t.Metadata_Id)
                .Index(t => t.ThumbnailImageFile_Id);
            
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bytes = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Metadatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(unicode: false),
                        Info = c.String(unicode: false),
                        Keywords = c.String(unicode: false),
                        Title = c.String(unicode: false),
                        H1 = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CallMes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(nullable: false, unicode: false),
                        RequesterName = c.String(unicode: false),
                        IsHandled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                        Instructor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.Instructor_Id)
                .Index(t => t.Instructor_Id);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        Patronymic = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Phone = c.String(nullable: false, unicode: false),
                        Phone2 = c.String(unicode: false),
                        UrlName = c.String(nullable: false, unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SortingNumber = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ThumbnailImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFiles", t => t.ThumbnailImage_Id)
                .Index(t => t.ThumbnailImage_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, unicode: false),
                        Name = c.String(nullable: false, unicode: false),
                        Phone = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                        Score = c.Double(nullable: false),
                        IsVisibleInList = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Instructor_Id = c.Int(nullable: false),
                        InstructorDetails_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.Instructor_Id, cascadeDelete: true)
                .ForeignKey("dbo.InstructorDetails", t => t.InstructorDetails_Id)
                .Index(t => t.Instructor_Id)
                .Index(t => t.InstructorDetails_Id);
            
            CreateTable(
                "dbo.InstructorDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Description = c.String(unicode: false),
                        VehicleDescriprion = c.String(unicode: false),
                        VehicleTitle = c.String(unicode: false),
                        VehicleSecondaryTitle = c.String(unicode: false),
                        FuelConsumption = c.String(unicode: false),
                        DetailsImage_Id = c.Int(),
                        Metadata_Id = c.Int(),
                        VehicleImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageFiles", t => t.DetailsImage_Id)
                .ForeignKey("dbo.Instructors", t => t.Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .ForeignKey("dbo.ImageFiles", t => t.VehicleImage_Id)
                .Index(t => t.Id)
                .Index(t => t.DetailsImage_Id)
                .Index(t => t.Metadata_Id)
                .Index(t => t.VehicleImage_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(nullable: false, unicode: false),
                        DetailText = c.String(unicode: false),
                        ListHeader = c.String(nullable: false, unicode: false),
                        ListDescription = c.String(unicode: false),
                        ListIcon = c.String(nullable: false, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        SortingNumber = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                        Metadata_Id = c.Int(),
                        ThumbnailImageFile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .ForeignKey("dbo.ImageFiles", t => t.ThumbnailImageFile_Id)
                .Index(t => t.Metadata_Id)
                .Index(t => t.ThumbnailImageFile_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VideoLessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(nullable: false, unicode: false),
                        YoutubeUrl = c.String(nullable: false, unicode: false),
                        Text = c.String(unicode: false),
                        ListHeader = c.String(unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        SortingNumber = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                        Metadata_Id = c.Int(),
                        ThumbnailImageFile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Metadatas", t => t.Metadata_Id)
                .ForeignKey("dbo.ImageFiles", t => t.ThumbnailImageFile_Id)
                .Index(t => t.Metadata_Id)
                .Index(t => t.ThumbnailImageFile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoLessons", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.VideoLessons", "Metadata_Id", "dbo.Metadatas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trainings", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Trainings", "Metadata_Id", "dbo.Metadatas");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CallMes", "Instructor_Id", "dbo.Instructors");
            DropForeignKey("dbo.Instructors", "ThumbnailImage_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.InstructorDetails", "VehicleImage_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.InstructorDetails", "Metadata_Id", "dbo.Metadatas");
            DropForeignKey("dbo.InstructorDetails", "Id", "dbo.Instructors");
            DropForeignKey("dbo.InstructorDetails", "DetailsImage_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Comments", "InstructorDetails_Id", "dbo.InstructorDetails");
            DropForeignKey("dbo.Comments", "Instructor_Id", "dbo.Instructors");
            DropForeignKey("dbo.Blogs", "ThumbnailImageFile_Id", "dbo.ImageFiles");
            DropForeignKey("dbo.Blogs", "Metadata_Id", "dbo.Metadatas");
            DropForeignKey("dbo.Blogs", "DetailsImageFile_Id", "dbo.ImageFiles");
            DropIndex("dbo.VideoLessons", new[] { "ThumbnailImageFile_Id" });
            DropIndex("dbo.VideoLessons", new[] { "Metadata_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Trainings", new[] { "ThumbnailImageFile_Id" });
            DropIndex("dbo.Trainings", new[] { "Metadata_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.InstructorDetails", new[] { "VehicleImage_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "Metadata_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "DetailsImage_Id" });
            DropIndex("dbo.InstructorDetails", new[] { "Id" });
            DropIndex("dbo.Comments", new[] { "InstructorDetails_Id" });
            DropIndex("dbo.Comments", new[] { "Instructor_Id" });
            DropIndex("dbo.Instructors", new[] { "ThumbnailImage_Id" });
            DropIndex("dbo.CallMes", new[] { "Instructor_Id" });
            DropIndex("dbo.Blogs", new[] { "ThumbnailImageFile_Id" });
            DropIndex("dbo.Blogs", new[] { "Metadata_Id" });
            DropIndex("dbo.Blogs", new[] { "DetailsImageFile_Id" });
            DropTable("dbo.VideoLessons");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Trainings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.InstructorDetails");
            DropTable("dbo.Comments");
            DropTable("dbo.Instructors");
            DropTable("dbo.CallMes");
            DropTable("dbo.Metadatas");
            DropTable("dbo.ImageFiles");
            DropTable("dbo.Blogs");
        }
    }
}
