﻿using System;
using System.ComponentModel.DataAnnotations;
using AutoCadet.Domain.Entities;

namespace AutoCadet.Models
{
    public class VideoLessonViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UrlName { get; set; }
        [Required]
        public string YoutubeUrl { get; set; }
        public string Text { get; set; }
        public string ListHeader { get; set; }
        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public Metadata Metadata { get; set; }
        public byte[] ThumbnailImageFile { get; set; }
    }
}