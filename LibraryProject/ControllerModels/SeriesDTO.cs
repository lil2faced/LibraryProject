﻿using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class SeriesDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public List<BookDTOChild> Books { get; set; } = new List<BookDTOChild>();
    }
}
