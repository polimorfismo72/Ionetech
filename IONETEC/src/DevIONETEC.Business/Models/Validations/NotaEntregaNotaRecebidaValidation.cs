using FluentValidation;

namespace DevIONETEC.Business.Models.Validations
{
    public class NotaEntregaNotaRecebidaValidation : AbstractValidator<NotaEntregaNotaRecebida>
    {
        public NotaEntregaNotaRecebidaValidation()
        {
            RuleFor(c => c.Emitente)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Destinatario)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}