using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class updateRegs19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxPresas",
                table: "RegistroResultados",
                newName: "MaxPresas2");

            migrationBuilder.AddColumn<int>(
                name: "MaxPresas1",
                table: "RegistroResultados",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPresas1",
                table: "RegistroResultados");

            migrationBuilder.RenameColumn(
                name: "MaxPresas2",
                table: "RegistroResultados",
                newName: "MaxPresas");
        }
    }
}
