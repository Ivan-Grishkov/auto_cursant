using System.Data.Entity;
using AutoCadel.Domain.Entities;

namespace AutoCadel.Domain
{
    public class AutoCadetDbContext:DbContext
    {
        public AutoCadetDbContext()
            : base("AutoCadet")
        {
        }

        public IDbSet<Person> Persons { get; set; }
    }
}
