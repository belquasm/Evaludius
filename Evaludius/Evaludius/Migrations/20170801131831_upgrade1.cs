using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Evaludius.Migrations
{
    public partial class upgrade1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPlayers_AppProducts_ProductId",
                table: "AppPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPlayers_AppOrders_TeamId",
                table: "AppPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AspNetUsers_CashierId",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders");

            migrationBuilder.DropTable(
                name: "AppCustomers");

            migrationBuilder.DropTable(
                name: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppOrders",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_CashierId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_CustomerId",
                table: "AppOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppPlayers",
                table: "AppPlayers");

            migrationBuilder.DropIndex(
                name: "IX_AppPlayers_ProductId",
                table: "AppPlayers");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "AppPlayers");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AppPlayers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AppPlayers");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "AppPlayers");

            migrationBuilder.RenameTable(
                name: "AppOrders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "AppPlayers",
                newName: "Players");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "BirthYear");

            migrationBuilder.RenameColumn(
                name: "CashierId",
                table: "Orders",
                newName: "TeamName");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Players",
                newName: "YearOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_AppPlayers_TeamId",
                table: "Players",
                newName: "IX_Players_TeamId");

            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Players",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillSetItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SkillSetId = table.Column<int>(nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillSetItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillSetItem_ProductCategories_SkillSetId",
                        column: x => x.SkillSetId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSetItem_SkillSetId",
                table: "SkillSetItem",
                column: "SkillSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Orders_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Orders_TeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "SkillSetItem");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "AppOrders");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "AppPlayers");

            migrationBuilder.RenameColumn(
                name: "TeamName",
                table: "AppOrders",
                newName: "CashierId");

            migrationBuilder.RenameColumn(
                name: "BirthYear",
                table: "AppOrders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "YearOfBirth",
                table: "AppPlayers",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TeamId",
                table: "AppPlayers",
                newName: "IX_AppPlayers_TeamId");

            migrationBuilder.AlterColumn<string>(
                name: "CashierId",
                table: "AppOrders",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "AppOrders",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AppOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "AppOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "AppOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "AppPlayers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "AppPlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "AppPlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "AppPlayers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppOrders",
                table: "AppOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppPlayers",
                table: "AppPlayers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuyingPrice = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Icon = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDiscontinued = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ProductCategoryId = table.Column<int>(nullable: false),
                    SellingPrice = table.Column<decimal>(nullable: false),
                    UnitsInStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProducts_AppProducts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppProducts_AppProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "AppProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_CashierId",
                table: "AppOrders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_CustomerId",
                table: "AppOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPlayers_ProductId",
                table: "AppPlayers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomers_Name",
                table: "AppCustomers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_Name",
                table: "AppProducts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_ParentId",
                table: "AppProducts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_ProductCategoryId",
                table: "AppProducts",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPlayers_AppProducts_ProductId",
                table: "AppPlayers",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPlayers_AppOrders_TeamId",
                table: "AppPlayers",
                column: "TeamId",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AspNetUsers_CashierId",
                table: "AppOrders",
                column: "CashierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppCustomers_CustomerId",
                table: "AppOrders",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
