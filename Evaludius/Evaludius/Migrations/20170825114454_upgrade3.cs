using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evaludius.Migrations
{
    public partial class upgrade3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssessmentDate",
                table: "Assessment",
                newName: "StartDate");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assessment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Assessment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assessment");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Assessment");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Assessment",
                newName: "AssessmentDate");
        }
    }
}
