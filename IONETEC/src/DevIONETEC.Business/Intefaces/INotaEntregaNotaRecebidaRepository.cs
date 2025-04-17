using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Business.Intefaces
{
    public interface INotaEntregaNotaRecebidaRepository : IRepository<NotaEntregaNotaRecebida>
    {
        Task<IEnumerable<NotaEntregaNotaRecebida>> ObterNotaEntregaNotaRecebidas();
    }
}
