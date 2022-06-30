using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSpawn.Database.Migrations
{
    public partial class fluentapi_config1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "category",
                newName: "category_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category_type",
                table: "category",
                newName: "category_name");
        }
    }
}
