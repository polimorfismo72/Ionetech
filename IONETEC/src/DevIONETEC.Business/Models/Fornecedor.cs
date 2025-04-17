namespace DevIONETEC.Business.Models
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        //public Email Email { get; private set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get;  set; }
        public bool Ativo { get; set; }

        /* EF Relations  Lado UM */
        public IEnumerable<Produto> Produtos { get; set; }

    }
}