using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildsByBrickwellNew.Migrations
{
    /// <inheritdoc />
    public partial class HaydenChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AspNetUserId",
                table: "Customers",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_AspNetUserId",
                table: "Customers",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
