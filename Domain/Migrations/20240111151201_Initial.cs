﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Passport = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skipasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<int>(type: "integer", nullable: false),
                    TariffId = table.Column<Guid>(type: "uuid", nullable: false),
                    TariffRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    VisitorId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitorRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skipasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skipasses_Tariffs_TariffRecordId",
                        column: x => x.TariffRecordId,
                        principalTable: "Tariffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Skipasses_Visitors_VisitorRecordId",
                        column: x => x.VisitorRecordId,
                        principalTable: "Visitors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skipasses_TariffRecordId",
                table: "Skipasses",
                column: "TariffRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Skipasses_VisitorRecordId",
                table: "Skipasses",
                column: "VisitorRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skipasses");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "Visitors");
        }
    }
}