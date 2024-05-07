using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class DeleteSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "Id",
                keyValue: new Guid("a62c3e1a-bb44-4ef5-9ca1-9e071219ae26"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "Id",
                keyValue: new Guid("c9122c0b-8a19-4c13-8d18-e0da3cebd945"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "Id",
                keyValue: new Guid("dbe47a37-1fea-4946-8e97-486761d43036"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "ChannelType", "Description", "Name" },
                values: new object[] { new Guid("dbe47a37-1fea-4946-8e97-486761d43036"), 0, "This channel is dedicated to DotNet Core", "DotNetCore" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "ChannelType", "Description", "Name" },
                values: new object[] { new Guid("c9122c0b-8a19-4c13-8d18-e0da3cebd945"), 0, "This channel is dedicated to Angular", "Angular" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "ChannelType", "Description", "Name" },
                values: new object[] { new Guid("a62c3e1a-bb44-4ef5-9ca1-9e071219ae26"), 0, "This channel is dedicated to ReactJs", "ReactJs" });
        }
    }
}
