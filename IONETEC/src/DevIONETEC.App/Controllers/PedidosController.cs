using Microsoft.AspNetCore.Mvc;
using DevIONETEC.App.ViewModels;
using AutoMapper;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using DevIONETEC.Data.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DevIONETEC.App.Controllers
{
    [Authorize]
    public class PedidosController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        //private readonly ApplicationDbContext _user;
        private readonly UserManager<IdentityUser> _user;
        private readonly IonetecDbContext _context;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoItemsRepository _pedidoItemsRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IVendedorRepository _vendedorRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public PedidosController(
            UserManager<IdentityUser> user,
            IonetecDbContext context,
            IPedidoRepository pedidoRepository,
            IPedidoItemsRepository pedidoItemsRepository,
            IMapper mapper,
            INotificador notificador,
            IClienteRepository clienteRepository,
            IVendedorRepository vendedorRepository,
            IProdutoRepository produtoRepository) : base(notificador)
        {
            _user = user;
            _context = context;
            _pedidoRepository = pedidoRepository;
            _pedidoItemsRepository = pedidoItemsRepository;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _vendedorRepository = vendedorRepository;
            _produtoRepository = produtoRepository;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL

        [Route("lista-de-clientes-para-o-pedido")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosClientes()));
        }


      
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        //[AllowAnonymous]
        [Route("seleciona-o-cliente-para-o-pedido")]
        public async Task<IActionResult> Create()
        {
            var pedidoViewModel = await PopularClientes(new PedidoViewModel());
            return View(pedidoViewModel);
        }

        public async Task<IActionResult> ClinteAdicionar()
        {
            var pedidoViewModel = await PopularClientes(new PedidoViewModel());
            return View(pedidoViewModel);
        }

        [HttpPost]
        [Route("cliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClinteAdicionar(PedidoViewModel pedidoViewModel)
        {
            if (ModelState.IsValid) return View(pedidoViewModel.Cliente);

            var cliente = _mapper.Map<Cliente>(pedidoViewModel.Cliente);
            // await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteViewModel));
             await _clienteRepository.Adicionar(cliente);
            //await _clienteService.Adicionar(cliente);

            if (!OperacaoValida()) return View(pedidoViewModel);
            var pedido = await PopularClientes(new PedidoViewModel());
            return RedirectToAction("Create", pedido);
        }

        [HttpPost]
        [Route("seleciona-o-cliente-para-o-pedido")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidoViewModel pedidoViewModel)
        {
            pedidoViewModel.PercentualDesconto = 0;
            pedidoViewModel.ValorDesconto = 0;
            pedidoViewModel.ValorTotal = 0;
            pedidoViewModel.OperacaoPedidos = 1;
            pedidoViewModel.Situacao = 1;
            pedidoViewModel.TipoPagamento = 2;
            pedidoViewModel.NumeroDeTransacaoDePagamento = "123456";

            pedidoViewModel = await PopularClientes(pedidoViewModel);

            if (!ModelState.IsValid) return View(pedidoViewModel);

            //var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            //await _pedidoService.Adicionar(pedido);
            //await _pedidoRepository.Adicionar(pedido);

            await _pedidoRepository.Adicionar(_mapper.Map<Pedido>(pedidoViewModel));
            //if (!OperacaoValida()) return View(pedidoViewModel);
            
            //var pedido = await ObterPedidoPedidoItemsCliente(id) ;
            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        // AD,VI,ED,EX

        //[ClaimsAuthorize("Pedidos", "ED")]
        [Route("carrinho-do-cliente-e-pedido/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var listaProduros = (from p in _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos())
                                 select new SelectListItem(){ Text=p.Nome, Value=p.Id.ToString() }).ToList();
            listaProduros.Insert(0, new SelectListItem(){Text = "----Selecione----", Value = string.Empty});

            var listaVenderos = (from p in _mapper.Map<IEnumerable<VendedorViewModel>>(await _vendedorRepository.ObterTodos())
                                 select new SelectListItem() { Text = p.Nome, Value = p.Id.ToString() }).ToList();
            listaVenderos.Insert(0, new SelectListItem() { Text = "----Selecione----", Value = string.Empty });

            ViewBag.Produro = listaProduros;
            ViewBag.Vendedor = listaVenderos;

            var pedido = await ObterPedidoPedidoItemsCliente(id);

            ViewBag.Id = pedido.Id;
            
            var cliente = await ObterPedidoPorCliente(id);
            ViewBag.Nome = cliente.Cliente.Nome;

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        [HttpPost]
        //[ClaimsAuthorize("Pedidos", "ED")]
        [ValidateAntiForgeryToken]
        [Route("carrinho-do-cliente-e-pedido/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, PedidoViewModel pedidoViewModel)
        {
            #region LISTAR PRODUTOS E VENDEDORES
            var listaProduros = (from p in _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos())
                                 select new SelectListItem() { Text = p.Nome, Value = p.Id.ToString() }).ToList();
            listaProduros.Insert(0, new SelectListItem() { Text = "----Selecione----", Value = string.Empty });

            var listaVenderos = (from p in _mapper.Map<IEnumerable<VendedorViewModel>>(await _vendedorRepository.ObterTodos())
                                 select new SelectListItem() { Text = p.Nome, Value = p.Id.ToString() }).ToList();
            listaVenderos.Insert(0, new SelectListItem() { Text = "----Selecione----", Value = string.Empty });
            ViewBag.Produro = listaProduros;
            ViewBag.Vendedor = listaVenderos;
            #endregion

            #region VARIAVEIS PARA BUSCA DE VALORES
            var idproduto = pedidoViewModel.PedidoItem.ProdutoId;
            var produto = await ObterNomeProduto(idproduto);
            var valorVenda = produto.ValorVenda;

            var produtoAtualizacao = await ObterProdutoPorId(idproduto);
            var pedido = await ObterPedido(id);
            var p2 = pedido.ValorDesconto;
            #endregion

            #region BUSCA PRODUTO
            ViewBag.ProdutoId = idproduto;
            var quantidadeProduto = produto.QuantidadeEstoque;
            var nome = produto.Nome;

            #endregion

            #region PEGAR O USUARIO LOGADO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _vendedorRepository.ObterVendedorPeloNome(nomeUsuarioLogado);
            Guid guidFuncionario = emailFuncionario.Id;
            var estadoFuncionario = emailFuncionario.Ativo;
            var email = emailFuncionario.Email;

            if (email != nomeUsuarioLogado || estadoFuncionario == false)
            {
                TempData["Erro"] = "Opa! Este Funcionário não Existe, deve solicitar ao Administrador :(";
                return RedirectToAction("Edit");
            }
            #endregion

            var quantidadeEstoque = (quantidadeProduto - pedidoViewModel.PedidoItem.Quantidade);
            ViewBag.QuantidadeEstoque = quantidadeEstoque;

            #region ACTUALIZAR STOQUE DO PRODUTO

            produtoAtualizacao.Id = produtoAtualizacao.Id;
            produtoAtualizacao.FornecedorId = produtoAtualizacao.FornecedorId;
            produtoAtualizacao.FabricanteId = produtoAtualizacao.FabricanteId;
            produtoAtualizacao.CategoriaId = produtoAtualizacao.CategoriaId;

            produtoAtualizacao.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoAtualizacao.Fabricante = produtoAtualizacao.Fabricante;
            produtoAtualizacao.Categoria = produtoAtualizacao.Categoria;
            produtoAtualizacao.Imagem = produtoAtualizacao.Imagem;

            if (produtoAtualizacao.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(produtoAtualizacao.ImagemUpload, imgPrefixo))
                {
                    return View(produtoAtualizacao);
                }

                produtoAtualizacao.Imagem = imgPrefixo + produtoAtualizacao.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoAtualizacao.Nome;
            produtoAtualizacao.Descricao = produtoAtualizacao.Descricao;
            produtoAtualizacao.ValorCompra = produtoAtualizacao.ValorCompra;
            produtoAtualizacao.ValorVenda = produtoAtualizacao.ValorVenda;
            produtoAtualizacao.QuantidadeEstoque = quantidadeEstoque;
            produtoAtualizacao.Filial = produtoAtualizacao.Filial;

            produtoAtualizacao.Ativo = produtoAtualizacao.Ativo;

            pedidoViewModel.PedidoItem.NomeProduto = nome;
            
            if (quantidadeEstoque > 0)
            {
                pedidoViewModel.PedidoItem.ValorUnitario = valorVenda;
                pedidoViewModel.PedidoItem.VendedorId = guidFuncionario;
                await _pedidoItemsRepository.Adicionar(_mapper.Map<PedidoItem>(pedidoViewModel.PedidoItem));
                await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
            }
            else
            {
                TempData["Erro"] = "Opa! Estoque Esgotado :(";
            }
            #endregion

            #region ACTUALIZAR PEDIDO

            if (id != pedidoViewModel.Id) return NotFound();
            var pedidoAtualizacao = await ObterPedido(id);
            var p3 = pedidoAtualizacao.ValorDesconto;

            pedidoViewModel.Cliente = pedidoAtualizacao.Cliente;

            pedidoAtualizacao.PercentualDesconto = pedidoViewModel.PercentualDesconto;
            pedidoAtualizacao.ValorDesconto = pedidoViewModel.ValorDesconto;
            pedidoAtualizacao.ValorTotal = ((pedidoAtualizacao.ValorTotal +
               (pedidoViewModel.PedidoItem.Quantidade * valorVenda)) - pedidoViewModel.ValorDesconto);

            pedidoAtualizacao.OperacaoPedidos = pedidoViewModel.OperacaoPedidos;
            pedidoAtualizacao.Situacao = pedidoViewModel.Situacao;
            pedidoAtualizacao.TipoPagamento = pedidoViewModel.TipoPagamento;
            pedidoAtualizacao.NumeroDeTransacaoDePagamento = pedidoViewModel.NumeroDeTransacaoDePagamento;
            pedidoAtualizacao.Ativo = pedidoViewModel.Ativo;
            await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(pedidoAtualizacao));

            #endregion

            #region REMOVER DO MODEL STATE
            ModelState.Remove("Nome");
            ModelState.Remove("Filial");
            ModelState.Remove("Descricao");
            ModelState.Remove("Produto.Id");
            ModelState.Remove("Produto.Nome");
            ModelState.Remove("Produto.Ativo");
            ModelState.Remove("Produto.Filial");
            ModelState.Remove("Produto.Descricao");
            ModelState.Remove("Produto.ValorVenda");
            ModelState.Remove("Produto.ValorVenda");
            ModelState.Remove("Produto.CategoriaId");
            ModelState.Remove("Produto.ValorCompra");
            ModelState.Remove("Produto.FabricanteId");
            ModelState.Remove("Produto.FornecedorId");
            ModelState.Remove("Produto.QuantidadeEstoque");
            ModelState.Remove("PedidoItem.VendedorId");
            ModelState.Remove("PedidoItem.NumeroDeSerie");
            ModelState.Remove("PedidoItem.ValorUnitario");
            #endregion

            if (!ModelState.IsValid) return View("Edit");

         
            //await _pedidoService.Atualizar(pedido);

            // if (!OperacaoValida()) return View(await ObterPedidoClientes(id));
            var pedidoItems = _mapper.Map<IEnumerable<PedidoItemViewModel>>(await _pedidoItemsRepository.ObterPedidoItemsPorPedido(pedido.Id));
           
            return RedirectToAction("Edit", pedidoItems);
        }

        #endregion

        #region FINALIZAR A VENDA
        public async Task<IActionResult> FinalizarVenda(Guid id, PedidoViewModel pedidoViewModel)
        {
            #region ACTUALIZAR PEDIDO
            if (id != pedidoViewModel.Id) return NotFound();
            var pedidoAtualizacao = await ObterPedido(id);
            var itemDoPedidoId = pedidoAtualizacao.Id;
            var pedido = await ObterPedidoPedidoItemsCliente(id);
            var contador = _context.PedidoItems.Where(c => c.PedidoId == itemDoPedidoId)
              .Select(c => c.PedidoId).Count();

            if (ModelState.IsValid) return View(pedidoViewModel);
            //pedidoAtualizacao.ven = guidFuncionario;
            pedidoAtualizacao.Situacao = 2;
            pedidoAtualizacao.Ativo = true;
            if (contador == 0)
            {
                TempData["Erro"] = "Opa ): Não é possível terminar a venda. Item Vazio!";
                return RedirectToAction("Edit", pedido);
            }
            await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(pedidoAtualizacao));

            #endregion

            //if (!OperacaoValida()) return View(produto);
            var index = _mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosClientes());
            return RedirectToAction("Index", index);
        }
        #endregion

        #region MÉTODO PARA EXCLUIR

        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-pedidoItem/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await ObterPedidoItemPorId(id);
            var idproduto = item.ProdutoId;
            var idpedido = item.PedidoId;
            var produtoAtualizacao = await ObterProdutoPorId(idproduto);
            var pedidoAtualizacao = await ObterPedido(idpedido);

            //var qtd = item.Quantidade;

            if (item == null) return NotFound();
            //await _produtoService.Remover(id);
            await _pedidoItemsRepository.Remover(id);

            #region REPOR A QUANTIDADE NO STOQUE DO PRODUTO
            
            //produtoAtualizacao.Id = produtoAtualizacao.Id;
            //produtoAtualizacao.FornecedorId = produtoAtualizacao.FornecedorId;
            //produtoAtualizacao.FabricanteId = produtoAtualizacao.FabricanteId;
            //produtoAtualizacao.CategoriaId = produtoAtualizacao.CategoriaId;

            //produtoAtualizacao.Imagem = produtoAtualizacao.Imagem;
            if (!ModelState.IsValid) return View(produtoAtualizacao);

            //if (produtoAtualizacao.ImagemUpload != null)
            //{
            //    var imgPrefixo = Guid.NewGuid() + "_";
            //    if (!await UploadArquivo(produtoAtualizacao.ImagemUpload, imgPrefixo))
            //    {
            //        return View(produtoAtualizacao);
            //    }

            //    produtoAtualizacao.Imagem = imgPrefixo + produtoAtualizacao.ImagemUpload.FileName;
            //}

            //produtoAtualizacao.Nome = produtoAtualizacao.Nome;
            //produtoAtualizacao.Descricao = produtoAtualizacao.Descricao;
            //produtoAtualizacao.ValorCompra = produtoAtualizacao.ValorCompra;
            //produtoAtualizacao.ValorVenda = produtoAtualizacao.ValorVenda;

            produtoAtualizacao.QuantidadeEstoque = (produtoAtualizacao.QuantidadeEstoque + item.Quantidade);
            //produtoAtualizacao.QuantidadeEstoque = (produtoAtualizacao.QuantidadeEstoque + qtd);

            //produtoAtualizacao.Filial = produtoAtualizacao.Filial;

            //produtoAtualizacao.Ativo = produtoAtualizacao.Ativo;
            
             await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
           
            pedidoAtualizacao.ValorTotal = (pedidoAtualizacao.ValorTotal - (item.Quantidade*item.ValorUnitario));
            await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(pedidoAtualizacao));

            #endregion

            //if (!OperacaoValida()) return View(produto);
            TempData["Sucesso"] = "Item excluido com sucesso!";
           
            return RedirectToAction("ItemDeleted");
        }

        [Route("item-excluido")]
        public async Task<IActionResult> ItemDeleted()
        {
            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosClientes()));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[ClaimsAuthorize("Fornecedor", "Editar")]
        public async Task<IActionResult> ApagarPedido(Guid id, PedidoViewModel pedidoViewModel)
        {
           var pedido = await ObterPedidoPorId(id);
            var idpedido = pedido.Id;

            var produto = await ObterPedidoItemsPeloPedido(idpedido);
            var index = _mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosClientes());

            if (produto == null)
            {
                await _pedidoRepository.Remover(id);
                return RedirectToAction("Index", index);
            }

            var idproduto = produto.Produto.Id;
           
            var contador = _context.PedidoItems.Where(c => c.PedidoId == idpedido)
              .Select(c => c.PedidoId).Count();
            for (int i = 0; i < contador; i++)
            {
                var produto1 = await ObterPedidoItemsPeloPedido(idpedido);
                var idpedidoItem = produto1.Id;
                var idproduto1 = produto1.ProdutoId;
                await _pedidoItemsRepository.Remover(idpedidoItem);

                #region REPOR A QUANTIDADE NO STOQUE DO PRODUTO
                _context.ChangeTracker.Clear();
                var produtoAtualizacao = await ObterProdutoPorId(idproduto1);
                if (ModelState.IsValid) return View(produtoAtualizacao);

                produtoAtualizacao.QuantidadeEstoque = (produtoAtualizacao.QuantidadeEstoque + produto.Quantidade);

                await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
                await _context.SaveChangesAsync();
                #endregion
            }

            if (pedido == null) return NotFound();
            //await _produtoService.Remover(id);
            await _pedidoRepository.Remover(id);

            //if (!OperacaoValida()) return View(produto);
            return RedirectToAction("Index", index);
        }
        #endregion

        #region MÉTODO PARA SERVIÇOS

        private async Task<PedidoViewModel> ObterPedidoPedidoItems(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoPedidoItems(id));
        }

        private async Task<PedidoViewModel> ObterPedidoPorCliente(Guid id)
        {
            var pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoClientes(id));
            return pedido;
        }
        private async Task<PedidoViewModel> ObterPedido(Guid id)
        {
            var pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoClientes(id));
            pedido.Clientes = _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
            return pedido;
        }
        private async Task<PedidoViewModel> ObterPedidoPeloItems(Guid id)
        {
           return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoPeloItems(id));
        }
        private async Task<PedidoViewModel> PopularClientes(PedidoViewModel pedido)
        {
            pedido.Clientes = _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
            return pedido;
        }
      
        private async Task<PedidoViewModel> PopularProdutos(PedidoViewModel pedido)
        {
            pedido.Produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
            return pedido;
        }
        
        private async Task<PedidoItemViewModel> ObterPedidoItemsPeloPedido(Guid id)
        {
            return _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPedidoItemsPeloPedido(id));
        }
        private async Task<PedidoItemViewModel> ObterPedidosItemsProdutos(PedidoItemViewModel pedido)
        {
            return _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPedidosItemsProdutos());
        }
        private async Task<PedidoViewModel> ObterPedidoClientes(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoClientes(id));
        }

        private async Task<ClienteViewModel> ObterCarrinhoCliente(Guid id)
        {
            var cliente = _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterPorId(id));
            return cliente;
        }
        private async Task<ProdutoViewModel> ObterNomeProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterNomeProduto(id));
            return produto;
        }
        private async Task<ProdutoViewModel> ObterProdutoPorId(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoPorId(id));
            return produto;
        }
        private async Task<PedidoItemViewModel> ObterPedidoItemPorId(Guid id)
        {
            var item = _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPorId(id));
            return item;
        }
        private async Task<PedidoViewModel> ObterPedidoPorId(Guid id)
        {
            var item = _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPorId(id));
            return item;
        }
        private async Task<PedidoItemViewModel> ObterPedidoItemsPorPedido(Guid id)
        {
            return _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPedidoItemsPorPedido(id));
        }
        private async Task<PedidoViewModel> ObterPedidoPedidoItemsCliente(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoPedidoItemsCliente(id));
        }

        [HttpGet]
        public JsonResult getPrecoProdutoUnitario(Guid produtoId)
        {
            //var produto = new Produto();
            decimal PrecoUnit = 0;
            PrecoUnit = _context.Produtos.Single(model => model.Id == produtoId).ValorVenda;

            return Json(PrecoUnit);
        }
        public IActionResult GetPrecoProdutoUnitario(Guid produtoId)
        {
            decimal PrecoUnit = _context.Produtos.Single(model => model.Id == produtoId).ValorVenda;
            return Json(PrecoUnit);
        }
        public IActionResult GetNifCliente(Guid clienteId)
        {
            string Nif = _context.Clientes.Single(model => model.Id == clienteId).Nif;
            return Json(Nif);
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
