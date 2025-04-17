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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IonetecDbContext context) : base(context) { }

        public async Task<Categoria> ObterCategoriaProdutos(Guid id)
        {
            return await Db.Categorias.AsNoTracking()
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
