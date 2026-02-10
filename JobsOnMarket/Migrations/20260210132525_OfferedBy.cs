using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsOnMarket.Migrations
{
    /// <inheritdoc />
    public partial class OfferedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfferedById",
                table: "JobOffer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_OfferedById",
                table: "JobOffer",
                column: "OfferedById");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Customer_OfferedById",
                table: "JobOffer",
                column: "OfferedById",
                principalTable: "Customer",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Customer_OfferedById",
                table: "JobOffer");

            migrationBuilder.DropIndex(
                name: "IX_JobOffer_OfferedById",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "OfferedById",
                table: "JobOffer");
        }
    }
}
