using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class CambioIdSede : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencias_Sedes_CompetenciaSedeIdSede",
                table: "Competencias");

            migrationBuilder.DropIndex(
                name: "IX_Competencias_CompetenciaSedeIdSede",
                table: "Competencias");

            migrationBuilder.DropColumn(
                name: "CompetenciaSedeIdSede",
                table: "Competencias");

            migrationBuilder.CreateIndex(
                name: "IX_Competencias_IdSede",
                table: "Competencias",
                column: "IdSede");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencias_Sedes_IdSede",
                table: "Competencias",
                column: "IdSede",
                principalTable: "Sedes",
                principalColumn: "IdSede");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencias_Sedes_IdSede",
                table: "Competencias");

            migrationBuilder.DropIndex(
                name: "IX_Competencias_IdSede",
                table: "Competencias");

            migrationBuilder.AddColumn<int>(
                name: "CompetenciaSedeIdSede",
                table: "Competencias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competencias_CompetenciaSedeIdSede",
                table: "Competencias",
                column: "CompetenciaSedeIdSede");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencias_Sedes_CompetenciaSedeIdSede",
                table: "Competencias",
                column: "CompetenciaSedeIdSede",
                principalTable: "Sedes",
                principalColumn: "IdSede");
        }
    }
}
