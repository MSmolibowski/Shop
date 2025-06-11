using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameOfSuppTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_category_category_id",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_products_product_id",
                table: "product_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_categories",
                table: "product_categories");

            migrationBuilder.RenameTable(
                name: "product_categories",
                newName: "products_categories");

            migrationBuilder.RenameIndex(
                name: "IX_product_categories_category_id",
                table: "products_categories",
                newName: "IX_products_categories_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories",
                columns: new[] { "product_id", "category_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_category_category_id",
                table: "products_categories",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_products_product_id",
                table: "products_categories",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_category_category_id",
                table: "products_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_products_product_id",
                table: "products_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories");

            migrationBuilder.RenameTable(
                name: "products_categories",
                newName: "product_categories");

            migrationBuilder.RenameIndex(
                name: "IX_products_categories_category_id",
                table: "product_categories",
                newName: "IX_product_categories_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_categories",
                table: "product_categories",
                columns: new[] { "product_id", "category_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_category_category_id",
                table: "product_categories",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_products_product_id",
                table: "product_categories",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
