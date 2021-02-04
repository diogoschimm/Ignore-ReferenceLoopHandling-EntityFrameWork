using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiSampleIncludes.Migrations
{
    public partial class SubstituicaoCampoPessoaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaFinanceira_Pessoa_PessoaId",
                table: "ContaFinanceira");

            migrationBuilder.DropColumn(
                name: "IdPessoa",
                table: "ContaFinanceira");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "ContaFinanceira",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContaFinanceira_Pessoa_PessoaId",
                table: "ContaFinanceira",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "PessoaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaFinanceira_Pessoa_PessoaId",
                table: "ContaFinanceira");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "ContaFinanceira",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdPessoa",
                table: "ContaFinanceira",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ContaFinanceira_Pessoa_PessoaId",
                table: "ContaFinanceira",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "PessoaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
