using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoProdutos.Api.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO \"Categorias\" (\"Nome\", \"ImagemUrl\") VALUES ('Motor','motor.jpg')");
            migrationBuilder.Sql("INSERT INTO \"Categorias\" (\"Nome\", \"ImagemUrl\") VALUES ('Freios','freios.jpg')");
            migrationBuilder.Sql("INSERT INTO \"Categorias\" (\"Nome\", \"ImagemUrl\") VALUES ('Suspensão','suspensao.jpg')");
            migrationBuilder.Sql("INSERT INTO \"Categorias\" (\"Nome\", \"ImagemUrl\") VALUES ('Elétrica','eletrica.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Categorias\"");
        }
    }
}
