using System;
using System.Collections.Generic;
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

        //One to One
        public int? DeadLineId { get; set; }
        public DeadLineModel? DeadLine { get; set; }  

        //One to Many
        //public string UserId { get; set; } = string.Empty; will add soon!!!!!!
    }
}