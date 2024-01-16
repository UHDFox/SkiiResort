using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class SkipassModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skipasses_Tariffs_TariffRecordId",
                table: "Skipasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Skipasses_Visitors_VisitorRecordId",
                table: "Skipasses");

            migrationBuilder.DropIndex(
                name: "IX_Skipasses_TariffRecordId",
                table: "Skipasses");

            migrationBuilder.DropIndex(
                name: "IX_Skipasses_VisitorRecordId",
                table: "Skipasses");

            migrationBuilder.DropColumn(
                name: "TariffRecordId",
                table: "Skipasses");

            migrationBuilder.DropColumn(
                name: "VisitorRecordId",
                table: "Skipasses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TariffRecordId",
                table: "Skipasses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitorRecordId",
                table: "Skipasses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skipasses_TariffRecordId",
                table: "Skipasses",
                column: "TariffRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Skipasses_VisitorRecordId",
                table: "Skipasses",
                column: "VisitorRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skipasses_Tariffs_TariffRecordId",
                table: "Skipasses",
                column: "TariffRecordId",
                principalTable: "Tariffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skipasses_Visitors_VisitorRecordId",
                table: "Skipasses",
                column: "VisitorRecordId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }
    }
}
