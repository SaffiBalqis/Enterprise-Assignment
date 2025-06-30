using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMTS.Migrations
{
    /// <inheritdoc />
    public partial class AddFilePathToForumPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ForumPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ForumPosts");
        }
    }
}
