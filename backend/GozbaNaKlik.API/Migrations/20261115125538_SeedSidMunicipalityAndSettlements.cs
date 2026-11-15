using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GozbaNaKlik.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedSidMunicipalityAndSettlements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Šid" });

            migrationBuilder.InsertData(
                table: "Settlements",
                columns: new[] { "Id", "MunicipalityId", "Name" },
                values: new object[,]
                {
                    { 1001, 1, "Šid" },
                    { 1002, 1, "Erdevik" },
                    { 1003, 1, "Kukujevci" },
                    { 1004, 1, "Adaševci" },
                    { 1005, 1, "Morović" },
                    { 1006, 1, "Višnjićevo" },
                    { 1007, 1, "Vašica" },
                    { 1008, 1, "Bačinci" },
                    { 1009, 1, "Berkasovo" },
                    { 1010, 1, "Gibarac" },
                    { 1011, 1, "Jamena" },
                    { 1012, 1, "Bingula" },
                    { 1013, 1, "Ilinci" },
                    { 1014, 1, "Sot" },
                    { 1015, 1, "Ljuba" },
                    { 1016, 1, "Bikić Do" },
                    { 1017, 1, "Batrovci" },
                    { 1018, 1, "Molovin" },
                    { 1019, 1, "Privina Glava" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "Settlements",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
