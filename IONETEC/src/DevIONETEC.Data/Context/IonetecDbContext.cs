using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Context
{
    public class IonetecDbContext : DbContext
    {
        public IonetecDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
       public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<NotaEntregaNotaRecebida> NotaEntregaNotaRecebidas { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Confiração padráo para varchar(100) das entidades cujo campo é string */
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            base.OnModelCreating(modelBuilder);

            /* ApplyConfigurationsFromAssembly Configura de uma só vez o mapeamento das entidades bo Mappings */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IonetecDbContext).Assembly);


            /* Desabilitar o Cascade Delete */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
