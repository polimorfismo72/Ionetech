using DevIONETEC.Business.Models;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Repository
{
    public class ContatoRepository : Repository<Contato>, IContatoRepository
    {
        public ContatoRepository(IonetecDbContext context) : base(context) { }
        public async Task<Contato> ObterContatoPorCliente(Guid clienteId)
        {
            return await Db.Contatos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }
    }
}
