using System;
using System.Threading.Tasks;
using DevIONETEC.Business.Models;

namespace DevIONETEC.Business.Intefaces
{
    public interface IClienteService : IDisposable
    {
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
        Task AtualizarContato(Contato contato);
    }
}