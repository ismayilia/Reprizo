using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reprizo.Migrations
{
    public partial class EditFeaturesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Features",
                newName: "TitleRight");

            migrationBuilder.AddColumn<string>(
                name: "TitleLeft",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleLeft",
                table: "Features");

            migrationBuilder.RenameColumn(
                name: "TitleRight",
                table: "Features",
                newName: "Title");
        }
    }
}
