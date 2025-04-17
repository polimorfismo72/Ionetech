namespace DevIONETEC.Business.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations  Lado UMA Categoria PARA MUITOS Produtos */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}