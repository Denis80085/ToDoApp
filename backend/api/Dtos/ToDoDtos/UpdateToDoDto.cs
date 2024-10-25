using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Atributes;
using Microsoft.AspNetCore.Identity;

namespace api.Dtos.ToDoDtos
{
    public class UpdateToDoDto
    {
        [Required]
        [MaxLength(150, ErrorMessage = "Titel can not have more then 150 characters")]
        public string Titel { get; set; }
        
        [Required]
        [MaxLength(6000, ErrorMessage = "The Task cannot have more then 6000 characters")]
        public string Task { get; set; }
        
        [Required]
        public string State { get; set; }
        
    }
}