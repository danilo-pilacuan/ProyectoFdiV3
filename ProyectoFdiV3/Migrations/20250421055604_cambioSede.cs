using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class cambioSede : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencias_Sedes_IdSedeNavigationIdSede",
                table: "Competencias");

            migrationBuilder.RenameColumn(
                name: "IdSedeNavigationIdSede",
                table: "Competencias",
                newName: "CompetenciaSedeIdSede");

            migrationBuilder.RenameIndex(
                name: "IX_Competencias_IdSedeNavigationIdSede",
                table: "Competencias",
                newName: "IX_Competencias_CompetenciaSedeIdSede");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencias_Sedes_CompetenciaSedeIdSede",
                table: "Competencias",
                column: "CompetenciaSedeIdSede",
                principalTable: "Sedes",
                principalColumn: "IdSede");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencias_Sedes_CompetenciaSedeIdSede",
                table: "Competencias");

            migrationBuilder.RenameColumn(
                name: "CompetenciaSedeIdSede",
                table: "Competencias",
                newName: "IdSedeNavigationIdSede");

            migrationBuilder.RenameIndex(
                name: "IX_Competencias_CompetenciaSedeIdSede",
                table: "Competencias",
                newName: "IX_Competencias_IdSedeNavigationIdSede");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencias_Sedes_IdSedeNavigationIdSede",
                table: "Competencias",
                column: "IdSedeNavigationIdSede",
                principalTable: "Sedes",
                principalColumn: "IdSede");
        }
    }
}
