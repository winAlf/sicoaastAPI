using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sicoaastAPI.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organismos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    empresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organismos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organismos_Empresas_empresaId",
                        column: x => x.empresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ccostos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    organismoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ccostos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ccostos_Organismos_organismoId",
                        column: x => x.organismoId,
                        principalTable: "Organismos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ccostoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamentos_Ccostos_ccostoId",
                        column: x => x.ccostoId,
                        principalTable: "Ccostos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovEmp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    activo = table.Column<int>(type: "int", nullable: false),
                    numEmp = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Folio = table.Column<int>(type: "int", nullable: false),
                    Nip = table.Column<int>(type: "int", nullable: true),
                    RutaImagen = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Genero = table.Column<int>(type: "int", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaReactivacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    empresaId = table.Column<int>(type: "int", nullable: false),
                    organismoId = table.Column<int>(type: "int", nullable: false),
                    ccostoId = table.Column<int>(type: "int", nullable: false),
                    departamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovEmp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovEmp_Ccostos_ccostoId",
                        column: x => x.ccostoId,
                        principalTable: "Ccostos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovEmp_Departamentos_departamentoId",
                        column: x => x.departamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovEmp_Empresas_empresaId",
                        column: x => x.empresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovEmp_Organismos_organismoId",
                        column: x => x.organismoId,
                        principalTable: "Organismos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ccostos_organismoId",
                table: "Ccostos",
                column: "organismoId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_ccostoId",
                table: "Departamentos",
                column: "ccostoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovEmp_ccostoId",
                table: "MovEmp",
                column: "ccostoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovEmp_departamentoId",
                table: "MovEmp",
                column: "departamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovEmp_empresaId",
                table: "MovEmp",
                column: "empresaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovEmp_organismoId",
                table: "MovEmp",
                column: "organismoId");

            migrationBuilder.CreateIndex(
                name: "IX_Organismos_empresaId",
                table: "Organismos",
                column: "empresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovEmp");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Ccostos");

            migrationBuilder.DropTable(
                name: "Organismos");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
