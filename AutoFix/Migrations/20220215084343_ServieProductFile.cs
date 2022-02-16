using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoFix.Migrations
{
    public partial class ServieProductFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "ServiceProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "ServiceProducts");
        }
    }
}
