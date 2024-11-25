using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiiResort.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:operation_type", "positive,negative")
                .Annotation("Npgsql:Enum:user_role", "super_admin,high_level_admin,low_level_admin,user")
                .OldAnnotation("Npgsql:Enum:operation_type", "positive,negative");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VisitorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_VisitorId",
                table: "Users",
                column: "VisitorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:operation_type", "positive,negative")
                .OldAnnotation("Npgsql:Enum:operation_type", "positive,negative")
                .OldAnnotation("Npgsql:Enum:user_role", "super_admin,high_level_admin,low_level_admin,user");
        }
    }
}
