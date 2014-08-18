namespace Mvczin.Web.Models
{
    using Mvczin.Web.Mvc.Validation;
    using System.ComponentModel.DataAnnotations;

    public class ValidationForm
    {
        [Display(Name = "CNPJ")]
        [Cnpj(ErrorMessage = "O valor '{0}' é inválido para CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "CPF")]
        [Cpf(ErrorMessage = "O valor '{0}' é inválido para CPF")]
        public string Cpf { get; set; }
    }
}