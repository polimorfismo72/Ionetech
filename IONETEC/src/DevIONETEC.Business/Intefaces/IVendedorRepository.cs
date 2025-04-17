using DevIONETEC.Business.Models;

namespace DevIONETEC.Business.Intefaces
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<Vendedor> ObterVendedorPedidoItem(Guid id);
        Task<Vendedor> ObterVendedorPeloNome(string nome);
    }
}
