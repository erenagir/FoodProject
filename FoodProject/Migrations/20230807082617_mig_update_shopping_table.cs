using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductProject.Migrations
{
    public partial class mig_update_shopping_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Products_ProductID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ProductID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PaymentId",
                table: "Products",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Payments_PaymentId",
                table: "Products",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Payments_PaymentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PaymentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ProductID",
                table: "Payments",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Products_ProductID",
                table: "Payments",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
