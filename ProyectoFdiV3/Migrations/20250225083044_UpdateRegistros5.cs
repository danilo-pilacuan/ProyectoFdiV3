using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completado3",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Intento3",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "MaxEscala3",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Puesto",
                table: "RegistroResultados");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completado3",
                table: "RegistroResultados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Intento3",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxEscala3",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Puesto",
                table: "RegistroResultados",
                type: "int",
                nullable: true);
        }
    }
}
