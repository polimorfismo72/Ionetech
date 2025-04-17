namespace DevIONETEC.Business.Models
{
    public class NotaEntregaNotaRecebida : Entity
    {
        public int Codigo { get; set; }
        public string DocumentoPdf { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataEmissao { get; set; }
        public TipoNota TipoNota { get; set; }
        public string Emitente { get; set; }
        public string Destinatario { get; set; }

        public bool Ativo { get; set; }
    }
}
