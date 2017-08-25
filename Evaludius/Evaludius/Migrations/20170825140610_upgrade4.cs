using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Evaludius.Migrations
{
    public partial class upgrade4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillSetItemss_SkillSets_SkillSetId",
                table: "SkillSetItemss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillSetItemss",
                table: "SkillSetItemss");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "SkillSetItemss",
                newName: "SkillSetItems");

            migrationBuilder.RenameIndex(
                name: "IX_SkillSetItemss_SkillSetId",
                table: "SkillSetItems",
                newName: "IX_SkillSetItems_SkillSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillSetItems",
                table: "SkillSetItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssessmentResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<int>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true),
                    SkillSetItemId = table.Column<int>(nullable: true),
                    TotalPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentResults_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResults_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResults_SkillSetItems_SkillSetItemId",
                        column: x => x.SkillSetItemId,
                        principalTable: "SkillSetItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => new { x.TeamId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Teams_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Players_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResults_AssessmentId",
                table: "AssessmentResults",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResults_PlayerId",
                table: "AssessmentResults",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResults_SkillSetItemId",
                table: "AssessmentResults",
                column: "SkillSetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_PlayerId",
                table: "TeamPlayer",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSetItems_SkillSets_SkillSetId",
                table: "SkillSetItems",
                column: "SkillSetId",
                principalTable: "SkillSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillSetItems_SkillSets_SkillSetId",
                table: "SkillSetItems");

            migrationBuilder.DropTable(
                name: "AssessmentResults");

            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillSetItems",
                table: "SkillSetItems");

            migrationBuilder.RenameTable(
                name: "SkillSetItems",
                newName: "SkillSetItemss");

            migrationBuilder.RenameIndex(
                name: "IX_SkillSetItems_SkillSetId",
                table: "SkillSetItemss",
                newName: "IX_SkillSetItemss_SkillSetId");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Players",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillSetItemss",
                table: "SkillSetItemss",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSetItemss_SkillSets_SkillSetId",
                table: "SkillSetItemss",
                column: "SkillSetId",
                principalTable: "SkillSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
