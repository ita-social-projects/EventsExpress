using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class ChangeCategoryOfMeasuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitOfMeasurings_CategoriesOfMeasurings_CategoryNameId",
                table: "UnitOfMeasurings");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurings_CategoryNameId",
                table: "UnitOfMeasurings");

            migrationBuilder.DropColumn(
                name: "CategoryNameId",
                table: "UnitOfMeasurings");

            migrationBuilder.DropColumn(
                name: "CategoryOfMeasuring",
                table: "UnitOfMeasurings");

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

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurings_CategoryId",
                table: "UnitOfMeasurings");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryNameId",
                table: "UnitOfMeasurings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryOfMeasuring",
                table: "UnitOfMeasurings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurings_CategoryNameId",
                table: "UnitOfMeasurings",
                column: "CategoryNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOfMeasurings_CategoriesOfMeasurings_CategoryNameId",
                table: "UnitOfMeasurings",
                column: "CategoryNameId",
                principalTable: "CategoriesOfMeasurings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
