using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopB1",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopB2",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopB3",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopB4",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZonaB1",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZonaB2",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZonaB3",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZonaB4",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopB1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "TopB2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "TopB3",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "TopB4",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "ZonaB1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "ZonaB2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "ZonaB3",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "ZonaB4",
                table: "RegistroResultados");
        }
    }
}
