using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevIONETEC.Business.Models;

namespace DevIONETEC.Data.Mappings
{
    public class ContatoMapping : IEntityTypeConfiguration<Contato>
    {

        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Telefone)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(250)");


            builder.ToTable("Contatos");
        }

    }
}