using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoProdutos.Api.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO \"Produtos\" (\"Nome\",\"Descricao\",\"Preco\",\"ImagemUrl\",\"Estoque\",\"DataCadastro\",\"CategoriaId\") " +
                "VALUES ('Filtro de Óleo','Filtro de óleo para motores 1.0 a 2.0',24.90,'filtro-oleo.jpg',35,now(),1)");

            migrationBuilder.Sql(
                "INSERT INTO \"Produtos\" (\"Nome\",\"Descricao\",\"Preco\",\"ImagemUrl\",\"Estoque\",\"DataCadastro\",\"CategoriaId\") " +
                "VALUES ('Pastilha de Freio Dianteira','Jogo de pastilhas de freio dianteiras',89.90,'pastilha-freio.jpg',18,now(),2)");

            migrationBuilder.Sql(
                "INSERT INTO \"Produtos\" (\"Nome\",\"Descricao\",\"Preco\",\"ImagemUrl\",\"Estoque\",\"DataCadastro\",\"CategoriaId\") " +
                "VALUES ('Amortecedor Traseiro','Amortecedor traseiro a gás',210.00,'amortecedor.jpg',8,now(),3)");

            migrationBuilder.Sql(
                "INSERT INTO \"Produtos\" (\"Nome\",\"Descricao\",\"Preco\",\"ImagemUrl\",\"Estoque\",\"DataCadastro\",\"CategoriaId\") " +
                "VALUES ('Bateria 60Ah','Bateria automotiva 60Ah 12V',450.00,'bateria.jpg',12,now(),4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Produtos\"");
        }
    }
}
