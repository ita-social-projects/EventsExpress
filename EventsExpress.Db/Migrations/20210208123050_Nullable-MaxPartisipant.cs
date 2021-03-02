using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class NullableMaxPartisipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxParticipants",
                table: "Events",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 2147483647);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxParticipants",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 2147483647,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
