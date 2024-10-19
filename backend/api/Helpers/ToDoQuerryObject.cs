using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class ToDoQuerryObject
    {
        public bool WithDeadline  { get; set; } = false;

        public DateOnly? FromDate { get; set; } = null;

        public DateOnly? ToDate { get; set; } = null;
    }
}