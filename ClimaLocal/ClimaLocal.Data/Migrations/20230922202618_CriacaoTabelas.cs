using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimaLocal.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clima",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Condicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondicaoDescricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemperaturaMin = table.Column<int>(type: "int", nullable: false),
                    TemperaturaMax = table.Column<int>(type: "int", nullable: false),
                    Temperatura = table.Column<float>(type: "real", nullable: false),
                    IndiceUv = table.Column<int>(type: "int", nullable: false),
                    PressaoAtmosferica = table.Column<int>(type: "int", nullable: true),
                    Visibilidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vento = table.Column<int>(type: "int", nullable: true),
                    DirecaoVento = table.Column<int>(type: "int", nullable: true),
                    Umidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clima", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrevisaoClimaAeroporto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoIcao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClimaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevisaoClimaAeroporto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrevisaoClimaAeroporto_Clima_ClimaId",
                        column: x => x.ClimaId,
                        principalTable: "Clima",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrevisaoClimaCidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CidadeId = table.Column<int>(type: "int", nullable: false),
                    ClimaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevisaoClimaCidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrevisaoClimaCidade_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrevisaoClimaCidade_Clima_ClimaId",
                        column: x => x.ClimaId,
                        principalTable: "Clima",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrevisaoClimaAeroporto_ClimaId",
                table: "PrevisaoClimaAeroporto",
                column: "ClimaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrevisaoClimaCidade_CidadeId",
                table: "PrevisaoClimaCidade",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrevisaoClimaCidade_ClimaId",
                table: "PrevisaoClimaCidade",
                column: "ClimaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "PrevisaoClimaAeroporto");

            migrationBuilder.DropTable(
                name: "PrevisaoClimaCidade");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Clima");
        }
    }
}
