using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Atributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MatchAtribute : ValidationAttribute
    {
        private string _ComparisonProperty;

        public MatchAtribute(string ComparisonProperty)
        {
            _ComparisonProperty = ComparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_ComparisonProperty);

            if(property == null)
            {
                return new ValidationResult($"{property} not found. Please make sure you entered it.");
            }

            var RepeatedPassword = property.GetValue(validationContext.ObjectInstance);
            
            if(!object.Equals(value, RepeatedPassword))
            {
                return new ValidationResult(ErrorMessage ?? "Reapeted password does not match the password, please make sure you entered same password 2 times");
            }

            return ValidationResult.Success;
        }
    }
}