using DevIONETEC.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevIONETEC.Data.Mappings
{
    public class NotaEntregaNotaRecebidaMapping : IEntityTypeConfiguration<NotaEntregaNotaRecebida>
    {

        public void Configure(EntityTypeBuilder<NotaEntregaNotaRecebida> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.Property(n => n.DocumentoPdf)
                .IsRequired()
                .HasColumnType("varchar(250)");

            // Entrega 
            builder.ToTable("NotaEntregaNotaRecebidas");
        }

    }
}