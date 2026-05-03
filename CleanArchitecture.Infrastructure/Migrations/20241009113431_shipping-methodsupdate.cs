using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shippingmethodsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "ShippingMethods",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "ShippingMethods",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ShippingMethods",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "ShippingMethods",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ShippingMethods",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ShippingMethods",
                newName: "Name");
        }
    }
}
