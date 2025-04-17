using DevIONETEC.Business.Models;

namespace DevIONETEC.Business.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Nif { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public Endereco Endereco { get; set; }
        public Contato Contato { get; set; }
        public bool Ativo { get; set; }
       // protected Cliente() { }

        /* EF Relations  Lado UM  */
        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
