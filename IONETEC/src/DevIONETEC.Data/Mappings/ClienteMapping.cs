using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Nif)
                .IsRequired()
                .HasColumnType("varchar(14)");

            //builder.OwnsOne(c => c.Email, tf =>
            //{
            //    tf.Property(c => c.Endereco)
            //        .IsRequired()
            //        .HasColumnName("Email")
            //        .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            //});

       

            //1 : 1 => Cliente: Endereco
            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Cliente);

            builder.HasOne(c => c.Contato)
            .WithOne(c => c.Cliente);

            builder.ToTable("Clientes");
        }
    }
}