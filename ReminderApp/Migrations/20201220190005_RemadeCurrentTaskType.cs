using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReminderApp.Migrations
{
    public partial class RemadeCurrentTaskType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "CurrentTasks");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "CurrentTasks",
                newName: "Date_Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date_Time",
                table: "CurrentTasks",
                newName: "Time");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CurrentTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
