using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class UpdateRegistros7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "LabelMaxEscala1",
                table: "RegistroResultados",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "LabelMaxEscala2",
                table: "RegistroResultados",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelMaxEscala1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "LabelMaxEscala2",
                table: "RegistroResultados");
        }
    }
}
