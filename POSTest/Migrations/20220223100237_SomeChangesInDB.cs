using Microsoft.EntityFrameworkCore.Migrations;

namespace POSTest.Migrations
{
    public partial class SomeChangesInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizePrice",
                table: "Sizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizePrice",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Items");
        }
    }
}
