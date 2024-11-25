using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamposDealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigração : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorUnitario",
                table: "Vendas",
                newName: "vlrUnitarioVenda");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Vendas",
                newName: "qtdVenda");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Vendas",
                newName: "idProduto");

            migrationBuilder.RenameColumn(
                name: "DataVenda",
                table: "Vendas",
                newName: "dthVenda");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Vendas",
                newName: "ProdutoidProduto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vendas",
                newName: "idVenda");

            migrationBuilder.RenameColumn(
                name: "ValorUnitario",
                table: "Produtos",
                newName: "vlrUnitario");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Produtos",
                newName: "dscProduto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Produtos",
                newName: "idProduto");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Clientes",
                newName: "nmCliente");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "IdCliente");

            migrationBuilder.AddColumn<int>(
                name: "ClienteIdCliente",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteIdCliente",
                table: "Vendas",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ProdutoidProduto",
                table: "Vendas",
                column: "ProdutoidProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Clientes_ClienteIdCliente",
                table: "Vendas",
                column: "ClienteIdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Produtos_ProdutoidProduto",
                table: "Vendas",
                column: "ProdutoidProduto",
                principalTable: "Produtos",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Clientes_ClienteIdCliente",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Produtos_ProdutoidProduto",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_ClienteIdCliente",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_ProdutoidProduto",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "ClienteIdCliente",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "vlrUnitarioVenda",
                table: "Vendas",
                newName: "ValorUnitario");

            migrationBuilder.RenameColumn(
                name: "qtdVenda",
                table: "Vendas",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "idProduto",
                table: "Vendas",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "dthVenda",
                table: "Vendas",
                newName: "DataVenda");

            migrationBuilder.RenameColumn(
                name: "ProdutoidProduto",
                table: "Vendas",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "idVenda",
                table: "Vendas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "vlrUnitario",
                table: "Produtos",
                newName: "ValorUnitario");

            migrationBuilder.RenameColumn(
                name: "dscProduto",
                table: "Produtos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "idProduto",
                table: "Produtos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nmCliente",
                table: "Clientes",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Clientes",
                newName: "Id");
        }
    }
}
