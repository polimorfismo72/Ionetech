using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DevIONETEC.Business.Models;

namespace DevIONETEC.Data.Mappings
{
    public class FabricanteMapping : IEntityTypeConfiguration<Fabricante>
    {
        public void Configure(EntityTypeBuilder<Fabricante> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");


            // 1 : N => Fabricante : Produtos
            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fabricante)
                .HasForeignKey(p => p.FabricanteId);

            builder.ToTable("Fabricantes");
        }
        
    }
}