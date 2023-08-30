using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIECommerce.Data.Migrations
{
    public partial class AddBusinessTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntry_Order_OrderId",
                table: "OrderEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntry_Products_ProductId",
                table: "OrderEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderEntry",
                table: "OrderEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderEntry",
                newName: "OrderEntries");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntry_ProductId",
                table: "OrderEntries",
                newName: "IX_OrderEntries_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntry_OrderId",
                table: "OrderEntries",
                newName: "IX_OrderEntries_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderEntries",
                table: "OrderEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntries_Orders_OrderId",
                table: "OrderEntries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntries_Products_ProductId",
                table: "OrderEntries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntries_Orders_OrderId",
                table: "OrderEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntries_Products_ProductId",
                table: "OrderEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderEntries",
                table: "OrderEntries");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderEntries",
                newName: "OrderEntry");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntries_ProductId",
                table: "OrderEntry",
                newName: "IX_OrderEntry_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntries_OrderId",
                table: "OrderEntry",
                newName: "IX_OrderEntry_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderEntry",
                table: "OrderEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntry_Order_OrderId",
                table: "OrderEntry",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntry_Products_ProductId",
                table: "OrderEntry",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
