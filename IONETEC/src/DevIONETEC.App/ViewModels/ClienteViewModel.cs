using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevIONETEC.App.ViewModels
{
    public class ClienteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("NIF")]
        public string Nif { get; set; }

        [DisplayName("Tipo")]
        public int TipoCliente { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public ContatoViewModel Contato { get; set; }
        public PedidoViewModel Pedido { get; set; }
        public PedidoItemViewModel PedidoItem { get; set; }
        public ProdutoViewModel Produto { get; set; }

        [NotMapped]
        public Guid ProdutoId { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        
       

        public IEnumerable<PedidoViewModel> Pedidos { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public IEnumerable<PedidoItemViewModel> PedidoItens { get; set; }
        public List<ClienteViewModel> RetornarListaCliente()
        {
            return new List<ClienteViewModel>();
        }

    }
}
