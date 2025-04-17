namespace DevIONETEC.Business.Models
{
    public class Produto : Entity
    {
       // public Produto() { }
        public Guid FornecedorId { get; set; }
        public Guid FabricanteId { get; set; }
        public Guid CategoriaId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string Filial { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations  Lado MUITO */
        public Fornecedor Fornecedor { get; set; }
        public Fabricante Fabricante { get; set; }
        public Categoria Categoria { get; set; }

        /* EF Relations  Lado UM */
        public IEnumerable<PedidoItem> PedidoItems { get; set; }

    }
}