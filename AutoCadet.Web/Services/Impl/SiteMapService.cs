using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoCadet.Domain;

namespace AutoCadet.Services.Impl
{
    public class SiteMapService: ISiteMapService
    {
        private const string BaseUrl = "http://www.uroki-vozhdeniya.by/";
        private const string ActionInstructors = "instructors";
        private const string ActionVideoLessons = "videolessons";
        private const string ActionTraining = "training";
        private const string ActionBlog = "blog";
        private readonly string _pathToSiteMap = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/sitemap_base.xml");
        private readonly AutoCadetDbContext _autoCadetDbContext;

        public SiteMapService(AutoCadetDbContext autoCadetDbContext)
        {
            _autoCadetDbContext = autoCadetDbContext;
        }

        public async Task<string> GenetateSiteMapAsync()
        {
            XDocument sitemap = XDocument.Load(_pathToSiteMap);
            XElement root = sitemap.Root;
            if (root == null)
            {
                return null;
            }

            var instructorsUrls = await _autoCadetDbContext.Instructors.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionInstructors, instructorsUrls);

            var videoUrls = await _autoCadetDbContext.VideoLessons.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionVideoLessons, videoUrls);

            var trainingUrls = await _autoCadetDbContext.Trainings.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionTraining, trainingUrls);

            var blogUrls = await _autoCadetDbContext.Blogs.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionBlog, blogUrls);

            return sitemap.ToString();
        }

        private void AddNewNodes(XElement root, string action, List<string> prettyUrls)
        {
            if (prettyUrls != null && prettyUrls.Any())
            {
                foreach (var prettyUrl in prettyUrls)
                {
                    var location = GetUrlForAction(action, prettyUrl);
                    var locElement = new XElement("loc", location);
                    var changefreqElement = new XElement("changefreq", "monthly");
                    var urlElement = new XElement("url");
                    urlElement.Add(locElement);
                    urlElement.Add(changefreqElement);
                    root.Add(urlElement);
                }
            }
        }

        private string GetUrlForAction(string action, string prettyUrl)
        {
            return $"{BaseUrl}{action}/{prettyUrl}";
        }
    }
}