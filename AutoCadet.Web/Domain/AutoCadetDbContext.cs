using System.Data.Entity;
using AutoCadet.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AutoCadet.Domain
{
    public class AutoCadetDbContext : IdentityDbContext<ApplicationUser>
    {
        public AutoCadetDbContext()
            : base("AutoCadetDb", false)
        {
        }


        public IDbSet<Instructor> Instructors { get; set; }
        public IDbSet<InstructorDetails> InstructorDetails { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Metadata> Metadatas { get; set; }
        public IDbSet<CallMe> CallMes { get; set; }
        public IDbSet<Video> Video { get; set; }
        public IDbSet<Obuchenie> Obuchenie { get; set; }
        public IDbSet<Blog> Blogs { get; set; }
        public IDbSet<ShareEvent> ShareEvents { get; set; }

        public static AutoCadetDbContext Create()
        {
            return new AutoCadetDbContext();
        }
    }
}
