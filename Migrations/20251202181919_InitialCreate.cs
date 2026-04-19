using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoAir.EnergyApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leituras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnidadeConsumidoraId = table.Column<int>(type: "int", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsumoKwh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoEstimado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FonteEnergia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leituras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leituras_Unidades_UnidadeConsumidoraId",
                        column: x => x.UnidadeConsumidoraId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Limites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnidadeConsumidoraId = table.Column<int>(type: "int", nullable: false),
                    LimiteDiarioKwh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LimiteMensalKwh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Limites_Unidades_UnidadeConsumidoraId",
                        column: x => x.UnidadeConsumidoraId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnidadeConsumidoraId = table.Column<int>(type: "int", nullable: false),
                    LeituraEnergiaId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resolvido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Leituras_LeituraEnergiaId",
                        column: x => x.LeituraEnergiaId,
                        principalTable: "Leituras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Alertas_Unidades_UnidadeConsumidoraId",
                        column: x => x.UnidadeConsumidoraId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_LeituraEnergiaId",
                table: "Alertas",
                column: "LeituraEnergiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_UnidadeConsumidoraId",
                table: "Alertas",
                column: "UnidadeConsumidoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_UnidadeConsumidoraId",
                table: "Leituras",
                column: "UnidadeConsumidoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Limites_UnidadeConsumidoraId",
                table: "Limites",
                column: "UnidadeConsumidoraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Limites");

            migrationBuilder.DropTable(
                name: "Leituras");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
