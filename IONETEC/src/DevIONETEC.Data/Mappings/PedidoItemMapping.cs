using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Mappings
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NomeProduto)
             .IsRequired()
             .HasColumnType("varchar(250)");

            builder.Property(p => p.NumeroDeSerie)
                .IsRequired()
                .HasColumnType("varchar(30)");

            // 1 : N => Pedido : PedidoItens
            builder.HasOne(c => c.Pedido)
                .WithMany(c => c.PedidoItems)
            .HasForeignKey(e => e.PedidoId);

            // 1 : N => Produto : PedidoItens
            builder.HasOne(c => c.Produto)
                .WithMany(c => c.PedidoItems)
            .HasForeignKey(e => e.ProdutoId);

            // 1 : N => Vendedor : PedidoItens
            builder.HasOne(c => c.Vendedor)
                .WithMany(c => c.PedidoItems)
            .HasForeignKey(e => e.VendedorId);

            builder.ToTable("PedidoItems");
        }
    }
}