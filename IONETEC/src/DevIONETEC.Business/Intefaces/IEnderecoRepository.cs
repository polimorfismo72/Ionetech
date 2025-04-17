using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Business.Intefaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {

        // Obter um Endereço atraves do Fornecedor
        Task<Endereco> ObterEnderecoPorCliente(Guid clienteId);
    }
}
