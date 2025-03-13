using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class updateRegistroRes21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FallRegistro1",
                table: "RegistroResultados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FallRegistro2",
                table: "RegistroResultados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SalidaFalse",
                table: "RegistroResultados",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FallRegistro1",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "FallRegistro2",
                table: "RegistroResultados");

            migrationBuilder.DropColumn(
                name: "SalidaFalse",
                table: "RegistroResultados");
        }
    }
}
