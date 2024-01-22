using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTrendTracker.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "visible",
                table: "BlogPosts",
                newName: "Visible");

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "BlogPosts",
                newName: "visible");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
