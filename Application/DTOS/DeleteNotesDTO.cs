﻿
using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class DeleteNotesDTO
    {
        [Required]
        public int noteId {  get; set; }

    }
}
