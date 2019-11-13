using Microsoft.EntityFrameworkCore.Migrations;

namespace SWHRMS.Migrations
{
    public partial class UpdateCreatorQRCodecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_QRCode_QRCodeId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_QRCodeId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "QRCodeId",
                table: "Attendance");

            migrationBuilder.AddColumn<string>(
                name: "CreatorQRCode",
                table: "Attendance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorQRCode",
                table: "Attendance");

            migrationBuilder.AddColumn<long>(
                name: "QRCodeId",
                table: "Attendance",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_QRCodeId",
                table: "Attendance",
                column: "QRCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_QRCode_QRCodeId",
                table: "Attendance",
                column: "QRCodeId",
                principalTable: "QRCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
