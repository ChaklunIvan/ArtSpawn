using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSpawn.Database.Migrations
{
    public partial class add_roles_toDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "07979afb-8392-4522-b40f-e5703ac3812d", "bc5cc19d-9242-4b8b-9c83-57b25dedc268", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68bc5d30-2fa8-4239-8fc5-a1d960419fa3", "0ececea1-50c9-41d7-bfdd-2cf6503f6184", "artist", "ARTIST" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07979afb-8392-4522-b40f-e5703ac3812d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68bc5d30-2fa8-4239-8fc5-a1d960419fa3");
        }
    }
}
