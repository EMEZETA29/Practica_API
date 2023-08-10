using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practica_API.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Practicas",
                columns: new[] { "Id", "Amenidad", "Detalle", "Espacio", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[] { 1, "", "Detalle de Practica uno", 10.0, new DateTime(2023, 8, 9, 19, 53, 58, 4, DateTimeKind.Local).AddTicks(9186), new DateTime(2023, 8, 9, 19, 53, 58, 4, DateTimeKind.Local).AddTicks(9146), "", "Practica uno", 10, 10.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Practicas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
