using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PuntajeCombinadaBloque",
                table: "RegistroResultados",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PuntajeCombinadaVia",
                table: "RegistroResultados",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntajeCombinadaBloque",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "PuntajeCombinadaVia",
                table: "RegistroResultados");
        }
    }
}
