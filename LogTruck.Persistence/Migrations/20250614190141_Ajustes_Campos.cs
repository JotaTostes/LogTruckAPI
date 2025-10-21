using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogTruck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Ajustes_Campos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Viagens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Viagens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracaoId",
                table: "Viagens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Usuarios",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracaoId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Motoristas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracaoId",
                table: "Motoristas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "CustosViagem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "CustosViagem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracaoId",
                table: "CustosViagem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Comissoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Comissoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracaoId",
                table: "Comissoes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Caminhao",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracaoId",
                table: "Caminhao",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Motoristas");

            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "CustosViagem");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "CustosViagem");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "CustosViagem");

            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Caminhao");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Motoristas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Caminhao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
