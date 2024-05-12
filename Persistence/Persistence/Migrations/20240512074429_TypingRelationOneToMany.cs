using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class TypingRelationOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TypingNotifications_ChannelId",
                table: "TypingNotifications");

            migrationBuilder.DropIndex(
                name: "IX_TypingNotifications_SenderId",
                table: "TypingNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_TypingNotifications_ChannelId",
                table: "TypingNotifications",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingNotifications_SenderId",
                table: "TypingNotifications",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TypingNotifications_ChannelId",
                table: "TypingNotifications");

            migrationBuilder.DropIndex(
                name: "IX_TypingNotifications_SenderId",
                table: "TypingNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_TypingNotifications_ChannelId",
                table: "TypingNotifications",
                column: "ChannelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypingNotifications_SenderId",
                table: "TypingNotifications",
                column: "SenderId",
                unique: true);
        }
    }
}
