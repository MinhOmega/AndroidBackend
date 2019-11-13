using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SWHRMS.Migrations
{
    public partial class AddUserInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountName",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyEmail",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractType",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IdentityCardIssueDate",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardIssuePlace",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OfficialStartDate",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrialStartDate",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkingStatus",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CompanyEmail",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "IdentityCardIssueDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "IdentityCardIssuePlace",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "OfficialStartDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "TrialStartDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "WorkingStatus",
                table: "AbpUsers");
        }
    }
}
