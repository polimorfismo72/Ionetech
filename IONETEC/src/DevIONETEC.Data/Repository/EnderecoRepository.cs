using DevIONETEC.Business.Models;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(IonetecDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorCliente(Guid clienteId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }
    }
}
