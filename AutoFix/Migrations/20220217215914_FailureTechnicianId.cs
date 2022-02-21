using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoFix.Migrations
{
    public partial class FailureTechnicianId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailureLoggings_AspNetUsers_UserId",
                table: "FailureLoggings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FailureLoggings",
                newName: "TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_FailureLoggings_UserId",
                table: "FailureLoggings",
                newName: "IX_FailureLoggings_TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_FailureLoggings_AspNetUsers_TechnicianId",
                table: "FailureLoggings",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailureLoggings_AspNetUsers_TechnicianId",
                table: "FailureLoggings");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "FailureLoggings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FailureLoggings_TechnicianId",
                table: "FailureLoggings",
                newName: "IX_FailureLoggings_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FailureLoggings_AspNetUsers_UserId",
                table: "FailureLoggings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
