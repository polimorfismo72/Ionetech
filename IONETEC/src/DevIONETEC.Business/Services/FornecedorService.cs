using System;
using System.Linq;
using System.Threading.Tasks;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Business.Models.Validations;

namespace DevIONETEC.Business.Services
{

    public class FornecedorService : BaseService, IFornecedorService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IFornecedorRepository _fornecedorRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
        }
        #endregion

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task Remover(Guid id)
        {
            await _fornecedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
        }
    }

}