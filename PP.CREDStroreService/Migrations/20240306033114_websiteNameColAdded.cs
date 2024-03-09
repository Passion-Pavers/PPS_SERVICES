using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP.CREDStroreService.Migrations
{
    /// <inheritdoc />
    public partial class websiteNameColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Credentials",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Website",
                table: "Credentials");
        }
    }
}
