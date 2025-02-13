using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFdiV3.Migrations
{
    public partial class InitialM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCat);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    IdClub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreClub = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.IdClub);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    IdGen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.IdGen);
                });

            migrationBuilder.CreateTable(
                name: "Modalidades",
                columns: table => new
                {
                    IdMod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionMod = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modalidades", x => x.IdMod);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    IdPro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.IdPro);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    IdSede = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSede = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.IdSede);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveUsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RolesUsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivoUsu = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsu);
                });

            migrationBuilder.CreateTable(
                name: "Entrenadores",
                columns: table => new
                {
                    IdEnt = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombresEnt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApellidosEnt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CedulaEnt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivoEnt = table.Column<bool>(type: "bit", nullable: true),
                    IdPro = table.Column<int>(type: "int", nullable: true),
                    IdProNavigationIdPro = table.Column<int>(type: "int", nullable: true),
                    IdUsuNavigationIdUsu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrenadores", x => x.IdEnt);
                    table.ForeignKey(
                        name: "FK_Entrenadores_Provincias_IdProNavigationIdPro",
                        column: x => x.IdProNavigationIdPro,
                        principalTable: "Provincias",
                        principalColumn: "IdPro");
                    table.ForeignKey(
                        name: "FK_Entrenadores_Usuarios_IdUsuNavigationIdUsu",
                        column: x => x.IdUsuNavigationIdUsu,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsu");
                });

            migrationBuilder.CreateTable(
                name: "Jueces",
                columns: table => new
                {
                    IdJuez = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombresJuez = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApellidosJuez = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CedulaJuez = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrincipalJuez = table.Column<bool>(type: "bit", nullable: true),
                    ActivoJuez = table.Column<bool>(type: "bit", nullable: true),
                    IdPro = table.Column<int>(type: "int", nullable: true),
                    IdProNavigationIdPro = table.Column<int>(type: "int", nullable: true),
                    IdUsuNavigationIdUsu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jueces", x => x.IdJuez);
                    table.ForeignKey(
                        name: "FK_Jueces_Provincias_IdProNavigationIdPro",
                        column: x => x.IdProNavigationIdPro,
                        principalTable: "Provincias",
                        principalColumn: "IdPro");
                    table.ForeignKey(
                        name: "FK_Jueces_Usuarios_IdUsuNavigationIdUsu",
                        column: x => x.IdUsuNavigationIdUsu,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsu");
                });

            migrationBuilder.CreateTable(
                name: "Deportistas",
                columns: table => new
                {
                    IdDep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombresDep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApellidosDep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CedulaDep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivoDep = table.Column<bool>(type: "bit", nullable: true),
                    IdGen = table.Column<int>(type: "int", nullable: true),
                    IdClub = table.Column<int>(type: "int", nullable: true),
                    IdEnt = table.Column<int>(type: "int", nullable: true),
                    IdGenNavigationIdGen = table.Column<int>(type: "int", nullable: true),
                    IdProNavigationIdPro = table.Column<int>(type: "int", nullable: true),
                    IdUsuNavigationIdUsu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deportistas", x => x.IdDep);
                    table.ForeignKey(
                        name: "FK_Deportistas_Clubs_IdClub",
                        column: x => x.IdClub,
                        principalTable: "Clubs",
                        principalColumn: "IdClub");
                    table.ForeignKey(
                        name: "FK_Deportistas_Entrenadores_IdEnt",
                        column: x => x.IdEnt,
                        principalTable: "Entrenadores",
                        principalColumn: "IdEnt");
                    table.ForeignKey(
                        name: "FK_Deportistas_Generos_IdGenNavigationIdGen",
                        column: x => x.IdGenNavigationIdGen,
                        principalTable: "Generos",
                        principalColumn: "IdGen");
                    table.ForeignKey(
                        name: "FK_Deportistas_Provincias_IdProNavigationIdPro",
                        column: x => x.IdProNavigationIdPro,
                        principalTable: "Provincias",
                        principalColumn: "IdPro");
                    table.ForeignKey(
                        name: "FK_Deportistas_Usuarios_IdUsuNavigationIdUsu",
                        column: x => x.IdUsuNavigationIdUsu,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsu");
                });

            migrationBuilder.CreateTable(
                name: "Competencias",
                columns: table => new
                {
                    IdCom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicioCom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinCom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivoCom = table.Column<bool>(type: "bit", nullable: true),
                    IdJuez = table.Column<int>(type: "int", nullable: true),
                    IdSede = table.Column<int>(type: "int", nullable: true),
                    IdMod = table.Column<int>(type: "int", nullable: true),
                    IdCatNavigationIdCat = table.Column<int>(type: "int", nullable: true),
                    IdSedeNavigationIdSede = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencias", x => x.IdCom);
                    table.ForeignKey(
                        name: "FK_Competencias_Categorias_IdCatNavigationIdCat",
                        column: x => x.IdCatNavigationIdCat,
                        principalTable: "Categorias",
                        principalColumn: "IdCat");
                    table.ForeignKey(
                        name: "FK_Competencias_Jueces_IdJuez",
                        column: x => x.IdJuez,
                        principalTable: "Jueces",
                        principalColumn: "IdJuez");
                    table.ForeignKey(
                        name: "FK_Competencias_Modalidades_IdMod",
                        column: x => x.IdMod,
                        principalTable: "Modalidades",
                        principalColumn: "IdMod");
                    table.ForeignKey(
                        name: "FK_Competencias_Sedes_IdSedeNavigationIdSede",
                        column: x => x.IdSedeNavigationIdSede,
                        principalTable: "Sedes",
                        principalColumn: "IdSede");
                });

            migrationBuilder.CreateTable(
                name: "CompetenciaDeportistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetenciaIdCom = table.Column<int>(type: "int", nullable: false),
                    DeportistaIdDep = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenciaDeportistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetenciaDeportistas_Competencias_CompetenciaIdCom",
                        column: x => x.CompetenciaIdCom,
                        principalTable: "Competencias",
                        principalColumn: "IdCom",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenciaDeportistas_Deportistas_DeportistaIdDep",
                        column: x => x.DeportistaIdDep,
                        principalTable: "Deportistas",
                        principalColumn: "IdDep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompetencias",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClasRes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OctavosRes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuartosRes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemiRes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalRes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdComNavigationIdCom = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompetencias", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_DetalleCompetencias_Competencias_IdComNavigationIdCom",
                        column: x => x.IdComNavigationIdCom,
                        principalTable: "Competencias",
                        principalColumn: "IdCom");
                });

            migrationBuilder.CreateTable(
                name: "RegistroResultado",
                columns: table => new
                {
                    IdRegistroResultado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDetalleCompetencia = table.Column<int>(type: "int", nullable: true),
                    IdDep = table.Column<int>(type: "int", nullable: true),
                    Puesto = table.Column<int>(type: "int", nullable: true),
                    Etapa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDepNavigationIdDep = table.Column<int>(type: "int", nullable: true),
                    CompetenciumNavigationIdCom = table.Column<int>(type: "int", nullable: true),
                    DetalleCompetenciumIdDetalle = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroResultado", x => x.IdRegistroResultado);
                    table.ForeignKey(
                        name: "FK_RegistroResultado_Competencias_CompetenciumNavigationIdCom",
                        column: x => x.CompetenciumNavigationIdCom,
                        principalTable: "Competencias",
                        principalColumn: "IdCom");
                    table.ForeignKey(
                        name: "FK_RegistroResultado_Deportistas_IdDepNavigationIdDep",
                        column: x => x.IdDepNavigationIdDep,
                        principalTable: "Deportistas",
                        principalColumn: "IdDep");
                    table.ForeignKey(
                        name: "FK_RegistroResultado_DetalleCompetencias_DetalleCompetenciumIdDetalle",
                        column: x => x.DetalleCompetenciumIdDetalle,
                        principalTable: "DetalleCompetencias",
                        principalColumn: "IdDetalle");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetenciaDeportistas_CompetenciaIdCom",
                table: "CompetenciaDeportistas",
                column: "CompetenciaIdCom");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenciaDeportistas_DeportistaIdDep",
                table: "CompetenciaDeportistas",
                column: "DeportistaIdDep");

            migrationBuilder.CreateIndex(
                name: "IX_Competencias_IdCatNavigationIdCat",
                table: "Competencias",
                column: "IdCatNavigationIdCat");

            migrationBuilder.CreateIndex(
                name: "IX_Competencias_IdJuez",
                table: "Competencias",
                column: "IdJuez");

            migrationBuilder.CreateIndex(
                name: "IX_Competencias_IdMod",
                table: "Competencias",
                column: "IdMod");

            migrationBuilder.CreateIndex(
                name: "IX_Competencias_IdSedeNavigationIdSede",
                table: "Competencias",
                column: "IdSedeNavigationIdSede");

            migrationBuilder.CreateIndex(
                name: "IX_Deportistas_IdClub",
                table: "Deportistas",
                column: "IdClub");

            migrationBuilder.CreateIndex(
                name: "IX_Deportistas_IdEnt",
                table: "Deportistas",
                column: "IdEnt");

            migrationBuilder.CreateIndex(
                name: "IX_Deportistas_IdGenNavigationIdGen",
                table: "Deportistas",
                column: "IdGenNavigationIdGen");

            migrationBuilder.CreateIndex(
                name: "IX_Deportistas_IdProNavigationIdPro",
                table: "Deportistas",
                column: "IdProNavigationIdPro");

            migrationBuilder.CreateIndex(
                name: "IX_Deportistas_IdUsuNavigationIdUsu",
                table: "Deportistas",
                column: "IdUsuNavigationIdUsu");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompetencias_IdComNavigationIdCom",
                table: "DetalleCompetencias",
                column: "IdComNavigationIdCom");

            migrationBuilder.CreateIndex(
                name: "IX_Entrenadores_IdProNavigationIdPro",
                table: "Entrenadores",
                column: "IdProNavigationIdPro");

            migrationBuilder.CreateIndex(
                name: "IX_Entrenadores_IdUsuNavigationIdUsu",
                table: "Entrenadores",
                column: "IdUsuNavigationIdUsu");

            migrationBuilder.CreateIndex(
                name: "IX_Jueces_IdProNavigationIdPro",
                table: "Jueces",
                column: "IdProNavigationIdPro");

            migrationBuilder.CreateIndex(
                name: "IX_Jueces_IdUsuNavigationIdUsu",
                table: "Jueces",
                column: "IdUsuNavigationIdUsu");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultado_CompetenciumNavigationIdCom",
                table: "RegistroResultado",
                column: "CompetenciumNavigationIdCom");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultado_DetalleCompetenciumIdDetalle",
                table: "RegistroResultado",
                column: "DetalleCompetenciumIdDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroResultado_IdDepNavigationIdDep",
                table: "RegistroResultado",
                column: "IdDepNavigationIdDep");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetenciaDeportistas");

            migrationBuilder.DropTable(
                name: "RegistroResultado");

            migrationBuilder.DropTable(
                name: "Deportistas");

            migrationBuilder.DropTable(
                name: "DetalleCompetencias");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Entrenadores");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Competencias");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Jueces");

            migrationBuilder.DropTable(
                name: "Modalidades");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
