using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoFix.Migrations
{
    public partial class IsDeletedNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ShoppingCarts",
                type: "bit",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ServiceProducts",
                type: "bit",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FailureLoggings",
                type: "bit",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ShoppingCarts",
                type: "bit",
                maxLength: 128,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ServiceProducts",
                type: "bit",
                maxLength: 128,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FailureLoggings",
                type: "bit",
                maxLength: 128,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
