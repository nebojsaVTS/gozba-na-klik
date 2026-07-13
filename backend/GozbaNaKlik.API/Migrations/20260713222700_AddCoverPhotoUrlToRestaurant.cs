using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GozbaNaKlik.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverPhotoUrlToRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPhotoUrl",
                table: "Restaurants",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPhotoUrl",
                table: "Restaurants");
        }
    }
}
