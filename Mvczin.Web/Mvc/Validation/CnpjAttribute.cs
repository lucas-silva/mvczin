namespace Mvczin.Web.Mvc.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CnpjAttribute : ValidationAttribute, IClientValidatable
    {
        public CnpjAttribute()
        {
            this.ErrorMessage = "The value {0} is invalid for CNPJ";
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueValidLength = 14;
                var maskChars = new[] { ".", "-", "/" };
                var multipliersForFirstDigit = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multipliersForSecondDigit = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                var mod11 = new Mod11();
                var isValid = mod11.IsValid(value.ToString(), valueValidLength, maskChars, multipliersForFirstDigit, multipliersForSecondDigit);

                if (!isValid)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "cnpj",
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName)
            };

            return new List<ModelClientValidationRule> { modelClientValidationRule };
        }
    }
}