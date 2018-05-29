using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MiroBello.Migrations
{
    public partial class ModifyProductsOnBillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "ProductsOnBill",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPricePerProduct",
                table: "ProductsOnBill",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductsOnBill");

            migrationBuilder.DropColumn(
                name: "TotalPricePerProduct",
                table: "ProductsOnBill");
        }
    }
}
