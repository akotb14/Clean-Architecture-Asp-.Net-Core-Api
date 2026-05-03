using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateshippingorder1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Region",
                table: "ShippingMethods",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "Carrier",
                table: "ShippingMethods",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "ShippingMethods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethods_OrderID",
                table: "ShippingMethods",
                column: "OrderID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingMethods_Orders_OrderID",
                table: "ShippingMethods",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingMethods_Orders_OrderID",
                table: "ShippingMethods");

            migrationBuilder.DropIndex(
                name: "IX_ShippingMethods_OrderID",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ShippingMethods");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ShippingMethods");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "ShippingMethods",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "ShippingMethods",
                newName: "Carrier");
        }
    }
}
