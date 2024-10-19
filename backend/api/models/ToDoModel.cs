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
        public string Titel { get; set; } = string.Empty;
        public string Task { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string State { get; set; } = "In progres";

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        //One to Many
        //public string UserId { get; set; } = string.Empty; will add soon!!!!!!
    }
}