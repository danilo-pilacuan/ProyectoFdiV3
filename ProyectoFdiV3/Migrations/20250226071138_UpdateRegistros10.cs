using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPresas",
                table: "RegistroResultados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumPresas",
                table: "Competencias",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPresas",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "NumPresas",
                table: "Competencias");
        }
    }
}
