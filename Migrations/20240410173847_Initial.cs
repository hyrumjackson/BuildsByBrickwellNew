using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildsByBrickwellNew.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_ID = table.Column<int>(type: "int", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birth_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country_of_residence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    transaction_ID = table.Column<int>(type: "int", nullable: true),
                    product_ID = table.Column<int>(type: "int", nullable: true),
                    qty = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    transaction_ID = table.Column<int>(type: "int", nullable: true),
                    customer_ID = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    day_of_week = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time = table.Column<int>(type: "int", nullable: true),
                    entry_mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<double>(type: "float", nullable: true),
                    type_of_transaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country_of_transaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shipping_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type_of_card = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fraud = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    product_ID = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    year = table.Column<int>(type: "int", nullable: true),
                    num_parts = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    img_link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    primary_color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    secondary_color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    secondary_category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tertiary_category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
