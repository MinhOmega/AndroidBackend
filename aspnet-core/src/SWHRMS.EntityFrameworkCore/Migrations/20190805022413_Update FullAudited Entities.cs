using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SWHRMS.Migrations
{
    public partial class UpdateFullAuditedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "QRCode",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "QRCode",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "QRCode",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QRCode",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "QRCode",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "QRCode",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Position",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Position",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Position",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Position",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Position",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Position",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Branch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Attendance",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Attendance",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Attendance",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Attendance",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Attendance",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Attendance",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Absence",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Absence",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Absence",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Absence",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Absence",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Absence",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "QRCode");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "QRCode");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "QRCode");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QRCode");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "QRCode");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "QRCode");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Absence");
        }
    }
}
