using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabricioMilano.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Mes = table.Column<int>(type: "INTEGER", nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    horasTrabalhadas = table.Column<int>(type: "INTEGER", nullable: false),
                    valorHora = table.Column<float>(type: "REAL", nullable: false),
                    salarioBruto = table.Column<float>(type: "REAL", nullable: false),
                    impostoRenda = table.Column<float>(type: "REAL", nullable: false),
                    inss = table.Column<float>(type: "REAL", nullable: false),
                    fgts = table.Column<float>(type: "REAL", nullable: false),
                    salarioLiquido = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
