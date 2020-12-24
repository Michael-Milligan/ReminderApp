using Microsoft.EntityFrameworkCore.Migrations;

namespace ReminderApp.Migrations
{
    public partial class AddedIdToCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTasks_CurrentTasks_TaskId1",
                table: "CompletedTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedTasks",
                table: "CompletedTasks");

            migrationBuilder.DropIndex(
                name: "IX_CompletedTasks_TaskId1",
                table: "CompletedTasks");

            migrationBuilder.RenameColumn(
                name: "TaskId1",
                table: "CompletedTasks",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "CompletedTasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CompletedTasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedTasks",
                table: "CompletedTasks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTasks_TaskId",
                table: "CompletedTasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTasks_CurrentTasks_TaskId",
                table: "CompletedTasks",
                column: "TaskId",
                principalTable: "CurrentTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTasks_CurrentTasks_TaskId",
                table: "CompletedTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedTasks",
                table: "CompletedTasks");

            migrationBuilder.DropIndex(
                name: "IX_CompletedTasks_TaskId",
                table: "CompletedTasks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CompletedTasks",
                newName: "TaskId1");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "CompletedTasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId1",
                table: "CompletedTasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedTasks",
                table: "CompletedTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTasks_TaskId1",
                table: "CompletedTasks",
                column: "TaskId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTasks_CurrentTasks_TaskId1",
                table: "CompletedTasks",
                column: "TaskId1",
                principalTable: "CurrentTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
