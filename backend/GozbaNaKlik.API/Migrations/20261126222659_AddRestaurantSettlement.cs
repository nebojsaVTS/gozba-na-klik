using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GozbaNaKlik.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantSettlement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SettlementId",
                table: "Restaurants",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_SettlementId",
                table: "Restaurants",
                column: "SettlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Settlements_SettlementId",
                table: "Restaurants",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Settlements_SettlementId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_SettlementId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Restaurants");
        }
    }
}
