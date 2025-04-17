using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Business.Models.Validations.Documentos;
using DevIONETEC.Business.Services;
using FluentValidation;

namespace DevIONETEC.Business.Models.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


            When(c => c.TipoCliente == TipoCliente.PessoaFisica, () =>
            {
                //  RuleFor(c => c.Nif.Length).Equal(PfisicaValidacao.PessoaFisica)
                //    .WithMessage("O campo Nif precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                //RuleFor(c => PfisicaValidacao.Validar(c.Nif)).Equal(true)
                //    .WithMessage("O Nif fornecido é inválido.");
                 RuleFor(c => c.Nif)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(10, 14).WithMessage("O campo Pessoa Física precisa ter entre {MinLength} e {MaxLength} caracteres");

            });

            When(c => c.TipoCliente == TipoCliente.PessoaJuridica, () =>
            {
                RuleFor(c => c.Nif)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(8, 10).WithMessage("O campo Pessoa Jurídica precisa ter entre {MinLength} e {MaxLength} caracteres");
            });

        }
    }
}