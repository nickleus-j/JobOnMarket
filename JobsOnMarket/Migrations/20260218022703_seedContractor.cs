using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsOnMarket.Migrations
{
    /// <inheritdoc />
    public partial class seedContractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContractorUser",
                keyColumn: "ID",
                keyValue: 1,
                column: "UserId",
                value: "8e445800-1111-4543-1111-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "CustomerUser",
                keyColumn: "ID",
                keyValue: 2,
                column: "UserId",
                value: "8e445801-0000-4543-0000-9443d048cdb9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContractorUser",
                keyColumn: "ID",
                keyValue: 1,
                column: "UserId",
                value: "8e445801-0000-4543-0000-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "CustomerUser",
                keyColumn: "ID",
                keyValue: 2,
                column: "UserId",
                value: "8e445800-1111-4543-1111-9443d048cdb9");
        }
    }
}
