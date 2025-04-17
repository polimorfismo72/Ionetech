using DevIONETEC.Business.Models;
using DevIONETEC.Business.DomainException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIONETEC.App.ViewModels
{
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter {2} caracteres  no mínimo", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(16, ErrorMessage = "O campo {0} precisa ter {2} caracteres  no mínimo", MinimumLength = 2)]
        public string Documento { get; set; }

        [DisplayName("Tipo")]
        public int TipoFornecedor { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(16, ErrorMessage = "O campo {0} precisa ter entre {9} caracteres  no mínimo", MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter {2} caracteres no mínimo", MinimumLength = 2)]
        public string Endereco { get;  set; }
        
        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public IEnumerable<FabricanteViewModel> Fabricantes { get; set; }
        public IEnumerable<CategoriaViewModel> Categorias { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }

    }
}