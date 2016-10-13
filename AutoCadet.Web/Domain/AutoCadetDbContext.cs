using System.Collections.Generic;
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
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<ImageFile> ImageFiles { get; set; }
        public IDbSet<Metadata> Metadatas { get; set; }

        public static AutoCadetDbContext Create()
        {
            return new AutoCadetDbContext();
        }
    }
}
