using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevIONETEC.Business.Models;

namespace DevIONETEC.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Bairro)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(e => e.Municipio)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Provincia)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.ToTable("Enderecos");
        }
    }
}