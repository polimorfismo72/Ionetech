using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIONETEC.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Filial)
                .IsRequired()
                .HasColumnType("varchar(250)");

            // 1 : N => Produto : PedidoItens
            builder.HasMany(p => p.PedidoItems)
                .WithOne(pi => pi.Produto)
                .HasForeignKey(pi => pi.ProdutoId);

            builder.ToTable("Produtos");
        }
    }
}
