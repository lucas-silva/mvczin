namespace Mvczin.Web.Mvc.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public class CpfAttribute : ValidationAttribute, IClientValidatable
    {
        public CpfAttribute()
        {
            this.ErrorMessage = "The value {0} is invalid for CPF";
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                var cpf = ThreatCpf(value.ToString());

                if (IsInvalidLength(cpf) ||
                    IsInvalidSequence(cpf) ||
                    IsNotNumbersOnly(cpf) ||
                    IsInvalidCpf(cpf))
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return null;
        }

        private static string ThreatCpf(
            string cpf)
        {
            return cpf.Trim().Replace(".", "").Replace("-", "");
        }

        private static int GetFirstDigit(
            string cpf)
        {
            var multipliers = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpfToWork = cpf.Substring(0, 9);

            var sum = multipliers
                .Select((d, i) => new
                {
                    Value = int.Parse(cpfToWork[i].ToString()),
                    Multiplier = multipliers[i]
                })
                .Sum(d => d.Value * d.Multiplier);

            var rest = sum % 11;

            var firstDigit = rest < 2 ? 0 : 11 - rest;

            return firstDigit;
        }

        private static int GetSecondDigit(
            string cpf,
            int firstDigit)
        {
            var multipliers = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpfToWork = string.Concat(cpf.Substring(0, 9), firstDigit);

            var sum = multipliers
                .Select((d, i) => new
                {
                    Value = int.Parse(cpfToWork[i].ToString()),
                    Multipler = d
                })
                .Sum(d => d.Value * d.Multipler);

            var rest = sum % 11;

            var secondDigit = rest < 2 ? 0 : 11 - rest;

            return secondDigit;
        }

        private static bool IsInvalidLength(
            string cpf)
        {
            return cpf.Length != 11;
        }

        private bool IsInvalidSequence(string cpf)
        {
            if (cpf == "00000000000" ||
                cpf == "11111111111" ||
                cpf == "22222222222" ||
                cpf == "33333333333" ||
                cpf == "44444444444" ||
                cpf == "55555555555" ||
                cpf == "66666666666" ||
                cpf == "77777777777" ||
                cpf == "88888888888" ||
                cpf == "99999999999")
            {
                return false;
            }

            return true;
        }

        private static bool IsNotNumbersOnly(
            string cpf)
        {
            return !Regex.IsMatch(cpf, @"\d+");
        }

        private static bool IsInvalidCpf(
            string cpf)
        {
            var firstDigit = GetFirstDigit(cpf);

            var secondDigit = GetSecondDigit(cpf, firstDigit);

            var expectedSufix = string.Concat(firstDigit, secondDigit);

            var isInvalid = !cpf.EndsWith(expectedSufix);

            return isInvalid;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "cpf",
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName)
            };

            return new List<ModelClientValidationRule> { modelClientValidationRule };
        }
    }
}