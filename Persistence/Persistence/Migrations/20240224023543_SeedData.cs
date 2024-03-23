using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("71e9cc37-a261-4493-91b3-4f6d1e3cc020"), "This channel is dedicated to DotNet Core", "DotNetCore" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("a389a51d-d2a7-400e-97ac-e5b299aeba92"), "This channel is dedicated to Angular", "Angular" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("550afbdf-08b6-4560-b2f0-4f56bffb9d65"), "This channel is dedicated to ReactJs", "ReactJs" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "Id",
                keyValue: new Guid("550afbdf-08b6-4560-b2f0-4f56bffb9d65"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "Id",
                keyValue: new Guid("71e9cc37-a261-4493-91b3-4f6d1e3cc020"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "Id",
                keyValue: new Guid("a389a51d-d2a7-400e-97ac-e5b299aeba92"));
        }
    }
}
