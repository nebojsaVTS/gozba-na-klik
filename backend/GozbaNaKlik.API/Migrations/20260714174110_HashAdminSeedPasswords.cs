using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GozbaNaKlik.API.Migrations
{
    /// <inheritdoc />
    public partial class HashAdminSeedPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKSqiTVL7qi6w2Gudvv5zFdzM2mFmGThBwj1by8cRHcWE4K76EWw77qSAK1J/nFvuA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKSqiTVL7qi6w2Gudvv5zFdzM2mFmGThBwj1by8cRHcWE4K76EWw77qSAK1J/nFvuA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKSqiTVL7qi6w2Gudvv5zFdzM2mFmGThBwj1by8cRHcWE4K76EWw77qSAK1J/nFvuA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "Password",
                value: "admin123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "Password",
                value: "admin123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                column: "Password",
                value: "admin123");
        }
    }
}
