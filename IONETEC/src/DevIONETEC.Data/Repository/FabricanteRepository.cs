using DevIONETEC.Business.Models;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Data.Repository
{
    public class FabricanteRepository : Repository<Fabricante>, IFabricanteRepository
    {
        public FabricanteRepository(IonetecDbContext context) : base(context) { }

        public async Task<Fabricante> ObterFabricanteProdutos(Guid id)
        {
            return await Db.Fabricantes.AsNoTracking()
               .Include(c => c.Produtos)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
