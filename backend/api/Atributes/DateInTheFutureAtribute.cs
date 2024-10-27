using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Atributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateInTheFutureAtribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var Date = value as DateTime?;
            var memberNames = new List<string>() { context.MemberName };

            if (Date != null)
            {
                if (Date.Value.Date < DateTime.UtcNow.Date)
                {
                    return new ValidationResult("This must be a date in the future", memberNames);
                }
            }

            return ValidationResult.Success;
        }
    }
}