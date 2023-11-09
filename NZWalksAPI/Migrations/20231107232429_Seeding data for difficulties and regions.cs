using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("356a7dd2-3aa8-4a8e-90e0-78e787fd05e7"), "Easy" },
                    { new Guid("cfa401e9-493c-435d-92e7-ef1319ff3306"), "Hard" },
                    { new Guid("e03d4053-c7e5-4327-897a-dc390e419a58"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[] { new Guid("2da9cfbb-5753-4abe-81e3-9e576e3167a3"), "AKL", "Auckland", "https://Demo.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("356a7dd2-3aa8-4a8e-90e0-78e787fd05e7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cfa401e9-493c-435d-92e7-ef1319ff3306"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e03d4053-c7e5-4327-897a-dc390e419a58"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2da9cfbb-5753-4abe-81e3-9e576e3167a3"));
        }
    }
}
