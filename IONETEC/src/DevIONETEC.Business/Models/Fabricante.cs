namespace DevIONETEC.Business.Models
{
    public class Fabricante : Entity
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations  Lado UM */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}