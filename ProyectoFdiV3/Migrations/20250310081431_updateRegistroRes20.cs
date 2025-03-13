using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class updateRegistroRes20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistroEditado",
                table: "RegistroResultados",
                newName: "RegistroEditadoT2");

            migrationBuilder.AddColumn<bool>(
                name: "RegistroEditadoT1",
                table: "RegistroResultados",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistroEditadoT1",
                table: "RegistroResultados");

            migrationBuilder.RenameColumn(
                name: "RegistroEditadoT2",
                table: "RegistroResultados",
                newName: "RegistroEditado");
        }
    }
}
