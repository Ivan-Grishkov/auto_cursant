using Microsoft.AspNet.Identity.EntityFramework;

namespace AutoCadet.Domain
{
    public class AutoCadetDbContext : IdentityDbContext<ApplicationUser>
    {
        public AutoCadetDbContext()
            : base("AutoCadet", false)
        {
        }

        public static AutoCadetDbContext Create()
        {
            return new AutoCadetDbContext();
        }
    }
}
