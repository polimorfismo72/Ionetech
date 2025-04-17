using System;
using System.Threading.Tasks;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Business.Models.Validations;

namespace DevIONETEC.Business.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IContatoRepository _contatoRepository;
        #endregion
        
        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ClienteService(IClienteRepository clienteRepository,
                             INotificador notificador,
                             IEnderecoRepository enderecoRepository,
                             IContatoRepository contatoRepository) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
            _contatoRepository = contatoRepository;
        }
        #endregion

        public async Task Adicionar(Cliente cliente)
        {
            // validar o estado da entidade e se o cliente não existe
            if (!ExecutarValidacao(new ClienteValidation(), cliente)
                 || !ExecutarValidacao(new EnderecoValidation(), cliente.Endereco)
                 || !ExecutarValidacao(new ContatoValidation(), cliente.Contato)) return;
            
            if (_clienteRepository.Buscar(f => f.Nif == cliente.Nif).Result.Any())
            {
                Notificar("Já existe um cliente com este nif infomado.");
                return;
            }
            if (_clienteRepository.Buscar(f => f.Contato.Telefone == cliente.Contato.Telefone).Result.Any())
            {
                Notificar("Já existe um cliente com este telefone infomado.");
                return;
            }
            if (_clienteRepository.Buscar(f => f.Contato.Email == cliente.Contato.Email).Result.Any())
            {
                Notificar("Já existe um cliente com este email infomado.");
                return;
            }

            await _clienteRepository.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente)) return;

            if (_clienteRepository.Buscar(f => f.Nif == cliente.Nif && f.Id != cliente.Id).Result.Any())
            {
                Notificar("Já existe um cliente com este nif infomado.");
                return;
            }
            await _clienteRepository.Atualizar(cliente);
        }
        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }
        public async Task AtualizarContato(Contato contato)
        {
            if(!ExecutarValidacao(new ContatoValidation(), contato)) return;
            await _contatoRepository.Atualizar(contato);
        }
        
       
        public async Task Remover(Guid id)
        {
            await _clienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
            _contatoRepository?.Dispose();
        }


    }
}