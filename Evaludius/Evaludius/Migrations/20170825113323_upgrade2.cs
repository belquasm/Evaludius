using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Evaludius.Migrations
{
    public partial class upgrade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_ApplicationUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ApplicationUserId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "TeamName",
                table: "Teams",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Teams",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Teams",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxPoints",
                table: "SkillSetItemss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentDate = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessment_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_PlayerId",
                table: "Assessment",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_ManagerId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MaxPoints",
                table: "SkillSetItemss");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teams",
                newName: "TeamName");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Teams",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Teams",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ApplicationUserId",
                table: "Teams",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_ApplicationUserId",
                table: "Teams",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
