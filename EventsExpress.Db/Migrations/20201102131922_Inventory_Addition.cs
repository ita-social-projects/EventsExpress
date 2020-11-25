using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class Inventory_Addition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_UnitOfMeasurings_UnitOfMeasuringId",
                table: "Inventories");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_UnitOfMeasurings_UnitOfMeasuringId",
                table: "Inventories",
                column: "UnitOfMeasuringId",
                principalTable: "UnitOfMeasurings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_UnitOfMeasurings_UnitOfMeasuringId",
                table: "Inventories");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_UnitOfMeasurings_UnitOfMeasuringId",
                table: "Inventories",
                column: "UnitOfMeasuringId",
                principalTable: "UnitOfMeasurings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
