using Microsoft.AspNetCore.Mvc;
using DevIONETEC.App.ViewModels;
using AutoMapper;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.App.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace DevIONETEC.App.Controllers
{
    [Authorize]
    [Route("meus-produtos")]
    public class ProdutosController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFabricanteRepository _fabricanteRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        //private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ProdutosController(IProdutoRepository produtoRepository,
                                IMapper mapper,
                                IFornecedorRepository fornecedorRepository,
                                IFabricanteRepository fabricanteRepository,
                                ICategoriaRepository categoriaRepository
         //,IProdutoService produtoService
                                ,INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
            _fabricanteRepository = fabricanteRepository;
            _categoriaRepository = categoriaRepository;
            //_produtoService = produtoService;
        }

        #endregion

        #region DECLARAR AS DEPENDENCIA

        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR

        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [ClaimsAuthorize("Produtos", "VI")]
        //[Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedoresFabricantesCategorias()));
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL

        [ClaimsAuthorize("Produtos", "VI")]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        //AD,VI,ED,EX
        [ClaimsAuthorize("Produtos", "AD")]
        [Route("criar-novo")]
        public async Task<IActionResult> Create()
        {
            var produtoViewModel = await PopularFornecedoresFabricantesCategorias(new ProdutoViewModel());
            return View(produtoViewModel);

        }

        [ClaimsAuthorize("Produtos", "AD")]
        [HttpPost]
        [Route("criar-novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedoresFabricantesCategorias(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            // await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));
            await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            //if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        [ClaimsAuthorize("Produtos", "AD")]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Produtos", "AD")]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);
            produtoViewModel.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoViewModel.Fabricante = produtoAtualizacao.Fabricante;
            produtoViewModel.Categoria = produtoAtualizacao.Categoria;

            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoViewModel);
                }

                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.ValorCompra = produtoViewModel.ValorCompra;
            produtoAtualizacao.ValorVenda = produtoViewModel.ValorVenda;
            produtoAtualizacao.QuantidadeEstoque = produtoViewModel.QuantidadeEstoque;
            produtoAtualizacao.Filial = produtoViewModel.Filial;

            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
            //await _produtoService.Atualizar(cliente);

            // if (!OperacaoValida()) return View(await ObterFornecedorProdutos(id));

            return RedirectToAction("Index");
        }

        #endregion

        #region MÉTODO PARA EXCLUIR

        [ClaimsAuthorize("Produtos", "EX")]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Produtos", "EX")]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null) return NotFound();

            //await _produtoService.Remover(id);
            await _produtoRepository.Remover(id);

            //if (!OperacaoValida()) return View(produto);
            TempData["Sucesso"] = "Produto excluido com sucesso!";
            return RedirectToAction("Index");
        }
       
        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedorFabricanteCategoria(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            produto.Fabricantes = _mapper.Map<IEnumerable<FabricanteViewModel>>(await _fabricanteRepository.ObterTodos());
            produto.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());

            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedoresFabricantesCategorias(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            produto.Fabricantes = _mapper.Map<IEnumerable<FabricanteViewModel>>(await _fabricanteRepository.ObterTodos());
            produto.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());

            return produto;
        }
        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            return produto;
        }
        private async Task<ProdutoViewModel> PopularFabricantes(ProdutoViewModel produto)
        {
            produto.Fabricantes = _mapper.Map<IEnumerable<FabricanteViewModel>>(await _fabricanteRepository.ObterTodos());
            return produto;
        }
        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            produto.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return produto;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
        
        #endregion

    }
}
