using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiSampleIncludes.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    PessoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.PessoaId);
                });

            migrationBuilder.CreateTable(
                name: "ContaFinanceira",
                columns: table => new
                {
                    ContaFinanceiraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeConta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPessoa = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaFinanceira", x => x.ContaFinanceiraId);
                    table.ForeignKey(
                        name: "FK_ContaFinanceira_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContaFinanceira_PessoaId",
                table: "ContaFinanceira",
                column: "PessoaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaFinanceira");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
