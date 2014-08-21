namespace Mvczin.Web.Mvc.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
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
                var cnpj = ThreatCnpj(value.ToString());

                if (IsInvalidLength(cnpj) ||
                    IsInvalidSequence(cnpj) ||
                    IsNotNumbersOnly(cnpj) ||
                    IsInvalidCnpj(cnpj))
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return null;
        }

        private bool IsInvalidSequence(string cnpj)
        {
            if (cnpj == "00000000000000" ||
                cnpj == "11111111111111" ||
                cnpj == "22222222222222" ||
                cnpj == "33333333333333" ||
                cnpj == "44444444444444" ||
                cnpj == "55555555555555" ||
                cnpj == "66666666666666" ||
                cnpj == "77777777777777" ||
                cnpj == "88888888888888" ||
                cnpj == "99999999999999")
            {
                return false;
            }

            return true;
        }

        private static string ThreatCnpj(
            string cnpj)
        {
            return cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        }

        private static int GetFirstDigit(
            string cnpj)
        {
            var cnpjToWork = cnpj.Substring(0, 12);

            var multipliers = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var sum = multipliers
                .Select((d, i) => new
                {
                    Value = int.Parse(cnpjToWork[i].ToString()),
                    Multiplier = d
                })
                .Sum(d => d.Value * d.Multiplier);

            var rest = sum % 11;

            var firstDigit = rest < 2 ? 0 : 11 - rest;

            return firstDigit;
        }

        private static int GetSecondDigit(
            string cnpj,
            int firstDigit)
        {
            var cnpjToWork = string.Concat(cnpj.Substring(0, 12), firstDigit);

            var multipliers = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var sum = multipliers
                .Select((d, i) => new { Value = int.Parse(cnpjToWork[i].ToString()), Multiplier = d })
                .Sum(d => d.Value * d.Multiplier);

            var rest = sum % 11;

            var secondDigit = rest < 2 ? 0 : 11 - rest;

            return secondDigit;
        }

        private static bool IsInvalidLength(
            string cnpj)
        {
            return cnpj.Length != 14;
        }

        private static bool IsNotNumbersOnly(
            string cnpj)
        {
            return !Regex.IsMatch(cnpj, @"\d+");
        }

        private static bool IsInvalidCnpj(
            string cnpj)
        {
            var firstDigit = GetFirstDigit(cnpj);

            var secondDigit = GetSecondDigit(cnpj, firstDigit);

            var expectedSufix = string.Concat(firstDigit, secondDigit);

            var isInvalid = !cnpj.EndsWith(expectedSufix);

            return isInvalid;
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