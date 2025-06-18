using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class FinalMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PuntajePrevio",
                table: "RegistroResultados",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntajePrevio",
                table: "RegistroResultados");
        }
    }
}
