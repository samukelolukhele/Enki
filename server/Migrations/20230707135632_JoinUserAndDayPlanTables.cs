using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class JoinUserAndDayPlanTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayPlans_Users_UserId",
                table: "DayPlans");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DayPlans",
                newName: "Userid");

            migrationBuilder.RenameIndex(
                name: "IX_DayPlans_UserId",
                table: "DayPlans",
                newName: "IX_DayPlans_Userid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Userid",
                table: "DayPlans",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "DayPlans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_DayPlans_Users_Userid",
                table: "DayPlans",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayPlans_Users_Userid",
                table: "DayPlans");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "DayPlans");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "DayPlans",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DayPlans_Userid",
                table: "DayPlans",
                newName: "IX_DayPlans_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "DayPlans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DayPlans_Users_UserId",
                table: "DayPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
