using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foo_Form.Migrations
{
    /// <inheritdoc />
    public partial class recordsreferringtoowner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Records");
        }
    }
}
