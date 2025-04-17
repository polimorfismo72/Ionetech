using Microsoft.AspNetCore.Mvc;
using DevIONETEC.App.ViewModels;
using DevIONETEC.Business.Intefaces;
using AutoMapper;
using DevIONETEC.Business.Models;
using DevIONETEC.Data.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DevIONETEC.App.Controllers
{
    [Authorize]
    public class FornecedoresController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFabricanteRepository _fabricanteRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        //private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                      IFabricanteRepository fabricanteRepository,
                                      ICategoriaRepository categoriaRepository,
                                      IProdutoRepository produtoRepository,
                                     IMapper mapper
          //,IFornecedorService fornecedorService
                                    ,INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _fabricanteRepository = fabricanteRepository;
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            // _fornecedorService = fornecedorService;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            //return View(await _fornecedorRepository.ObterTodos());
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedor(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-fornecedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            //await _fornecedorService.Adicionar(fornecedor);
            await _fornecedorRepository.Adicionar(fornecedor);

            if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        // [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutos(id);

            if(fornecedorViewModel is null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            //await _fornecedorService.Atualizar(fornecedor);
            await _fornecedorRepository.Atualizar(fornecedor);

            // if (!OperacaoValida()) return View(await ObterFornecedorProdutos(id));

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedor(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedor(id);

            if (fornecedorViewModel == null) return NotFound();

            //await _fornecedorService.Remover(id);
            await _fornecedorRepository.Remover(id);

            //if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }
        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<FornecedorViewModel> ObterFornecedor(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedor(id));
        }
        private async Task<FornecedorViewModel> ObterFornecedorProdutos(Guid id)
        {
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutos(id));
            return  fornecedor;
        }
        #endregion

    }
}
