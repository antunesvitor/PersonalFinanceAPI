using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionColumnExpenseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Expenses");
        }
    }
}
