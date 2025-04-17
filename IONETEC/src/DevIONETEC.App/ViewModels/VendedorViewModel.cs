using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIONETEC.App.ViewModels
{
    public class VendedorViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        public IEnumerable<PedidoItemViewModel> PedidoItems { get; set; }
    }
}
