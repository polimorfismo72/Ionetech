using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Business.Intefaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        /* 1 Categoria : N Produtos
         * Obter uma determinada Categoria com os seus Produtos */
        Task<Categoria> ObterCategoriaProdutos(Guid id);
    }
}
