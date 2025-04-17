using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Business.Intefaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //--------------------------- Fornecedor --------------------------------------
        /* N Produto : 1 Fornecedor (Varios Produtos estão associados a um Fornecedor)
         * Obter um determinado Produto com os seus Fornecedores */
        Task<IEnumerable<Produto>> ObterProdutos();
        Task<Produto> ObterProdutoPrecoValorVenda(Guid id);
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        /* N Produto : 1 Fornecedor (Varios Produtos estão associados a um Fornecedor)
         * Obter Produtos com os seus Fornecedores */
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        /* N Produto : 1 Fornecedor (Varios Produtos estão associados a um Fornecedor)
         * Obter um determinado Produto com os seu Fornecedor */
        Task<Produto> ObterProdutoFornecedor(Guid id);
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<Produto> ObterPrecoProdutoPorId(Guid id);
        Task<Produto> ObterNomeProduto(Guid id);
        //--------------------------- Categoria --------------------------------------
        /* N Produto : 1 Categoria (Varios Produtos estão associados a uma Categoria)
         * Obter um determinado Produto com as suas Categorias */
        Task<IEnumerable<Produto>> ObterProdutoPorCategoria(Guid categoriaId);
        /* N Produto : 1 Categoria (Varios Produtos estão associados a uma Categoria)
         * Obter Produto com as suas Categorias */
        Task<IEnumerable<Produto>> ObterProdutosCategorias();
        /* N Produto : 1 Categoria (Varios Produtos estão associados a uma Categoria)
        * Obter um determinado Produto com a sua Categoria */
        Task<Produto> ObterProdutoCategoria(Guid id);

        //--------------------------- Fabricante --------------------------------------
        /* N Produtos : 1 Fabricante (Varios Produtos estão associados a um Fabricante)
         * Obter um determinado Produto com os seus Fabricantes */
        Task<IEnumerable<Produto>> ObterProdutosPorFabricante(Guid fabricanteId);
        /* N Produtos : 1 Fabricante (Varios Produtos estão associados a um Fabricante)
         * Obter PedidosItems com os seus Fabricantes */
        Task<IEnumerable<Produto>> ObterProdutosFabricantes();
        /* N Produtos : 1 Fabricante (Varios Produtos estão associados a um Fabricante)
        * Obter um determinado Produto com os seu Fabricante */
        Task<Produto> ObterProdutoFabricante(Guid id);

        //----------------------------- PedidoItems -------------------------------------
        /* 1 Pedido : N PedidoItems
        * Obter um determinado Produto com os seus PedidoItems */
        Task<Produto> ObterProdutoPedidoItems(Guid id);

        Task<Produto> ObterProdutoFornecedorFabricanteCategoria(Guid id);
        Task<Produto> ObterProdutoFornecedorFabricanteCategoria1(Guid id);
        Task<IEnumerable<Produto>>ObterProdutosFornecedoresFabricantesCategorias();
    }
}
