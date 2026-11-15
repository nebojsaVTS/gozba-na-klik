using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GozbaNaKlik.API.Migrations
{
    /// <inheritdoc />
    public partial class AddHouseNumberToAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Addresses");
        }
    }
}
