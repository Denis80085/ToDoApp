using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Atributes;

namespace api.Helpers
{
    public class ToDoQuerryObject
    {
        public bool WithDeadline  { get; set; } = false;
     
        [DateInTheFutureAtribute]
        public DateTime? FromDate { get; set; } = null;

        [DateInTheFutureAtribute]
        public DateTime? ToDate { get; set; } = null;
    }
}