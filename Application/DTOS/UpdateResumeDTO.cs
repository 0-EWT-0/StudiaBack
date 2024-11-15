﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class UpdateResumeDTO
    {
        [Required]
        public int ResumeId { get; set; }

        [Required]
        public string content { get; set; } = string.Empty;

        public bool isPublic { get; set; } = false;

        public string image_url { get; set; } = string.Empty;
    }
}