using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsOnMarket.Migrations
{
    /// <inheritdoc />
    public partial class dueDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DuetDate",
                table: "Job",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Job",
                newName: "DuetDate");
        }
    }
}
