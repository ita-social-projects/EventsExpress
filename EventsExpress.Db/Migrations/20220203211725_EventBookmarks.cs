using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class EventBookmarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Events_EventId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_UserFromId",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                table: "Rates");

            migrationBuilder.RenameTable(
                name: "Rates",
                newName: "EventRelationships");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_UserFromId",
                table: "EventRelationships",
                newName: "IX_EventRelationships_UserFromId");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_EventId",
                table: "EventRelationships",
                newName: "IX_EventRelationships_EventId");

            migrationBuilder.AlterColumn<byte>(
                name: "Score",
                table: "EventRelationships",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "EventRelationships",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "Rate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRelationships",
                table: "EventRelationships",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EventRelationships_Discriminator_UserFromId_EventId",
                table: "EventRelationships",
                columns: new[] { "Discriminator", "UserFromId", "EventId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRelationships_Events_EventId",
                table: "EventRelationships",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRelationships_Users_UserFromId",
                table: "EventRelationships",
                column: "UserFromId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRelationships_Events_EventId",
                table: "EventRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRelationships_Users_UserFromId",
                table: "EventRelationships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRelationships",
                table: "EventRelationships");

            migrationBuilder.DropIndex(
                name: "IX_EventRelationships_Discriminator_UserFromId_EventId",
                table: "EventRelationships");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "EventRelationships");

            migrationBuilder.RenameTable(
                name: "EventRelationships",
                newName: "Rates");

            migrationBuilder.RenameIndex(
                name: "IX_EventRelationships_UserFromId",
                table: "Rates",
                newName: "IX_Rates_UserFromId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRelationships_EventId",
                table: "Rates",
                newName: "IX_Rates_EventId");

            migrationBuilder.AlterColumn<byte>(
                name: "Score",
                table: "Rates",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                table: "Rates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Events_EventId",
                table: "Rates",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_UserFromId",
                table: "Rates",
                column: "UserFromId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
