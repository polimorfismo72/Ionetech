using DevIONETEC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Business.Intefaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {

        /*(Na relação 1:1) No caso Um Cliente Possui Um Endereço
         Visualizar um Cliente com os seu Endereço referido */
        Task<Cliente> ObterClienteEndereco(Guid id);
        /*(Na relação 1:1) No caso Um Cliente Possui Um Contato
         Visualizar um determinado  Cliente com os seu Contato referido */
        Task<Cliente> ObterClienteContato(Guid id);
        /* (Na relação 1:N) No caso Um Cliente Possui muitos Pedidos
         Visualizar um determinado Cliente com os seus Pedidos referidos */
        Task<Cliente> ObterClientePedidos(Guid id);
        /* Visualizar um determinado Cliente com os seus referidos
          Pedidos, Endereco e Contato  */
        Task<Cliente> ObterClientePedidosEnderecoContato(Guid id);

        Task<Cliente> ObterClienteEnderecoContato(Guid id);
        Task<Cliente> ObterClientes();
    }
}
