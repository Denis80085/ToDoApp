using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ToDoDtos
{
    public class CreateToDoDto
    {
        [Required]
        public string Titel { get; set; } = string.Empty;
        [Required]
        public string Task { get; set; } = string.Empty;
        public string State { get; set; } = "In progres";

    }
}