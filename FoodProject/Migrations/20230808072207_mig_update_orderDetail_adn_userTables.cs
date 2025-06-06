﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductProject.Migrations
{
    public partial class mig_update_orderDetail_adn_userTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_AppUserID",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_AppUserID",
                table: "OrderDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductOrderDate",
                table: "OrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductQuantity",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrderDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                table: "OrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AppUserID",
                table: "OrderDetails",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_AppUserID",
                table: "OrderDetails",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
