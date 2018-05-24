using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MiroBello.Migrations
{
    public partial class AddTotalPricePerProductAndOthers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPricePerProduct",
                table: "ProductsOnCart",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPriceOfCartForUser",
                table: "ClientCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPricePerProduct",
                table: "ProductsOnCart");

            migrationBuilder.DropColumn(
                name: "TotalPriceOfCartForUser",
                table: "ClientCart");
        }
    }
}
