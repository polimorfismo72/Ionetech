using System;
using System.Threading.Tasks;
using DevIONETEC.Business.Models;

namespace DevIONETEC.Business.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}