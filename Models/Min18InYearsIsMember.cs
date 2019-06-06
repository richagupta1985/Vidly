using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18InYearsIsMember:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == 1)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is Required.");

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Manager should be greater than 18 years");
            //return base.IsValid(value, validationContext);
        }
    }
}