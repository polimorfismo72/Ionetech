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
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(IonetecDbContext context) : base(context) { }

        public async Task<Vendedor> ObterVendedorPedidoItem(Guid id)
        {
            return await Db.Vendedores.AsNoTracking()
                 .Include(p => p.PedidoItems)
                 .FirstOrDefaultAsync(v => v.Id == id);
        }
        public async Task<Vendedor> ObterVendedorPeloNome(string nome)
        {
            return await Db.Vendedores.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Email == nome);
        }
    }
}
