using DevIONETEC.Business.Models;
using DevIONETEC.Business.DomainException;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevIONETEC.App.ViewModels
{
    public class PedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Cliente")]
        public Guid ClienteId { get; set; }

        [ScaffoldColumn(false)]
        public int Codigo { get; set; }

        [NotMapped]
        public string Nif { get; set; }
        [NotMapped]
        public Guid ProdutoId { get; set; }

        [DisplayName("Desconto Percentual")]
        public decimal PercentualDesconto { get; set; }

        [DisplayName("Valor do Desconto")]
        public decimal ValorDesconto { get; set; }

        [DisplayName("VaTotal a Pagar")]
        public decimal ValorTotal { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        //[DisplayName("Número de Série")]
        //public string NumeroDeSerie { get; set; }

        [DisplayName("Tipo de Operação")]
        public int OperacaoPedidos { get; set; }

        [DisplayName("Situação do Pedido")]
        public int Situacao { get; set; }

        [DisplayName("Tipo de Pagamento")]
        public int TipoPagamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Código da Transação")]
        public string NumeroDeTransacaoDePagamento { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public ClienteViewModel Cliente { get; set; }
        public ContatoViewModel Contato { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public PedidoItemViewModel PedidoItem { get; set; }
        public ProdutoViewModel Produto { get; set; }
     

        public IEnumerable<ClienteViewModel> Clientes { get; set; }
        public IEnumerable<PedidoItemViewModel> PedidoItems { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }

    }
}