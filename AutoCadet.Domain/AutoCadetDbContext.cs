using System.Data.Entity;

namespace AutoCadet.Domain
{
    class AutoCadetDbContext:DbContext
    {
        
        public AutoCadetDbContext():base("AutoCadetDb")
        {
        }
    }
}
