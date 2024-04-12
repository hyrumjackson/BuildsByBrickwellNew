using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildsByBrickwellNew.Migrations
{
    /// <inheritdoc />
    public partial class AdjustHighRatedRecModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "product_id",
                table: "high_rated_rec",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "product_id",
                table: "high_rated_rec",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
