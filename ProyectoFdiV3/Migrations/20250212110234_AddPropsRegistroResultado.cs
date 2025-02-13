using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class AddPropsRegistroResultado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroResultado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistroResultado",
                columns: table => new
                {
                    IdRegistroResultado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetenciumNavigationIdCom = table.Column<int>(type: "int", nullable: true),
                    IdDepNavigationIdDep = table.Column<int>(type: "int", nullable: true),
                    DetalleCompetenciumIdDetalle = table.Column<int>(type: "int", nullable: true),
                    Etapa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDep = table.Column<int>(type: "int", nullable: true),
                    IdDetalleCompetencia = table.Column<int>(type: "int", nullable: true),
                    Puesto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroResultado", x => x.IdRegistroResultado);
                    table.ForeignKey(
                        name: "FK_RegistroResultado_Competencias_CompetenciumNavigationIdCom",
                        column: x => x.CompetenciumNavigationIdCom,
                        principalTable: "Competencias",
                        principalColumn: "IdCom");
                    table.ForeignKey(
                        name: "FK_RegistroResultado_Deportistas_IdDepNavigationIdDep",
                        column: x => x.IdDepNavigationIdDep,
                        principalTable: "Deportistas",
                        principalColumn: "IdDep");
                    table.ForeignKey(
                        name: "FK_RegistroResultado_DetalleCompetencias_DetalleCompetenciumIdDetalle",
                        column: x => x.DetalleCompetenciumIdDetalle,
                        principalTable: "DetalleCompetencias",
                        principalColumn: "IdDetalle");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultado_CompetenciumNavigationIdCom",
                table: "RegistroResultado",
                column: "CompetenciumNavigationIdCom");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultado_DetalleCompetenciumIdDetalle",
                table: "RegistroResultado",
                column: "DetalleCompetenciumIdDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultado_IdDepNavigationIdDep",
                table: "RegistroResultado",
                column: "IdDepNavigationIdDep");
        }
    }
}
