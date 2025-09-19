using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Location", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Dubai Marina", "Sunshine Apartments", 500000m },
                    { 2, "Palm Jumeirah", "Palm Villas", 1200000m },
                    { 3, "Business Bay", "Green Park Residence", 750000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
