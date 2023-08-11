using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bechelor.Infrastructure.Migrations
{
    public partial class ExtenBaseEntityTanent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Tanents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Tanents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Tanents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Tanents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "Tanents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TanentId",
                table: "Tanents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Tanents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Tanents",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tanents");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Tanents");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Tanents");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Tanents");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "Tanents");

            migrationBuilder.DropColumn(
                name: "TanentId",
                table: "Tanents");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Tanents");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Tanents");
        }
    }
}
