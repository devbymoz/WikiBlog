using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiBlog.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priotity",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priotity",
                table: "Articles");
        }
    }
}
