using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.App.ViewModels
{
    public class NotaEntregaNotaRecebidaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public int Codigo { get; set; }

        //[DisplayName("Documento PDF")]
       // public IFormFile DocPdf { get; set; }
        public string DocumentoPdf { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Data de Emissão")]
        public DateTime DataEmissao { get; set; }

        [DisplayName("Nota de Entrega?")]
        public int TipoNota { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Empresa que Emitiu")]
        public string Emitente { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Empresa Destinatária")]
        public string Destinatario { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
    }
}
