using Microsoft.AspNetCore.Mvc;
using DevIONETEC.App.ViewModels;
using AutoMapper;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Data.Repository;
using Microsoft.EntityFrameworkCore;
using DevIONETEC.Data.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DevIONETEC.App.Controllers
{
    [Authorize]
    public class ClientesController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IContatoRepository _contatoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;
        private readonly IonetecDbContext _context;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ClientesController(IClienteRepository clienteRepository,
                                  IMapper mapper,
                                  IEnderecoRepository enderecoRepository,
                                  IContatoRepository contatoRepository,
                                  IClienteService clienteService,
                                  IProdutoRepository produtoRepository,
                                  IPedidoRepository pedidoRepository,
        INotificador notificador,
        IonetecDbContext context) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
            _contatoRepository = contatoRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _clienteService = clienteService;
            _context = context;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [Route("lista-de-clientes")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos()));

        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [Route("dados-do-cliente/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var clienteViewModel = await ObterClienteEnderecoContato(id);

            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }
        #endregion

        #region MÉTODO PARA CRIAR NOVO
        [Route("novo-cliente")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-cliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return View(clienteViewModel);

            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            // await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteViewModel));
            // await _clienteRepository.Adicionar(cliente);
            await _clienteService.Adicionar(cliente);

            if (!OperacaoValida()) return View(clienteViewModel);

            return RedirectToAction("Index");
        }

        #endregion

        #region MÉTODO PARA EDITAR
        [Route("editar-cliente/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var clienteViewModel = await ObterClienteEnderecoContato(id);

            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-cliente/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(clienteViewModel);
            //await _clienteService.Atualizar(_mapper.Map<Cliente>(clienteViewModel));
            await _clienteRepository.Atualizar(_mapper.Map<Cliente>(clienteViewModel));


             if (!OperacaoValida()) return View(await ObterClienteEnderecoContato(id));

            return RedirectToAction("Index");
        }

        #endregion

        #region MÉTODO PARA CARRINHO

        [Route("compra-de-produto/{id:guid}")]
        //[Route("compra-de-produto")]
        public async Task<IActionResult> Carrinho(Guid id, /*PedidoViewModel PedidoViewModel,*/ ClienteViewModel clienteViewModel)
        {
             clienteViewModel = await ObterClientePorId(id);
            ViewBag.Id = clienteViewModel.Id;
            ViewBag.Nome = clienteViewModel.Nome; 
            //clienteViewModel = await PopularProdutos(clienteViewModel);
            //ViewBag.ValorVenda = await PopularProdutos(clienteViewModel);
            //ViewBag.Preco = "100.00";
            //var tutorials = await _produtoRepository.ObterTodos();
            //var tutorials = await ObterProdutoPrecoValorVenda(id);
            
            //ViewBag.ValorVenda = new SelectList(tutorials, "Id", "Nome");
            //ViewBag.ValorVenda = new SelectList(tutorials, "Id", "ValorVenda");
            
            //ViewBag.ValorVenda = await ObterProdutoPrecoValorVenda(id);

            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
            //return PartialView("_Pedido", PedidoViewModel);
            //return PartialView("_Pedido", clienteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("compra-de-produto/{id:guid}")]
        public async Task<IActionResult> Carrinho(Guid id, ClienteViewModel clienteViewModel, PedidoViewModel pedidoViewModel)
        {
            clienteViewModel = await ObterClientePorId(id);

            pedidoViewModel.ClienteId = clienteViewModel.Id;
            pedidoViewModel.PercentualDesconto = 0;
            pedidoViewModel.ValorDesconto = 0;
            pedidoViewModel.ValorTotal = 0;
            pedidoViewModel.Situacao = 1;
            pedidoViewModel.TipoPagamento = 2;
            pedidoViewModel.NumeroDeTransacaoDePagamento = "5553456";

            //if (!ModelState.IsValid) return View(clienteViewModel);
            if (id != clienteViewModel.Id) return NotFound();

            
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            await _pedidoRepository.Adicionar(pedido);

            // if (!OperacaoValida()) return View(await ObterClienteEnderecoContato(id));
            if (!OperacaoValida()) return View(pedidoViewModel);
            return RedirectToAction("PedidoCliente");
        }


        #endregion

        #region MÉTODO PARA EXCLUIR
        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-cliente/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var clienteViewModel = await ObterClienteEnderecoContato(id);

            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-cliente/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await ObterClienteEnderecoContato(id);

            if (cliente == null) return NotFound();

            await _clienteService.Remover(id);
            //await _clienteRepository.Remover(id);

            if (!OperacaoValida()) return View(cliente);

            return RedirectToAction("Index");
        }
        #endregion

        #region METODOS PARA ENDEREÇO E CONTATO
        [Route("obter-endereco-e-contacto/{id:guid}")]
        public async Task<IActionResult> ObterEnderecoContato(Guid id)
        {
            var cliente = await ObterClienteEnderecoContato(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEnderecoContato", cliente);
        }
        
        [Route("atualizar-endereco-e-contacto/{id:guid}")]
        public async Task<IActionResult> AtualizarEnderecoContato(Guid id)
        {
            var cliente = await ObterClienteEnderecoContato(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEnderecoContato", new ClienteViewModel { Endereco = cliente.Endereco, Contato = cliente.Contato });
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("atualizar-endereco-e-contacto/{id:guid}")]
        public async Task<IActionResult> AtualizarEnderecoContato(ClienteViewModel clienteViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Nif");

            if (!ModelState.IsValid) return PartialView("_AtualizarEnderecoContato", clienteViewModel);

            await _clienteService.AtualizarEndereco(_mapper.Map<Endereco>(clienteViewModel.Endereco));
            await _clienteService.AtualizarContato(_mapper.Map<Contato>(clienteViewModel.Contato));

            if (!OperacaoValida()) return PartialView("_AtualizarEnderecoContato", clienteViewModel);

            var url = Url.Action("ObterEnderecoContato", "Clientes", new { id = clienteViewModel.Endereco.ClienteId, clienteViewModel.Contato.ClienteId });
            return Json(new { success = true, url });
        }
        #endregion
      

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<ClienteViewModel> ObterClienteEnderecoContato(Guid id)
        {

            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterClienteEnderecoContato(id));
        }
        private async Task<ClienteViewModel> ObterClientePorId(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterPorId(id));
        }
        
        private async Task<ClienteViewModel> ObterClientePedidosEnderecoContato(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterClientePedidosEnderecoContato(id));
        }
        #endregion

        private async Task<ProdutoViewModel> ObterProdutoPrecoValorVenda(Guid id)
        {
           return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoPrecoValorVenda(id));
        }
        
        private async Task<ClienteViewModel> PopularProdutos(ClienteViewModel item)
        {
            item.Produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
            return item;
        }
        
        [HttpGet]
         //private async Task<IActionResult> GetPrecoProdutoUnitario1(Guid produtoId)
         private async Task<ProdutoViewModel> GetPrecoProdutoUnitario1(Guid produtoId)
         {
            //decimal PrecoUnit = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPrecoProdutoPorId(produtoId));
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPrecoProdutoPorId(produtoId));
            //return Json(PrecoUnit);
         }

        public IActionResult GetPrecoProdutoUnitario(Guid produtoId)
        //public JsonResult GetPrecoProdutoUnitario(Guid produtoId)
        {
            //decimal PrecoUnit = _context.Produtos.FirstOrDefault(model => model.Id == produtoId).ValorVenda;
            decimal PrecoUnit = _context.Produtos.Single(model => model.Id == produtoId).ValorVenda;
            return Json(PrecoUnit);
        }
    }
}
