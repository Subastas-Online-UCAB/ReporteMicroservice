using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReporteService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialReporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reporte",
                table: "Reporte");

            migrationBuilder.RenameTable(
                name: "Reporte",
                newName: "Reportes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reportes",
                table: "Reportes",
                column: "IdReporte");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reportes",
                table: "Reportes");

            migrationBuilder.RenameTable(
                name: "Reportes",
                newName: "Reportes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reporte",
                table: "Reporte",
                column: "IdReporte");
        }
    }
}
