using NS.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace NS.WebApp.MVC.Extensions
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Cpf.Validate(value.ToString()) ? ValidationResult.Success : new ValidationResult("CPF number is invalid");
        }
    }
}