using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initaldatabase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "MethodName",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "DiscountCode",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Discounts");

            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDeliveryDays",
                table: "ShippingMethods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Discounts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Discounts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsageLimit",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Discounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CategoryID",
                table: "Discounts",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_OrderID",
                table: "Discounts",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductID",
                table: "Discounts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_UserID",
                table: "Discounts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_AspNetUsers_UserID",
                table: "Discounts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Categories_CategoryID",
                table: "Discounts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Orders_OrderID",
                table: "Discounts",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Products_ProductID",
                table: "Discounts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_AspNetUsers_UserID",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Categories_CategoryID",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Orders_OrderID",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Products_ProductID",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_CategoryID",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_OrderID",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_ProductID",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_UserID",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryDays",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "UsageLimit",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Discounts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ShippingMethods",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MethodName",
                table: "ShippingMethods",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Discounts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiscountCode",
                table: "Discounts",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Discounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Discounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
