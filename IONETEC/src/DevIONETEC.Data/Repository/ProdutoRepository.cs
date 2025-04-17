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
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IonetecDbContext context) : base(context) { }
        public async Task<IEnumerable<Produto>> ObterProdutos()
        {
            return await Db.Produtos.AsNoTracking()
                .Include(fa => fa.Fabricante)
                .OrderBy(n => n.Nome).ToListAsync();
        }
        public async Task<Produto> ObterProdutoCategoria(Guid id)
        {
            return await Db.Produtos.AsNoTracking().Include(c => c.Categoria)
              .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterProdutoFabricante(Guid id)
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Fabricante)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await Db.Produtos.AsNoTracking().Include(c => c.Fornecedor)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterNomeProduto(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterProdutoFornecedorFabricanteCategoria(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(fo => fo.Fornecedor)
                .Include(fa => fa.Fabricante)
                .Include(c => c.Categoria)
           .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterProdutoFornecedorFabricanteCategoria1(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(fo => fo.Fornecedor)
                .Include(fa => fa.Fabricante)
                .Include(c => c.Categoria)
           .FirstOrDefaultAsync(p => p.FornecedorId == id);
        }
        public async Task<Produto> ObterProdutoPrecoValorVenda(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(c => c.ValorVenda)
           .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterProdutoPedidoItems(Guid id)
        {
            return await Db.Produtos.AsNoTracking().Include(p => p.PedidoItems)
             .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Produto>> ObterProdutoPorCategoria(Guid categoriaId)
        {
            return await Buscar(p => p.CategoriaId == categoriaId);
        }
        public async Task<IEnumerable<Produto>> ObterProdutosCategorias()
        {
            return await Db.Produtos.AsNoTracking().Include(c => c.Categoria)
                .OrderBy(p => p.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Produto>> ObterProdutosFabricantes()
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Fabricante)
                 .OrderBy(p => p.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                  .OrderBy(p => p.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Produto>> ObterProdutosFornecedoresFabricantesCategorias()
        {
                return await Db.Produtos.AsNoTracking()
                .Include(f => f.Fornecedor)
                .Include(f => f.Fabricante)
                .Include(f => f.Categoria)
                    .OrderBy(p => p.Nome).ToListAsync();
          
        }
        public async Task<IEnumerable<Produto>> ObterProdutosPorFabricante(Guid fabricanteId)
        {
            return await Buscar(p => p.FabricanteId == fabricanteId);
        }
        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
        public async Task<Produto> ObterProdutoPorId(Guid id)
        {
            return await Db.Produtos
             .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterPrecoProdutoPorId(Guid id)
        {
            return await Db.Produtos
                .Include(v=> v.ValorVenda)
             .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
