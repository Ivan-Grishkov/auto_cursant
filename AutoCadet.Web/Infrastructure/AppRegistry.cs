using System.Data.Entity;
using System.Web;
using AutoCadet.Domain;
using AutoCadet.Models.Mapper;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StructureMap;

namespace AutoCadet.Infrastructure
{
    public class AppRegistry:Registry
    {
        public AppRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory();
                scan.IncludeNamespace("AutoCadet");
                scan.WithDefaultConventions();
            });

            //AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoCadetMapperProfile>();
            });
            For<IMapper>().Use(context => config.CreateMapper()).Singleton();
            For<IdentityDbContext<ApplicationUser>>().Use<AutoCadetDbContext>().Singleton();

            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
            For<DbContext>().Use(x => x.GetInstance<AutoCadetDbContext>());
            For<IUserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
            For<UserManager<ApplicationUser>>().Use<ApplicationUserManager>();
            For<SignInManager<ApplicationUser, string>>().Use<ApplicationSignInManager>();
        }
    }
}