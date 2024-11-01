using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ToDoDtos
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Task { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string State { get; set; } = string.Empty;

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

    }
}