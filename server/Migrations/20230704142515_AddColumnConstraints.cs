using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestone_Tasks_task_id",
                table: "Milestone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Milestone",
                table: "Milestone");

            migrationBuilder.RenameTable(
                name: "Milestone",
                newName: "Milestones");

            migrationBuilder.RenameIndex(
                name: "IX_Milestone_task_id",
                table: "Milestones",
                newName: "IX_Milestones_task_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Tasks_task_id",
                table: "Milestones",
                column: "task_id",
                principalTable: "Tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Tasks_task_id",
                table: "Milestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones");

            migrationBuilder.RenameTable(
                name: "Milestones",
                newName: "Milestone");

            migrationBuilder.RenameIndex(
                name: "IX_Milestones_task_id",
                table: "Milestone",
                newName: "IX_Milestone_task_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Milestone",
                table: "Milestone",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestone_Tasks_task_id",
                table: "Milestone",
                column: "task_id",
                principalTable: "Tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
