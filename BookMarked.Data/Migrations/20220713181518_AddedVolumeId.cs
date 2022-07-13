using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarked.Data.Migrations
{
    public partial class AddedVolumeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VolumeId",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VolumeId",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "VolumeId",
                table: "Ratings");
        }
    }
}
