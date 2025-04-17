using DevIONETEC.Business.Models;
using System;
using System.Runtime.ConstrainedExecution;

namespace DevIONETEC.Business.Models
{
    public class Endereco : Entity
    {
        public Guid ClienteId { get; private set; }
        public string Bairro { get; private set; }
        public string Municipio { get; private set; }
        public string Provincia { get; private set; }

        /* EF Relations UM PARA UM COM Cliente  */
        public Cliente Cliente { get; private set; }
    }
}