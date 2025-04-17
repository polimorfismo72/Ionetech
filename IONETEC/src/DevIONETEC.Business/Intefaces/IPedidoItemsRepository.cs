using DevIONETEC.Business.Models;

namespace DevIONETEC.Business.Intefaces
{
    public interface IPedidoItemsRepository : IRepository<PedidoItem>
    {
        Task<PedidoItem> AdicionarAoCarrinho(PedidoItem pedidoItem);
        //--------------------------- Produto --------------------------------------
        /* N PedidoItems : 1 Produto (Varios PedidoItems estão associados a um Produto)
      * Obter um determinado PedidoItem com os seus Produtos */
        Task<IEnumerable<PedidoItem>> ObterPedidoItemsPorProduto(Guid produtoId);
        Task<IEnumerable<PedidoItem>> ObterPedidoItemsPorPedido(Guid pedidoId);
        Task<PedidoItem> ObterPedidoItemsPeloPedido(Guid pedidoId);
        /* N PedidoItems : 1 Produto (Varios PedidoItems estão associados a um Produto)
         * Obter PedidosItems com os seus Produtos */
        Task<IEnumerable<PedidoItem>> ObterPedidosItemsProdutos();
        
        /* N PedidoItems : 1 Produto (Varios PedidoItems estão associados a um Produto)
         * Obter um determinado PedidoItem com os seu Produto */
        Task<PedidoItem> ObterPedidoItemProduto(Guid id);
        Task<PedidoItem> ObterProdutoPorId(Guid id);

        //--------------------------- Vendedor --------------------------------------
        /* N PedidoItems : 1 Vendedor (Varios PedidoItems estão associados a um Vendedor)
         * Obter um determinado PedidoItem com os seus Vendedores */
        Task<IEnumerable<PedidoItem>> ObterPedidoItemsPorVendedor(Guid vendedorId);
        /* N PedidoItems : 1 Vendedor (Varios PedidoItems estão associados a um Vendedor)
         * Obter PedidosItems com os seus Vendedores */
        Task<IEnumerable<PedidoItem>> ObterPedidosItemsVendedores();
        /* N PedidoItems : 1 Vendedor (Varios PedidoItems estão associados a um Vendedor)
        * Obter um determinado PedidoItem com os seu Vendedor */
        Task<PedidoItem> ObterPedidoItemVendedor(Guid id);
    }
}
