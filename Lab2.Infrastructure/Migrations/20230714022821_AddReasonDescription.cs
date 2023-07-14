using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReasonDescription",
                table: "DisqualifiedLeads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonDescription",
                table: "DisqualifiedLeads");
        }
    }
}
