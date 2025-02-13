using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class AddPropsRegistroResultado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistroResultados",
                columns: table => new
                {
                    IdRegistroResultado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDep = table.Column<int>(type: "int", nullable: true),
                    IdCom = table.Column<int>(type: "int", nullable: true),
                    Tiempo = table.Column<float>(type: "real", nullable: true),
                    Intento = table.Column<int>(type: "int", nullable: true),
                    Completado = table.Column<bool>(type: "bit", nullable: false),
                    MaxEscala = table.Column<int>(type: "int", nullable: true),
                    PorcentajeAlcanzado = table.Column<double>(type: "float", nullable: false),
                    UltimaPresa = table.Column<int>(type: "int", nullable: false),
                    Puesto = table.Column<int>(type: "int", nullable: true),
                    Etapa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroResultados", x => x.IdRegistroResultado);
                    table.ForeignKey(
                        name: "FK_RegistroResultados_Competencias_IdCom",
                        column: x => x.IdCom,
                        principalTable: "Competencias",
                        principalColumn: "IdCom");
                    table.ForeignKey(
                        name: "FK_RegistroResultados_Deportistas_IdDep",
                        column: x => x.IdDep,
                        principalTable: "Deportistas",
                        principalColumn: "IdDep");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultados_IdCom",
                table: "RegistroResultados",
                column: "IdCom");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultados_IdDep",
                table: "RegistroResultados",
                column: "IdDep");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroResultados");
        }
    }
}
