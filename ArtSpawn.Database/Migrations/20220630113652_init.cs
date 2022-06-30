using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSpawn.Database.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_artist_artist_name",
                table: "artist",
                column: "artist_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_artist_artist_name",
                table: "artist");
        }
    }
}
