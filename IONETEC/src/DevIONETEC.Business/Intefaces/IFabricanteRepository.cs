using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Business.Intefaces
{
    public interface IFabricanteRepository : IRepository<Fabricante>
    {
        /* 1 Categoria : N Produtos
         * Obter uma determinada Fabricante com os seus Produtos */
        Task<Fabricante> ObterFabricanteProdutos(Guid id);
    }
}
