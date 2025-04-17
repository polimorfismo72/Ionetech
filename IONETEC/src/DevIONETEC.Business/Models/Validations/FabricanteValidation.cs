using FluentValidation;

namespace DevIONETEC.Business.Models.Validations
{
    public class FabricanteValidation : AbstractValidator<Fabricante>
    {
        public FabricanteValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}