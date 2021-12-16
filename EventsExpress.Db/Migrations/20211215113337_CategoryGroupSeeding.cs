using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class CategoryGroupSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoryGroups",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("78ed6ee2-9d5a-4802-aced-b3284e948a83"), "Wellness, Health&Fitness" });

            migrationBuilder.InsertData(
                table: "CategoryGroups",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("88b791a5-6ce3-4b50-80ae-65572991f676"), "Art&Craft" });

            migrationBuilder.InsertData(
                table: "CategoryGroups",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("d11d77e5-818d-41b4-affd-780a1991a16c"), "Education&Training" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryGroups",
                keyColumn: "Id",
                keyValue: new Guid("78ed6ee2-9d5a-4802-aced-b3284e948a83"));

            migrationBuilder.DeleteData(
                table: "CategoryGroups",
                keyColumn: "Id",
                keyValue: new Guid("88b791a5-6ce3-4b50-80ae-65572991f676"));

            migrationBuilder.DeleteData(
                table: "CategoryGroups",
                keyColumn: "Id",
                keyValue: new Guid("d11d77e5-818d-41b4-affd-780a1991a16c"));
        }
    }
}
