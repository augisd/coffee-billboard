using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class sqlite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coffees",
                table: "Coffees");

            migrationBuilder.RenameTable(
                name: "Coffees",
                newName: "Coffee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coffee",
                table: "Coffee",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coffee",
                table: "Coffee");

            migrationBuilder.RenameTable(
                name: "Coffee",
                newName: "Coffees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coffees",
                table: "Coffees",
                column: "Id");
        }
    }
}
