using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevIONETEC.Business.DomainException;

namespace DevIONETEC.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(f => f.Documento)
                .IsRequired()
                .HasColumnType("varchar(16)");

            //builder.OwnsOne(c => c.Email, tf =>
            //{
            //    tf.Property(c => c.Endereco)
            //        .IsRequired()
            //        .HasColumnName("Email")
            //        .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            //});

            builder.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(254)");

            builder.Property(f => f.Telefone)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(f => f.Endereco)
                .IsRequired()
                .HasColumnType("varchar(250)");


            // 1 : N => Fornecedor : Produtos
            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");
        }
        
    }
}