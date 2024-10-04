using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminapp.Migrations
{
    /// <inheritdoc />
    public partial class oppdatertdbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "VacationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Schedules",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Schedules",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_ScheduleId",
                table: "VacationRequests",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequests_Schedules_ScheduleId",
                table: "VacationRequests",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequests_Schedules_ScheduleId",
                table: "VacationRequests");

            migrationBuilder.DropIndex(
                name: "IX_VacationRequests_ScheduleId",
                table: "VacationRequests");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "VacationRequests");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Schedules");
        }
    }
}
