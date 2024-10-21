using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        [Required]
        public string Titel { get; set; } = string.Empty;
        [Required]
        public string Task { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Required]
        public string State { get; set; } = "In progres";

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        //One to Many
        //public string UserId { get; set; } = string.Empty; will add soon!!!!!!
    }
}