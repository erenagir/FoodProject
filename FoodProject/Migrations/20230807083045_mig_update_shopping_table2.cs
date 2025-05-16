using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductProject.Migrations
{
    public partial class mig_update_shopping_table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
