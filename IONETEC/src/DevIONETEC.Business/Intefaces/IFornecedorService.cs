using System;
using System.Threading.Tasks;
using DevIONETEC.Business.Models;

namespace DevIONETEC.Business.Intefaces
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task Remover(Guid id);
    }
}