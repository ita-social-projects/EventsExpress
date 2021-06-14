using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class AddCategoryOfMeasuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "UnitOfMeasurings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CategoriesOfMeasurings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesOfMeasurings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurings_CategoryId",
                table: "UnitOfMeasurings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOfMeasurings_CategoriesOfMeasurings_CategoryId",
                table: "UnitOfMeasurings",
                column: "CategoryId",
                principalTable: "CategoriesOfMeasurings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitOfMeasurings_CategoriesOfMeasurings_CategoryId",
                table: "UnitOfMeasurings");

            migrationBuilder.DropTable(
                name: "CategoriesOfMeasurings");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurings_CategoryId",
                table: "UnitOfMeasurings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "UnitOfMeasurings");
        }
    }
}
