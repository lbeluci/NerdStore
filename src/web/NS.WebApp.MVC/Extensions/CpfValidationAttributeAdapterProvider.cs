using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace NS.WebApp.MVC.Extensions
{
    public class CpfValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _provider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is CpfAttribute cpfAttribute)
            {
                return new CpfAttributeAdapter(cpfAttribute, stringLocalizer);
            }

            return _provider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}