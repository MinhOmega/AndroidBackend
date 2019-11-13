using Microsoft.EntityFrameworkCore.Migrations;

namespace SWHRMS.Migrations
{
    public partial class AddskillcolorcodeLevelProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Position");

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Skill",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Level",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Level");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Position",
                nullable: false,
                defaultValue: false);
        }
    }
}
