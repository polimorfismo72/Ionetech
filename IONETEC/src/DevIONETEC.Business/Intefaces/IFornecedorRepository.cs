using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Business.Intefaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        /* 1 Fornecedor : N Produtos
       * Obter um determinado Fornecedor com os seus Produtos */
        Task<Fornecedor> ObterFornecedor(Guid id);
        Task<Fornecedor> ObterFornecedorProdutos(Guid id);

    }
}
