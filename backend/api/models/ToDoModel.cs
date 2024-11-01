using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("ToDos")]
    public class ToDoModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "Titel can not have more then 150 characters")]
        public string Titel { get; set; } = string.Empty;
        [Required]
        [MaxLength(6000, ErrorMessage = "The Task cannot have more then 6000 characters")]
        public string Task { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Required]
        public string State { get; set; } = string.Empty;

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<Portfolio>? Portfolios { get; set; }
    }
}