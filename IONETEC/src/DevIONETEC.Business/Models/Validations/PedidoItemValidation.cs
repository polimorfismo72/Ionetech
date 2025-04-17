using FluentValidation;

namespace DevIONETEC.Business.Models.Validations
{
    public class PedidoItemValidation : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidation()
        {
            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            //RuleFor(c => c.ProdutoNome)
            //    .NotEmpty()
            //    .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade miníma para o {item.Produto.Nome} é 1");

            //RuleFor(c => c.ValorUnitario)
            //    .GreaterThan(0)
            //    .WithMessage(item => $"O valor do {item.ProdutoNome} precisa ser maior que 0");
        }
    }
}
