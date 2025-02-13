using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class AddPropsRegistroResultado3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UltimaPresa",
                table: "RegistroResultados",
                newName: "UltimaPresa3");

            migrationBuilder.RenameColumn(
                name: "Tiempo",
                table: "RegistroResultados",
                newName: "Tiempo2");

            migrationBuilder.RenameColumn(
                name: "PorcentajeAlcanzado",
                table: "RegistroResultados",
                newName: "PorcentajeAlcanzado3");

            migrationBuilder.RenameColumn(
                name: "MaxEscala",
                table: "RegistroResultados",
                newName: "MaxEscala3");

            migrationBuilder.RenameColumn(
                name: "Intento",
                table: "RegistroResultados",
                newName: "MaxEscala2");

            migrationBuilder.RenameColumn(
                name: "Completado",
                table: "RegistroResultados",
                newName: "Completado3");

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

            migrationBuilder.AddColumn<int>(
                name: "Intento1",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Intento2",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Intento3",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxEscala1",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddColumn<float>(
                name: "Tiempo1",
                table: "RegistroResultados",
                type: "real",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completado1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Completado2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Intento1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Intento2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Intento3",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "MaxEscala1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "PorcentajeAlcanzado1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "PorcentajeAlcanzado2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "Tiempo1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "UltimaPresa1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "UltimaPresa2",
                table: "RegistroResultados");

            migrationBuilder.RenameColumn(
                name: "UltimaPresa3",
                table: "RegistroResultados",
                newName: "UltimaPresa");

            migrationBuilder.RenameColumn(
                name: "Tiempo2",
                table: "RegistroResultados",
                newName: "Tiempo");

            migrationBuilder.RenameColumn(
                name: "PorcentajeAlcanzado3",
                table: "RegistroResultados",
                newName: "PorcentajeAlcanzado");

            migrationBuilder.RenameColumn(
                name: "MaxEscala3",
                table: "RegistroResultados",
                newName: "MaxEscala");

            migrationBuilder.RenameColumn(
                name: "MaxEscala2",
                table: "RegistroResultados",
                newName: "Intento");

            migrationBuilder.RenameColumn(
                name: "Completado3",
                table: "RegistroResultados",
                newName: "Completado");
        }
    }
}
