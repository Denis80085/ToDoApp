using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class DeadLineModel
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        //one to one
        public int ToDoId { get; set; }
        public ToDoModel ToDo { get; set; }
    }
}