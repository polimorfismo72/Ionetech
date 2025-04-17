
namespace DevIONETEC.Business.Models
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get;  set; }
        public Guid ProdutoId { get;  set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get;  set; }
        public decimal ValorUnitario { get;  set; }
        public string NumeroDeSerie { get; set; }
        public Guid VendedorId { get;  set; }

        /* EF Relations  Lado MUITO */
        public Produto Produto { get; set; }
        public Pedido Pedido { get; set; }
        public Vendedor Vendedor { get; set; }
    }
}