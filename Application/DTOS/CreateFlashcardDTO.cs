﻿

namespace Application.DTOS
{
    public class CreateFlashcardDTO
    {
        public string content { get; set; } = string.Empty;

        public bool isPublic { get; set; } = false;

        public string image_url { get; set; } = string.Empty;
    }
}
