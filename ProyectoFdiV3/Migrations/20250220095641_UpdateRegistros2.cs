using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoRegistro",
                table: "RegistroResultados",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "TipoRegistro",
                table: "RegistroResultados");
        }
    }
}
