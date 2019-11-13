using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SWHRMS.Migrations
{
    public partial class UpdateFullAuditforMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "UserMetaInfos",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "UserMetaInfoDetails",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "UserMetaInfos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "UserMetaInfos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "UserMetaInfos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "UserMetaInfos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "UserMetaInfos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "UserMetaInfoDetails",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "UserMetaInfoDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "UserMetaInfoDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "UserMetaInfoDetails",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "UserMetaInfoDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "UserMetaInfos");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "UserMetaInfos");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "UserMetaInfos");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "UserMetaInfos");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "UserMetaInfos");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "UserMetaInfoDetails");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "UserMetaInfoDetails");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "UserMetaInfoDetails");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "UserMetaInfoDetails");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "UserMetaInfoDetails");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "UserMetaInfos",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "UserMetaInfoDetails",
                newName: "IsActive");
        }
    }
}
