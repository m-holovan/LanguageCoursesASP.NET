using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageCourses.Migrations
{
    /// <inheritdoc />
    public partial class addFieldDetailInformation_For_Teachers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetailInformation",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailInformation",
                table: "Teachers");
        }
    }
}
