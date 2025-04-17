using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIONETEC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroDeSerie",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ValorUnitario",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "ProdutoNome",
                table: "PedidoItems",
                newName: "NomeProduto");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NumeroDeSerie",
                table: "PedidoItems",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "NumeroDeSerie",
                table: "PedidoItems");

            migrationBuilder.RenameColumn(
                name: "NomeProduto",
                table: "PedidoItems",
                newName: "ProdutoNome");

            migrationBuilder.AddColumn<string>(
                name: "NumeroDeSerie",
                table: "Pedidos",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitario",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
