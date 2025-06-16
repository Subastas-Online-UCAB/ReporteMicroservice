using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReporteService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialNuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_reportePrueba",
                table: "reportePrueba");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "reportePrueba");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "reportePrueba");

            migrationBuilder.RenameTable(
                name: "reportePrueba",
                newName: "Reporte");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reporte",
                newName: "IdUsuario");

            migrationBuilder.AddColumn<Guid>(
                name: "IdReporte",
                table: "Reporte",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Reporte",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Reporte",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reporte",
                table: "Reporte",
                column: "IdReporte");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reporte",
                table: "Reporte");

            migrationBuilder.DropColumn(
                name: "IdReporte",
                table: "Reporte");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Reporte");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Reporte");

            migrationBuilder.RenameTable(
                name: "Reporte",
                newName: "reportePrueba");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "reportePrueba",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "reportePrueba",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "reportePrueba",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reportePrueba",
                table: "reportePrueba",
                column: "Id");
        }
    }
}
