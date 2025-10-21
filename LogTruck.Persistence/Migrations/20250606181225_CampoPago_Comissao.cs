using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogTruck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CampoPago_Comissao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Comissoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Comissoes");
        }
    }
}
