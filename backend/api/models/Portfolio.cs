using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public int ToDoId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public AppUser? User { get; set; }
        public ToDoModel? ToDo { get; set; } 
    }
}