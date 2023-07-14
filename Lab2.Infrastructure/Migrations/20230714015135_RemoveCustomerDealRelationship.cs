using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomerDealRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Accounts_CustomerId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_CustomerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Deals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CustomerId",
                table: "Deals",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Accounts_CustomerId",
                table: "Deals",
                column: "CustomerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
