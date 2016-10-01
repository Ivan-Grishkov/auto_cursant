using Microsoft.AspNet.Identity.EntityFramework;

namespace AutoCadet.Domain
{
    public class AutoCadetDbContext : IdentityDbContext<ApplicationUser>
    {
        public AutoCadetDbContext()
            : base("AutoCadetDb", false)
        {
        }

        public static AutoCadetDbContext Create()
        {
            return new AutoCadetDbContext();
        }
    }
}
