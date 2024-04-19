using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedOperationTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:operation_type", "positive,negative");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "VisitorActions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "VisitorActions");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:operation_type", "positive,negative");
        }
    }
}
