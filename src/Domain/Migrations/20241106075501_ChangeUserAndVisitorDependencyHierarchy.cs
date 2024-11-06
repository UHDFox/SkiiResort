using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiiResort.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAndVisitorDependencyHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Visitors_VisitorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_VisitorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Visitors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_UserId",
                table: "Visitors",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Users_UserId",
                table: "Visitors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Users_UserId",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_UserId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Visitors");

            migrationBuilder.AddColumn<Guid>(
                name: "VisitorId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_VisitorId",
                table: "Users",
                column: "VisitorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Visitors_VisitorId",
                table: "Users",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
