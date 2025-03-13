using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class updateRegistroRes22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumPresasR1ClasifVias",
                table: "Competencias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumPresasR1FinalVias",
                table: "Competencias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumPresasR2ClasifVias",
                table: "Competencias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumPresasR2FinalVias",
                table: "Competencias",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPresasR1ClasifVias",
                table: "Competencias");

            migrationBuilder.DropColumn(
                name: "NumPresasR1FinalVias",
                table: "Competencias");

            migrationBuilder.DropColumn(
                name: "NumPresasR2ClasifVias",
                table: "Competencias");

            migrationBuilder.DropColumn(
                name: "NumPresasR2FinalVias",
                table: "Competencias");
        }
    }
}
