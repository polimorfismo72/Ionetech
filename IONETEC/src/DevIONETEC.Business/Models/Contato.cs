
namespace DevIONETEC.Business.Models
{
    public class Contato : Entity
    {
        public Guid ClienteId { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        /* EF Relations UM PARA UM COM Cliente  */
        public Cliente Cliente { get; set; }

    }
}