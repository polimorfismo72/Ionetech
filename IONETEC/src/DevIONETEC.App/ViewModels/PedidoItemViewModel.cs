using DevIONETEC.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevIONETEC.App.ViewModels
{
    public class PedidoItemViewModel
    {
        public decimal CarrinhoCompraTotal { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Produto")]
        public Guid ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Pedido")]
        public Guid PedidoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Vendedor")]
        public Guid VendedorId { get; set; }
        public int Quantidade { get; set; }

        [DisplayName("Produto")]
        public string NomeProduto { get; set; }

        [DisplayName("Valor")]
        public decimal ValorUnitario { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Número de Série")]
        public string NumeroDeSerie { get; set; }           
        public ProdutoViewModel Produto { get; set; }
        public PedidoViewModel Pedido { get; set; }
        public VendedorViewModel Vendedor { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public IEnumerable<ClienteViewModel> Clientes { get; set; }
        //public IEnumerable<VendedorViewModel> Vendedores { get; set; }

    }
}