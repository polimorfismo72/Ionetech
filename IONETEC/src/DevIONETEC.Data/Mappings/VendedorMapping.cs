using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Mappings
{
    public class VendedorMapping : IEntityTypeConfiguration<Vendedor>
    {

        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(254)");

            // 1 : N => Vendedores : PedidoItem
            builder.HasMany(f => f.PedidoItems)
                .WithOne(p => p.Vendedor)
                .HasForeignKey(p => p.VendedorId);

            builder.ToTable("Vendedores");
        }

    }
}