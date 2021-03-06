﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoCadet.Domain;

namespace AutoCadet.Services.Impl
{
    public class SiteMapService: ISiteMapService
    {
        private const string BaseUrl = "http://uroki-vozhdeniya.by/";
        private const string ActionInstructors = "instructors";
        private const string ActionVideo = "video";
        private const string ActionObuchenie = "obuchenie";
        private const string ActionBlog = "blog";
        private readonly string _pathToSiteMap = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/sitemap_base.xml");
        private readonly AutoCadetDbContext _autoCadetDbContext;
        private readonly XNamespace _nameSpace = @"http://www.sitemaps.org/schemas/sitemap/0.9";

        public SiteMapService(AutoCadetDbContext autoCadetDbContext)
        {
            _autoCadetDbContext = autoCadetDbContext;
        }

        public async Task<string> GenetateSiteMapAsync()
        {
            XDocument sitemap = XDocument.Load(_pathToSiteMap, LoadOptions.PreserveWhitespace);
            XElement root = sitemap.Root;
            if (root == null)
            {
                return null;
            }

            var instructorsUrls = await _autoCadetDbContext.Instructors.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionInstructors, instructorsUrls);

            var videoUrls = await _autoCadetDbContext.Video.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionVideo, videoUrls);

            var obuchenieUrls = await _autoCadetDbContext.Obuchenie.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionObuchenie, obuchenieUrls);

            var blogUrls = await _autoCadetDbContext.Blogs.Select(x => x.UrlName).ToListAsync().ConfigureAwait(false);
            AddNewNodes(root, ActionBlog, blogUrls);

            return $"<?xml version=\"1.0\" encoding=\"UTF-8\"?> {sitemap}";
        }

        private void AddNewNodes(XElement root, string action, List<string> prettyUrls)
        {
            if (prettyUrls != null && prettyUrls.Any())
            {
                foreach (var prettyUrl in prettyUrls)
                {
                    var location = GetUrlForAction(action, prettyUrl);
                    var locElement = new XElement(_nameSpace + "loc", location);
                    var changefreqElement = new XElement(_nameSpace + "changefreq", "monthly");
                    var urlElement = new XElement(_nameSpace + "url");
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