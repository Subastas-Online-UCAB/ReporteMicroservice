using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReporteService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePrueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_reporte",
                table: "reporte");

            migrationBuilder.RenameTable(
                name: "reporte",
                newName: "reportePrueba");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reportePrueba",
                table: "reportePrueba",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_reportePrueba",
                table: "reportePrueba");

            migrationBuilder.RenameTable(
                name: "reportePrueba",
                newName: "reporte");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reporte",
                table: "reporte",
                column: "Id");
        }
    }
}
