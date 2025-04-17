using DevIONETEC.Business.Models;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(IonetecDbContext context) : base(context){ }
        public async Task<Cliente> ObterClientes()
        {
            return await Db.Clientes.AsNoTracking()
               .FirstOrDefaultAsync();
        }
        public async Task<Cliente> ObterClienteContato(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
               .Include(c => c.Contato)
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClienteEndereco(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
              .Include(c => c.Endereco)
              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClienteEnderecoContato(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
              .Include(c => c.Endereco)
              .Include(c => c.Contato)
              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClientePedidos(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
              .Include(c => c.Pedidos)
              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClientePedidosEnderecoContato(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
               .Include(c => c.Pedidos)
               .Include(c => c.Endereco)
               .Include(c => c.Contato)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
