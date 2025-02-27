using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completado1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Completado2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "PorcentajeAlcanzado1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "PorcentajeAlcanzado2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "UltimaPresa1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "UltimaPresa2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "UltimaPresa3",
                table: "RegistroResultados");

            migrationBuilder.RenameColumn(
                name: "PorcentajeAlcanzado3",
                table: "RegistroResultados",
                newName: "PuntajeFinalVia");

            migrationBuilder.RenameColumn(
                name: "Intento2",
                table: "RegistroResultados",
                newName: "RankingVia2");

            migrationBuilder.RenameColumn(
                name: "Intento1",
                table: "RegistroResultados",
                newName: "RankingVia1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RankingVia2",
                table: "RegistroResultados",
                newName: "Intento2");

            migrationBuilder.RenameColumn(
                name: "RankingVia1",
                table: "RegistroResultados",
                newName: "Intento1");

            migrationBuilder.RenameColumn(
                name: "PuntajeFinalVia",
                table: "RegistroResultados",
                newName: "PorcentajeAlcanzado3");

            migrationBuilder.AddColumn<bool>(
                name: "Completado1",
                table: "RegistroResultados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Completado2",
                table: "RegistroResultados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeAlcanzado1",
                table: "RegistroResultados",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeAlcanzado2",
                table: "RegistroResultados",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UltimaPresa1",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UltimaPresa2",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UltimaPresa3",
                table: "RegistroResultados",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
