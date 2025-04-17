using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(IonetecDbContext context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedor(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutos(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
              .Include(c => c.Produtos)
              .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
