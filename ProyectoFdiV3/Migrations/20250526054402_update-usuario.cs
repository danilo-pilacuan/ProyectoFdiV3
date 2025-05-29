using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class updateusuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deportistas_Generos_IdGenNavigationIdGen",
                table: "Deportistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Deportistas_Provincias_IdProNavigationIdPro",
                table: "Deportistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Deportistas_Usuarios_IdUsuNavigationIdUsu",
                table: "Deportistas");

            migrationBuilder.DropIndex(
                name: "IX_Deportistas_IdGenNavigationIdGen",
                table: "Deportistas");

            migrationBuilder.DropColumn(
                name: "IdGenNavigationIdGen",
                table: "Deportistas");

            migrationBuilder.RenameColumn(
                name: "IdUsuNavigationIdUsu",
                table: "Deportistas",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "IdProNavigationIdPro",
                table: "Deportistas",
                newName: "IdProvincia");

            migrationBuilder.RenameIndex(
                name: "IX_Deportistas_IdUsuNavigationIdUsu",
                table: "Deportistas",
                newName: "IX_Deportistas_IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Deportistas_IdProNavigationIdPro",
                table: "Deportistas",
                newName: "IX_Deportistas_IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Deportistas_IdGen",
                table: "Deportistas",
                column: "IdGen");

            migrationBuilder.AddForeignKey(
                name: "FK_Deportistas_Generos_IdGen",
                table: "Deportistas",
                column: "IdGen",
                principalTable: "Generos",
                principalColumn: "IdGen");

            migrationBuilder.AddForeignKey(
                name: "FK_Deportistas_Provincias_IdProvincia",
                table: "Deportistas",
                column: "IdProvincia",
                principalTable: "Provincias",
                principalColumn: "IdPro");

            migrationBuilder.AddForeignKey(
                name: "FK_Deportistas_Usuarios_IdUsuario",
                table: "Deportistas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deportistas_Generos_IdGen",
                table: "Deportistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Deportistas_Provincias_IdProvincia",
                table: "Deportistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Deportistas_Usuarios_IdUsuario",
                table: "Deportistas");

            migrationBuilder.DropIndex(
                name: "IX_Deportistas_IdGen",
                table: "Deportistas");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Deportistas",
                newName: "IdUsuNavigationIdUsu");

            migrationBuilder.RenameColumn(
                name: "IdProvincia",
                table: "Deportistas",
                newName: "IdProNavigationIdPro");

            migrationBuilder.RenameIndex(
                name: "IX_Deportistas_IdUsuario",
                table: "Deportistas",
                newName: "IX_Deportistas_IdUsuNavigationIdUsu");

            migrationBuilder.RenameIndex(
                name: "IX_Deportistas_IdProvincia",
                table: "Deportistas",
                newName: "IX_Deportistas_IdProNavigationIdPro");

            migrationBuilder.AddColumn<int>(
                name: "IdGenNavigationIdGen",
                table: "Deportistas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deportistas_IdGenNavigationIdGen",
                table: "Deportistas",
                column: "IdGenNavigationIdGen");

            migrationBuilder.AddForeignKey(
                name: "FK_Deportistas_Generos_IdGenNavigationIdGen",
                table: "Deportistas",
                column: "IdGenNavigationIdGen",
                principalTable: "Generos",
                principalColumn: "IdGen");

            migrationBuilder.AddForeignKey(
                name: "FK_Deportistas_Provincias_IdProNavigationIdPro",
                table: "Deportistas",
                column: "IdProNavigationIdPro",
                principalTable: "Provincias",
                principalColumn: "IdPro");

            migrationBuilder.AddForeignKey(
                name: "FK_Deportistas_Usuarios_IdUsuNavigationIdUsu",
                table: "Deportistas",
                column: "IdUsuNavigationIdUsu",
                principalTable: "Usuarios",
                principalColumn: "IdUsu");
        }
    }
}
