using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZonaA1",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZonaA2",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZonaA3",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZonaA4",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZonaA1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "ZonaA2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "ZonaA3",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "ZonaA4",
                table: "RegistroResultados");
        }
    }
}
