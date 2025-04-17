using AutoMapper;
using DevIONETEC.App.ViewModels;
using DevIONETEC.Business.Models;

namespace DevIONETEC.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<PedidoItem, PedidoItemViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Fabricante, FabricanteViewModel>().ReverseMap();
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Vendedor, VendedorViewModel>().ReverseMap();
            CreateMap <Pedido, PedidoViewModel > ().ReverseMap();
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Contato, ContatoViewModel>().ReverseMap();
            CreateMap<NotaEntregaNotaRecebida, NotaEntregaNotaRecebidaViewModel>().ReverseMap();


            
        }
    }
}