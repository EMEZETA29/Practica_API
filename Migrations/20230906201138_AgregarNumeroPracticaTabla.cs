using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practica_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroPracticaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroPracticas",
                columns: table => new
                {
                    PracticaNo = table.Column<int>(type: "int", nullable: false),
                    PracticaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroPracticas", x => x.PracticaNo);
                    table.ForeignKey(
                        name: "FK_NumeroPracticas_Practicas_PracticaId",
                        column: x => x.PracticaId,
                        principalTable: "Practicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3374), new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3325) });

            migrationBuilder.UpdateData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3380), new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3379) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroPracticas_PracticaId",
                table: "NumeroPracticas",
                column: "PracticaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroPracticas");

            migrationBuilder.UpdateData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4773), new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4717) });

            migrationBuilder.UpdateData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4779), new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4778) });
        }
    }
}
