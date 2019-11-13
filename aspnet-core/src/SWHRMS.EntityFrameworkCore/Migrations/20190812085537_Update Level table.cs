using Microsoft.EntityFrameworkCore.Migrations;

namespace SWHRMS.Migrations
{
    public partial class UpdateLeveltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Level");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Level",
                nullable: false,
                defaultValue: false);
        }
    }
}
