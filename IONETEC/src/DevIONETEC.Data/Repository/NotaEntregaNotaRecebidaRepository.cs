using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Data.Repository
{
    public class NotaEntregaNotaRecebidaRepository : Repository<NotaEntregaNotaRecebida>, INotaEntregaNotaRecebidaRepository
    {
        public NotaEntregaNotaRecebidaRepository(IonetecDbContext context) : base(context) { }

        public async Task<IEnumerable<NotaEntregaNotaRecebida>> ObterNotaEntregaNotaRecebidas()
        {
            return await Db.NotaEntregaNotaRecebidas.AsNoTracking()
                .OrderBy(n => n.TipoNota).ToListAsync();
        }
    }
}
