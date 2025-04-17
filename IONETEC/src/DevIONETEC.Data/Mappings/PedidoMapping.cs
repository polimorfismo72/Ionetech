using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {

        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(n => n.Codigo)
               .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");


            builder.Property(p => p.NumeroDeTransacaoDePagamento)
             .IsRequired()
             .HasColumnType("varchar(150)");


            //builder.Property(p => p.NumeroDeSerie)
            //    .IsRequired()
            //    .HasColumnType("varchar(30)");


            // 1 : N => Pedido : PedidoItem
            builder.HasMany(p => p.PedidoItems)
                .WithOne(pi => pi.Pedido)
                .HasForeignKey(pi => pi.PedidoId);


            builder.ToTable("Pedidos");
        }


    }
}