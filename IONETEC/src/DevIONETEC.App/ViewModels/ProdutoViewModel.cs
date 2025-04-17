using DevIONETEC.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIONETEC.App.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Fabricante")]
        public Guid FabricanteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }

        [ScaffoldColumn(false)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Valor da Compra")]
        public decimal ValorCompra { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Valor da Venda")]
        public decimal ValorVenda { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Estoque")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Filial { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
        public FabricanteViewModel Fabricante { get; set; }
        public CategoriaViewModel Categoria { get; set; }

        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
        public IEnumerable<FabricanteViewModel> Fabricantes { get; set; }
        public IEnumerable<CategoriaViewModel> Categorias { get; set; }

        public IEnumerable<PedidoItemViewModel> PedidoItems { get; set; }

    }
}