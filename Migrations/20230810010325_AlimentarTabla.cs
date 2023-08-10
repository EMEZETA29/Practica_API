using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practica_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4773), new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4717) });

            migrationBuilder.InsertData(
                table: "Practicas",
                columns: new[] { "Id", "Amenidad", "Detalle", "Espacio", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[] { 2, "", "Detalle de Practica dos", 20.0, new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4779), new DateTime(2023, 8, 9, 21, 3, 25, 807, DateTimeKind.Local).AddTicks(4778), "", "Practica dos", 20, 20.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 9, 19, 53, 58, 4, DateTimeKind.Local).AddTicks(9186), new DateTime(2023, 8, 9, 19, 53, 58, 4, DateTimeKind.Local).AddTicks(9146) });
        }
    }
}
