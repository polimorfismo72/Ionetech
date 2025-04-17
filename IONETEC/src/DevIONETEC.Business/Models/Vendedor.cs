namespace DevIONETEC.Business.Models
{
    public class Vendedor : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations side 1*/
        public IEnumerable<PedidoItem> PedidoItems { get; set; }
     
    }
}
