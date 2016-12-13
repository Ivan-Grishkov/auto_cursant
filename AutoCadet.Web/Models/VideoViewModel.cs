using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoCadet.Domain.Entities;

namespace AutoCadet.Models
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UrlName { get; set; }
        [Required]
        public string YoutubeUrl { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public string ListHeader { get; set; }
        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public Metadata Metadata { get; set; }
        public string ThumbnailImageName { get; set; }
    }
}