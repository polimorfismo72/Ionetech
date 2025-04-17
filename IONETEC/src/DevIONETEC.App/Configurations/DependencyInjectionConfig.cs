using DevIONETEC.App.Extensions;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models.Validations.Documentos;
using DevIONETEC.Business.Notificacoes;
using DevIONETEC.Business.Services;
using DevIONETEC.Data.Context;
using DevIONETEC.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace DevIONETEC.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IonetecDbContext>();
            services.AddScoped<IPedidoItemsRepository, PedidoItemsRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFabricanteRepository, FabricanteRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IVendedorRepository, VendedorRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoItemsRepository, PedidoItemsRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<INotaEntregaNotaRecebidaRepository, NotaEntregaNotaRecebidaRepository>();

            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            services.AddScoped<INotificador, Notificador>();
            //services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IClienteService, ClienteService>();
            //services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}