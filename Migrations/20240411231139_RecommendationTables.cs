using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildsByBrickwellNew.Migrations
{
    /// <inheritdoc />
    public partial class RecommendationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auth_new_user_rec",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img_link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_new_user_rec", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "Customer2_rec",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rec_1 = table.Column<int>(type: "int", nullable: true),
                    rec_2 = table.Column<int>(type: "int", nullable: true),
                    rec_3 = table.Column<int>(type: "int", nullable: true),
                    rec_4 = table.Column<int>(type: "int", nullable: true),
                    rec_5 = table.Column<int>(type: "int", nullable: true),
                    rec_6 = table.Column<int>(type: "int", nullable: true),
                    rec_7 = table.Column<int>(type: "int", nullable: true),
                    rec_8 = table.Column<int>(type: "int", nullable: true),
                    rec_9 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer2_rec", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "high_rated_rec",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating = table.Column<float>(type: "real", nullable: true),
                    qty = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img_link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_high_rated_rec", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "item_based_rec",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recommended_product_ID_1 = table.Column<int>(type: "int", nullable: true),
                    recommended_product_ID_2 = table.Column<int>(type: "int", nullable: true),
                    recommended_product_ID_3 = table.Column<int>(type: "int", nullable: true),
                    recommended_product_ID_4 = table.Column<int>(type: "int", nullable: true),
                    recommended_product_ID_5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_based_rec", x => x.product_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth_new_user_rec");

            migrationBuilder.DropTable(
                name: "Customer2_rec");

            migrationBuilder.DropTable(
                name: "high_rated_rec");

            migrationBuilder.DropTable(
                name: "item_based_rec");
        }
    }
}
