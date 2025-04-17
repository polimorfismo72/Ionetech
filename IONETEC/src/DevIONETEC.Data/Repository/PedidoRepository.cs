using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {   
        public PedidoRepository(IonetecDbContext context) : base(context){ }

     
        public async Task<Pedido> ObterPedido(Guid id)
        {
            return await Db.Pedidos.AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        
       
        public async Task<Pedido> ObterPedidoClientes(Guid id)
        {
            return await Db.Pedidos.AsNoTracking()
               .Include(c => c.Cliente)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Pedido> ObterPedidoPedidoItems(Guid id)
        {
            return await Db.Pedidos.AsNoTracking().Include(pi => pi.PedidoItems)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Pedido> ObterPedidoPeloItems(Guid pedidoId)
        {
            return await Db.Pedidos.AsNoTracking().Include(pi => pi.PedidoItems)
                 .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }
        public async Task<Pedido> ObterPedidoPedidoItemsCliente(Guid id)
         {
            return await Db.Pedidos.AsNoTracking()
                .Include(pi => pi.PedidoItems)
                .Include(c => c.Cliente)
                  .FirstOrDefaultAsync(p => p.Id == id);
         }
        public async Task<IEnumerable<Pedido>> ObterPedidoPorPedidoItems(Guid pedidoId)
        {
            return await Buscar(p => p.Id == pedidoId);
        }
        public async Task<IEnumerable<Pedido>> ObterPedidosClientes()
        {
            return await Db.Pedidos.AsNoTracking().Include(c => c.Cliente)
                .OrderBy(p => p.Codigo).ToListAsync();
        }
       
        public async Task<IEnumerable<Pedido>> ObterPedidosPorCliente(Guid clienteId)
        {
            return await Buscar(p => p.ClienteId == clienteId);
        }
    }
}
