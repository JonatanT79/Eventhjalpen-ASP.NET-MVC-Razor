using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EVTHJÄLPEN.Data.Migrations
{
    public partial class Addeduseradressguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "UserAdress",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserAdress");

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Events",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }
    }
}
