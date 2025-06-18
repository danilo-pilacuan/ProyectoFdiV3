using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class FinalMig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PuntajePrevio",
                table: "RegistroResultados",
                type: "real",
                nullable: true,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PuntajePrevio",
                table: "RegistroResultados",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldDefaultValue: 0f);
        }
    }
}
