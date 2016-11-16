using System.Threading.Tasks;

namespace AutoCadet.Services
{
    public interface ISiteMapService
    {
        Task<string> GenetateSiteMapAsync();
    }
}
