using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogTruck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CampoDataPagamento_Comissao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataPagamento",
                table: "Comissoes",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Comissoes");
        }
    }
}
